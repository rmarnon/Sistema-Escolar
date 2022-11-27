using System.Net;

namespace Sistema.Escolar.Util
{
    public class Result<T>
    {
        public T Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public bool Error { get; set; }
        public List<string> Message { get; set; } = new();
    }
}
