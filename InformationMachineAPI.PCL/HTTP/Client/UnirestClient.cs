using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InformationMachineAPI.PCL.Http.Request;
using InformationMachineAPI.PCL.Http.Response;
using unirest_net.http;
using UniHttpRequest = unirest_net.request.HttpRequest;
using UniHttpMethod = System.Net.Http.HttpMethod;

namespace InformationMachineAPI.PCL.Http.Client
{
    public class UnirestClient: IHttpClient
    {
        public static IHttpClient SharedClient { get; set; }

        static UnirestClient() {
            SharedClient = new UnirestClient();
        }

        private static UniHttpMethod ConvertHttpMethod(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Get:
                    return new UniHttpMethod(HttpMethod.Get.ToString());

                case HttpMethod.Post:
                    return new UniHttpMethod(HttpMethod.Post.ToString());

                case HttpMethod.Put:
                    return new UniHttpMethod(HttpMethod.Put.ToString());

                case HttpMethod.Patch:
                    return new UniHttpMethod(HttpMethod.Patch.ToString());

                case HttpMethod.Delete:
                    return new UniHttpMethod(HttpMethod.Delete.ToString());

                default:
                    throw new ArgumentOutOfRangeException("Unkown method" + method.ToString());
            }
        }

        private static UniHttpRequest ConvertRequest(HttpRequest request)
        {
            var uniMethod = ConvertHttpMethod(request.HttpMethod);
            var queryUrl = request.QueryUrl;
            
            //instantiate unirest request object
            UniHttpRequest uniRequest = new UniHttpRequest(uniMethod,queryUrl);

            //set request payload
            if (request.Body != null)
            {
                uniRequest.body(request.Body);
            }
            else if (request.FormParameters != null)
            {
                if (request.FormParameters.Any(p => p.Value is Stream || p.Value is FileStreamInfo))
                {
					//multipart
                    foreach (var kvp in request.FormParameters)
                    {
						if (kvp.Value is FileStreamInfo){
							var fileInfo = (FileStreamInfo) kvp.Value;
							uniRequest.field(kvp.Key,fileInfo.FileStream);
							continue;
						}
                        uniRequest.field(kvp.Key,kvp.Value);
                    }
                }
                else
                {
                    //URL Encode params
                    var paramsString = string.Join("&",
                        request.FormParameters.Select(kvp =>
                        string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value.ToString()))));
                    uniRequest.body(paramsString);
                    uniRequest.header("Content-Type", "application/x-www-form-urlencoded");
                }
            }

            //set request headers
            Dictionary<string, Object> headers = request.Headers.ToDictionary(item=> item.Key,item=> (Object) item.Value);
            uniRequest.headers(headers);

            //Set basic auth credentials if any
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                uniRequest.basicAuth(request.Username, request.Password);
            }

            return uniRequest;
        }

        private static HttpResponse ConvertResponse(HttpResponse<Stream> binaryResponse)
        {
            return new HttpResponse
            {
                Headers = binaryResponse.Headers,
                RawBody = binaryResponse.Body,
                StatusCode = binaryResponse.Code
            };
        }

        private static HttpResponse ConvertResponse(HttpResponse<string> stringResponse)
        {
            return new HttpStringResponse
            {
                Headers = stringResponse.Headers,
                RawBody = stringResponse.Raw,
                Body = stringResponse.Body,
                StatusCode = stringResponse.Code
            };
        }

        public HttpResponse ExecuteAsString(HttpRequest request)
        {
            UniHttpRequest uniRequest = ConvertRequest(request);
            return ConvertResponse(uniRequest.asString());
        }

        public Task<HttpResponse> ExecuteAsStringAsync(HttpRequest request)
        {
            return Task.Factory.StartNew(() => ExecuteAsString(request));
        }

        public HttpResponse ExecuteAsBinary(HttpRequest request)
        {
            UniHttpRequest uniRequest = ConvertRequest(request);
            return ConvertResponse(uniRequest.asBinary());
        }

        public Task<HttpResponse> ExecuteAsBinaryAsync(HttpRequest request)
        {
            return Task.Factory.StartNew(() => ExecuteAsString(request));
        }


        public HttpRequest Get(string queryUrl, Dictionary<string, string> headers, string username = null, string password = null)
        {
            return new HttpRequest(HttpMethod.Get, queryUrl, headers, username, password);
        }

        public HttpRequest Get(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Get,queryUrl);
        }

        public HttpRequest Post(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Post, queryUrl);
        }

        public HttpRequest Put(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Put, queryUrl);
        }

        public HttpRequest Delete(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Delete, queryUrl);
        }

        public HttpRequest Patch(string queryUrl)
        {
            return new HttpRequest(HttpMethod.Patch, queryUrl);
        }

        public HttpRequest Post(string queryUrl, Dictionary<string, string> headers, Dictionary<string, object> formParameters, string username = null,
            string password = null)
        {
            return new HttpRequest(HttpMethod.Post, queryUrl, headers,formParameters, username, password);
        }

        public HttpRequest PostBody(string queryUrl, Dictionary<string, string> headers, string body, string username = null, string password = null)
        {
            return new HttpRequest(HttpMethod.Post, queryUrl, headers, body, username, password);
        }

        public HttpRequest Put(string queryUrl, Dictionary<string, string> headers, Dictionary<string, object> formParameters, string username = null,
            string password = null)
        {
            return new HttpRequest(HttpMethod.Put, queryUrl, headers, formParameters, username, password);
        }

        public HttpRequest PutBody(string queryUrl, Dictionary<string, string> headers, string body, string username = null, string password = null)
        {
            return new HttpRequest(HttpMethod.Put, queryUrl, headers, body, username, password);
        }

        public HttpRequest Patch(string queryUrl, Dictionary<string, string> headers, Dictionary<string, object> formParameters, string username = null,
            string password = null)
        {
            return new HttpRequest(HttpMethod.Patch, queryUrl, headers, formParameters, username, password);
        }

        public HttpRequest PatchBody(string queryUrl, Dictionary<string, string> headers, string body, string username = null, string password = null)
        {
            return new HttpRequest(HttpMethod.Patch, queryUrl, headers, body, username, password);
        }

        public HttpRequest Delete(string queryUrl, Dictionary<string, string> headers, Dictionary<string, object> formParameters, string username = null,
            string password = null)
        {
            return new HttpRequest(HttpMethod.Delete, queryUrl, headers, formParameters, username, password);
        }

        public HttpRequest DeleteBody(string queryUrl, Dictionary<string, string> headers, string body, string username = null, string password = null)
        {
            return new HttpRequest(HttpMethod.Delete, queryUrl, headers, body, username, password);
        }
    }
}
