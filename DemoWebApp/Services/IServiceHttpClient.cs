using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebApp.Services
{
    public interface IServiceHttpClient
    {
        Task<ApiCallResult<T>> Get<T>(HttpClient httpClient, string uri);
        Task<ApiCallResult<T>> Post<T>(HttpClient httpClient, string uri, T content);
    }
}
