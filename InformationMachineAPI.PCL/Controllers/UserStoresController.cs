/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationMachineAPI.PCL;
using InformationMachineAPI.PCL.Http.Request;
using InformationMachineAPI.PCL.Http.Response;
using InformationMachineAPI.PCL.Http.Client;
using InformationMachineAPI.PCL.Exceptions;
using InformationMachineAPI.PCL.Models;

namespace InformationMachineAPI.PCL.Controllers
{
    public partial class UserStoresController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserStoresController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static UserStoresController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new UserStoresController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Delete user store connection
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the DeleteSingleStoreWrapper response from the API call</return>
        public DeleteSingleStoreWrapper UserStoresDeleteSingleStore(string userId, long id)
        {
            Task<DeleteSingleStoreWrapper> t = UserStoresDeleteSingleStoreAsync(userId, id);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Delete user store connection
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the DeleteSingleStoreWrapper response from the API call</return>
        public async Task<DeleteSingleStoreWrapper> UserStoresDeleteSingleStoreAsync(string userId, long id)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
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
                { "user-agent", "" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Delete(_queryUrl, _headers, null);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<DeleteSingleStoreWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Update a user's store connection credentials
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the UpdateStoreConnectionWrapper response from the API call</return>
        public UpdateStoreConnectionWrapper UserStoresUpdateStoreConnection(UpdateUserStoreRequest payload, string userId, long id)
        {
            Task<UpdateStoreConnectionWrapper> t = UserStoresUpdateStoreConnectionAsync(payload, userId, id);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Update a user's store connection credentials
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the UpdateStoreConnectionWrapper response from the API call</return>
        public async Task<UpdateStoreConnectionWrapper> UserStoresUpdateStoreConnectionAsync(UpdateUserStoreRequest payload, string userId, long id)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
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
                { "user-agent", "" },
                { "accept", "application/json" },
                { "content-type", "application/json; charset=utf-8" }
            };

            //append body params
            var _body = APIHelper.JsonSerialize(payload);

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.PutBody(_queryUrl, _headers, _body);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<UpdateStoreConnectionWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get user store connection information
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the GetSingleStoresWrapper response from the API call</return>
        public GetSingleStoresWrapper UserStoresGetSingleStore(string userId, long id)
        {
            Task<GetSingleStoresWrapper> t = UserStoresGetSingleStoreAsync(userId, id);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get user store connection information
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="id">Required parameter: Example: </param>
        /// <return>Returns the GetSingleStoresWrapper response from the API call</return>
        public async Task<GetSingleStoresWrapper> UserStoresGetSingleStoreAsync(string userId, long id)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
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
                { "user-agent", "" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetSingleStoresWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Connect store for specified user
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public ConnectStoreWrapper UserStoresConnectOAuthStore(ConnectOAuthUserStoreRequest payload, string userId)
        {
            Task<ConnectStoreWrapper> t = UserStoresConnectOAuthStoreAsync(payload, userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Connect store for specified user
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public async Task<ConnectStoreWrapper> UserStoresConnectOAuthStoreAsync(ConnectOAuthUserStoreRequest payload, string userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores/oauth");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
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
                { "user-agent", "" },
                { "accept", "application/json" },
                { "content-type", "application/json; charset=utf-8" }
            };

            //append body params
            var _body = APIHelper.JsonSerialize(payload);

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.PostBody(_queryUrl, _headers, _body);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Bad request", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<ConnectStoreWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Connect store for specified user
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public ConnectStoreWrapper UserStoresConnectStore(ConnectUserStoreRequest payload, string userId)
        {
            Task<ConnectStoreWrapper> t = UserStoresConnectStoreAsync(payload, userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Connect store for specified user
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public async Task<ConnectStoreWrapper> UserStoresConnectStoreAsync(ConnectUserStoreRequest payload, string userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
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
                { "user-agent", "" },
                { "accept", "application/json" },
                { "content-type", "application/json; charset=utf-8" }
            };

            //append body params
            var _body = APIHelper.JsonSerialize(payload);

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.PostBody(_queryUrl, _headers, _body);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Bad request", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<ConnectStoreWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get all store connections associated with a user
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="page">Optional parameter: Example: </param>
        /// <param name="perPage">Optional parameter: Example: </param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public GetAllStoresWrapper UserStoresGetAllUserStores(string userId, int? page = null, int? perPage = null)
        {
            Task<GetAllStoresWrapper> t = UserStoresGetAllUserStoresAsync(userId, page, perPage);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get all store connections associated with a user
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="page">Optional parameter: Example: </param>
        /// <param name="perPage">Optional parameter: Example: </param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public async Task<GetAllStoresWrapper> UserStoresGetAllUserStoresAsync(string userId, int? page = null, int? perPage = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/stores");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
            });

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
                { "user-agent", "" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetAllStoresWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get all store connections associated with a API owner account
        /// </summary>
        /// <param name="page">Optional parameter: default:1</param>
        /// <param name="perPage">Optional parameter: default:1000</param>
        /// <param name="accountLocked">Optional parameter: Filter out locked store connections.</param>
        /// <param name="accountLockedDateAfter">Optional parameter: Filter out store connections locked after specified date. Expected format: yyyy-MM-dd HH:mm:ss<br />[e.g., 2016-06-14 16:29:23]</param>
        /// <param name="connectionLost">Optional parameter: Filter out store connections that lost credentials status "Verified".</param>
        /// <param name="connectionLostDateAfter">Optional parameter: Filter out store connections that lost credentials status "Verified" after specified date. Expected format: yyyy-MM-dd HH:mm:ss<br />[e.g., 2016-06-14 16:29:23]</param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public GetAllStoresWrapper UserStoresGetAllAPIAccountStores(
                int? page = null,
                int? perPage = null,
                bool? accountLocked = null,
                DateTime? accountLockedDateAfter = null,
                bool? connectionLost = null,
                DateTime? connectionLostDateAfter = null)
        {
            Task<GetAllStoresWrapper> t = UserStoresGetAllAPIAccountStoresAsync(page, perPage, accountLocked, accountLockedDateAfter, connectionLost, connectionLostDateAfter);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get all store connections associated with a API owner account
        /// </summary>
        /// <param name="page">Optional parameter: default:1</param>
        /// <param name="perPage">Optional parameter: default:1000</param>
        /// <param name="accountLocked">Optional parameter: Filter out locked store connections.</param>
        /// <param name="accountLockedDateAfter">Optional parameter: Filter out store connections locked after specified date. Expected format: yyyy-MM-dd HH:mm:ss<br />[e.g., 2016-06-14 16:29:23]</param>
        /// <param name="connectionLost">Optional parameter: Filter out store connections that lost credentials status "Verified".</param>
        /// <param name="connectionLostDateAfter">Optional parameter: Filter out store connections that lost credentials status "Verified" after specified date. Expected format: yyyy-MM-dd HH:mm:ss<br />[e.g., 2016-06-14 16:29:23]</param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public async Task<GetAllStoresWrapper> UserStoresGetAllAPIAccountStoresAsync(
                int? page = null,
                int? perPage = null,
                bool? accountLocked = null,
                DateTime? accountLockedDateAfter = null,
                bool? connectionLost = null,
                DateTime? connectionLostDateAfter = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/store_connections");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "page", page },
                { "per_page", perPage },
                { "account_locked", accountLocked },
                { "account_locked_date_after", accountLockedDateAfter },
                { "connection_lost", connectionLost },
                { "connection_lost_date_after", connectionLostDateAfter },
                { "client_id", Configuration.ClientId },
                { "client_secret", Configuration.ClientSecret }
            });


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetAllStoresWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 