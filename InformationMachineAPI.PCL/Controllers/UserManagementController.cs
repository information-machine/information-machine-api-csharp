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
using InformationMachineAPI.PCL;
using InformationMachineAPI.PCL.Http.Request;
using InformationMachineAPI.PCL.Http.Response;
using InformationMachineAPI.PCL.Http.Client;

using InformationMachineAPI.PCL.Models;

namespace InformationMachineAPI.PCL.Controllers
{
    public partial class UserManagementController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserManagementController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static UserManagementController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new UserManagementController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Get all users associated with your account.
        /// </summary>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <return>Returns the GetAllUsersWrapper response from the API call</return>
        public GetAllUsersWrapper UserManagementGetAllUsers(
                int? page = null,
                int? perPage = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "page", page },
                { "per_page", perPage },
                { "client_id", Configuration.ClientId },
                { "client_secret", Configuration.ClientSecret }
            });

            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"}
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Bad request", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetAllUsersWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Register a new user by specifying "email", "zip" and "user_id". The "user_id" is mandatory and it represents the identifier you will use to identify your user in the IM API infrastructure.Note: The following characters are restricted within "user_id" string ---&gt; { '/', '^', '[',  '\\', 'w', '.', ']', '+', '$', '/' }
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the CreateUserWrapper response from the API call</return>
        public CreateUserWrapper UserManagementCreateUser(
                RegisterUserRequest payload)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "client_id", Configuration.ClientId },
                { "client_secret", Configuration.ClientSecret }
            });

            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"},
                {"content-type", "application/json; charset=utf-8"}
            };

            //append body params
            var _body = APIHelper.JsonSerialize(payload);

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.PostBody(_queryUrl, _headers, _body);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Bad request", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable entity", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<CreateUserWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Delete a user from the IM API infrastructure by specifying user's "id".
        /// </summary>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the DeleteUserWrapper response from the API call</return>
        public DeleteUserWrapper UserManagementDeleteUser(
                string id)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "id", id },
                { "client_id", Configuration.ClientId },
                { "client_secret", Configuration.ClientSecret }
            });

            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"}
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Delete(_queryUrl, _headers, null);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 404)
                throw new APIException(@"Not found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<DeleteUserWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Get user associated with your account specifying "id" of a user.
        /// </summary>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetSingleUserWrapper response from the API call</return>
        public GetSingleUserWrapper UserManagementGetSingleUser(
                string id)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "id", id }
            });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "client_id", Configuration.ClientId },
                { "client_secret", Configuration.ClientSecret }
            });

            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"}
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetSingleUserWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

    }
} 