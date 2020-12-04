namespace DemoWebApp.Services
{
    public class ApiResultObject<T>
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public string Message { get; set; }

        public T Resource { get; set; }
    }
}
