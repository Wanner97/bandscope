using System.Net;

namespace BandScope.Client.APIs.ServerApiObjects
{
    public class ServerApiBase<T>
    {
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
