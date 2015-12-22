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
        /// Get all store connections for a specified user, must identify user by "user_id". Note: Within response focus on the following  properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)To get the value of credentials_status first check if scrape_status is one of the following: "Scraping", "Done", "Done With Warning"Sometimes the account can be locked because a security question, image captcha or sms verification code is needed in order to proceed with scraping.You can check whether the account is locked if property account_locked is set to true. To unlock the store connection visit the url the can be found in unlock_url property.For more information on this please visit the <a href="https://www.iamdata.co/docs?section=user-stores-section#userstoreunlock">docs</a> page.
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public GetAllStoresWrapper UserStoresGetAllStores(
                string userId,
                int? page = null,
                int? perPage = null)
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
                return APIHelper.JsonDeserialize<GetAllStoresWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Connect a user's store by specifying the user ID ("user_id"), store ID ("store_id") and user's credentials for specified store ("username" and "password"). You can find store IDs in Lookup/Stores section above or in this <a href="http://api.iamdata.co/docs/storeids" target="blank">LINK</a>. Note: Within response you should focus on the following properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress, credentials are set)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)To get the value of credentials_status first check if scrape_status is one of the following: "Scraping", "Done", "DoneWithWarning"
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public ConnectStoreWrapper UserStoresConnectStore(
                ConnectUserStoreRequest payload,
                string userId)
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

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<ConnectStoreWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Connect a user's store by specifying the user ID ("user_id"), store ID ("store_id") and OAuth2 provider (such as GMailAPI) You can find store IDs in Lookup/Stores section above or in this <a href="http://api.iamdata.co/docs/storeids" target="blank">LINK</a>. Note: Within response you should focus on the following properties: "scrape_status", "credentials_status" and OAuth providers where you will find a url link for authorization. Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)To get the value of credentials_status first check if scrape_status is one of the following: "Scraping", "Done", "DoneWithWarning"
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public ConnectStoreWrapper UserStoresConnectOAuthStore(
                ConnectOAuthUserStoreRequest payload,
                string userId)
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

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<ConnectStoreWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Get single store connection by specifying user ("user_id") and store connection ID ("id" - generated upon successful store connection). Note: Within response focus on the following properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)To get the value of credentials_status first check if scrape_status is one of the following: "Scraping", "Done", "Done With Warning"Sometimes the account can be locked because a security question, image captcha or sms verification code is needed in order to proceed with scraping.You can check whether the account is locked if property account_locked is set to true. To unlock the store connection visit the url the can be found in unlock_url property.For more information on this please visit the <a href="https://www.iamdata.co/docs?section=user-stores-section#userstoreunlock">docs</a> page.
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetSingleStoresWrapper response from the API call</return>
        public GetSingleStoresWrapper UserStoresGetSingleStore(
                string userId,
                int id)
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
                return APIHelper.JsonDeserialize<GetSingleStoresWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Update username and/or password of existing store connection, for a specified user ID ("user_id") and user store ID ("user_store_id"  - generated on store connect).
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the UpdateStoreConnectionWrapper response from the API call</return>
        public UpdateStoreConnectionWrapper UserStoresUpdateStoreConnection(
                UpdateUserStoreRequest payload,
                string userId,
                int id)
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
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"},
                {"content-type", "application/json; charset=utf-8"}
            };

            //append body params
            var _body = APIHelper.JsonSerialize(payload);

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.PutBody(_queryUrl, _headers, _body);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<UpdateStoreConnectionWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// Delete store connection for a specified user ("user_id") and specified store ("user_store_id"  - generated on store connect).
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the DeleteSingleStoreWrapper response from the API call</return>
        public DeleteSingleStoreWrapper UserStoresDeleteSingleStore(
                string userId,
                int id)
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
                {"user-agent", "IAMDATA V1"},
                {"accept", "application/json"}
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Delete(_queryUrl, _headers, null);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) ClientInstance.ExecuteAsString(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<DeleteSingleStoreWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

    }
} 