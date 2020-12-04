using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebApp.Services
{
    public class ServiceHttpClient : IServiceHttpClient
    {
        public async Task<ApiCallResult<T>> Get<T>(HttpClient httpClient, string uri)
        {
            return await SendRequest<T>(httpClient, uri, HttpMethod.Get);
        }

        public async Task<ApiCallResult<T>> Post<T>(HttpClient httpClient, string uri, T content)
        {
            return await SendRequest<T>(httpClient, uri, HttpMethod.Post, JsonConvert.SerializeObject(content));
        }

        private async Task<ApiCallResult<T>> SendRequest<T>(HttpClient httpClient, string uri, HttpMethod method, string requestContent = null)
        {
            var apiCallResult = new ApiCallResult<T>();
            try
            {
                if (httpClient == null)
                    throw new ArgumentNullException(nameof(httpClient));

                if (uri == null)
                    throw new ArgumentNullException(nameof(uri));

                var req = BuildRequest(method, uri, requestContent);
                var response = await httpClient.SendAsync(req);

                if (response == null)
                    throw new InvalidOperationException("Response is null, cannot proceed.");

                apiCallResult.StatusCode = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    apiCallResult.Result = JsonConvert.DeserializeObject<T>(content);
                    apiCallResult.Success = true;
                }
                else
                {
                    apiCallResult.ErrorMessage = $"{response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                apiCallResult.ErrorMessage = $"An error occurred: {ex.Message}";
                apiCallResult.Success = false;
            }

            return apiCallResult;
        }

        private HttpRequestMessage BuildRequest(HttpMethod method, string uri, string content = null)
        {
            var req = new HttpRequestMessage(method, new Uri(uri, UriKind.Relative));

            if (content != null)
                req.Content = new StringContent(content, Encoding.UTF8, "application/json");

            return req;
        }
    }
}
