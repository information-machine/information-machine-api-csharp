/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
using unirest_net.request;
using InformationMachineAPI.PCL;
using InformationMachineAPI.PCL.Models;
 
namespace InformationMachineAPI.PCL.Controllers
{
    public class UsersController
    {
 

        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;

        /// <summary>
        /// Constructor with authentication and configuration parameters
        /// </summary>
        public UsersController(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Get all users associated with your account.
        /// </summary>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <return>Returns the GetAllUsersWrapper response from the API call</return>
        public GetAllUsersWrapper UsersGetAllUsers(
                int? page = null,
                int? perPage = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "page", page },
                    { "per_page", perPage },
                    { "client_id", clientId },
                    { "client_secret", clientSecret }
                });

            //validate and preprocess url
            string queryUrl = APIHelper.CleanUrl(queryBuilder);

            //prepare and invoke the API call request to fetch the response
            HttpRequest request = Unirest.get(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 400)
                throw new APIException(@"Bad request", 400);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetAllUsersWrapper>(response.Body);
        }

        /// <summary>
        /// Register a new user by specifying "email", "zip" and "user_id". The “user_id” is mandatory and it represents the identifier you will use to identify your user in the IM API infrastructure.Note: The following characters are restricted within "user_id" string ---&gt; { '/', '^', '[', '-', '\\', 'w', '.', ']', '+', '$', '/' }
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the CreateUserWrapper response from the API call</return>
        public CreateUserWrapper UsersCreateUser(
                RegisterUserRequest payload)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret }
                });

            //validate and preprocess url
            string queryUrl = APIHelper.CleanUrl(queryBuilder);

            //prepare and invoke the API call request to fetch the response
            HttpRequest request = Unirest.post(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json")
                .header("content-type", "application/json; charset=utf-8")
                .body(APIHelper.JsonSerialize(payload));

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 400)
                throw new APIException(@"Bad request", 400);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable entity", 422);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<CreateUserWrapper>(response.Body);
        }

        /// <summary>
        /// Delete a user from the IM API infrastructure by specifying user's "id".
        /// </summary>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the DeleteUserWrapper response from the API call</return>
        public DeleteUserWrapper UsersDeleteUser(
                string id)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "id", id },
                    { "client_id", clientId },
                    { "client_secret", clientSecret }
                });

            //validate and preprocess url
            string queryUrl = APIHelper.CleanUrl(queryBuilder);

            //prepare and invoke the API call request to fetch the response
            HttpRequest request = Unirest.delete(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 404)
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<DeleteUserWrapper>(response.Body);
        }

        /// <summary>
        /// Get user associated with your account specifying “id”.
        /// </summary>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetSingleUserWrapper response from the API call</return>
        public GetSingleUserWrapper UsersGetSingleUser(
                string id)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "id", id }
                });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret }
                });

            //validate and preprocess url
            string queryUrl = APIHelper.CleanUrl(queryBuilder);

            //prepare and invoke the API call request to fetch the response
            HttpRequest request = Unirest.get(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 400)
                throw new APIException(@"Bad request", 400);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetSingleUserWrapper>(response.Body);
        }

    }
} 