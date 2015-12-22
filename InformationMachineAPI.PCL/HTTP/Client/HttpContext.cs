using InformationMachineAPI.PCL.Http.Request;
using InformationMachineAPI.PCL.Http.Response;
namespace InformationMachineAPI.PCL.Http.Client
{
    /// <summary>
    /// Represents the contextual information of HTTP request and response
    /// </summary>
    public class HttpContext
    {
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

		public HttpContext(HttpRequest request, HttpResponse response)
        {
            Request = request;
            Response = response;
        }
    }
}
