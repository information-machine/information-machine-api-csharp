using System;
using System.Collections.Generic;

namespace InformationMachineAPI.PCL.Http.Request
{
    public class HttpRequest
    {
        public HttpMethod HttpMethod { get; set; }
        public String QueryUrl { get; set; }
        public Dictionary<String, String> Headers { get; set; }
        public Dictionary<String, Object> FormParameters { get; set; }
        public String Body { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public HttpRequest(HttpMethod method, String queryUrl)
        {
            this.HttpMethod = method;
            this.QueryUrl = queryUrl;
        }

        public HttpRequest(HttpMethod method, string queryUrl, Dictionary<String, String> headers, string username, string password)
            :this (method,queryUrl)
        {
            this.Headers = headers;
            this.Username = username;
            this.Password = password;

        }
        public HttpRequest(HttpMethod method, string queryUrl, Dictionary<String, String> headers, String body, string username, string password)
            : this(method, queryUrl, headers, username, password)
        {
            this.Body = body;
        }
        public HttpRequest(HttpMethod method, string queryUrl, Dictionary<String, String> headers,Dictionary<String, Object> formParameters, string username, string password)
            : this(method, queryUrl, headers, username, password)
        {
            this.FormParameters = formParameters;
        }
    }
}
