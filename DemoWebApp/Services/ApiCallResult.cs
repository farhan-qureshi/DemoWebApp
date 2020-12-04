using System.Net;

namespace DemoWebApp.Services
{
    public class ApiCallResult<T>
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T Result { get; set; }

        public string ErrorMessage { get; set; }

    }
}
