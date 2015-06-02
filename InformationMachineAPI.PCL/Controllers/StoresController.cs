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
    public class StoresController
    {
 

        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;

        /// <summary>
        /// Constructor with authentication and configuration parameters
        /// </summary>
        public StoresController(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Get all store connections for a specified user, must identify user by "user_id". Note: Within response focus on the following  properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetAllStoresWrapper response from the API call</return>
        public GetAllStoresWrapper StoresGetAllStores(
                string userId,
                int? page = null,
                int? perPage = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/stores");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId }
                });

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
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetAllStoresWrapper>(response.Body);
        }

        /// <summary>
        /// Connect a user's store by specifying the user ID (“user_id”), store ID (“store_id”) and user’s credentials for specified store (“username” and “password”). Note: Within response you should focus on the following properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the ConnectStoreWrapper response from the API call</return>
        public ConnectStoreWrapper StoresConnectStore(
                ConnectUserStoreRequest payload,
                string userId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/stores");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId }
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

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<ConnectStoreWrapper>(response.Body);
        }

        /// <summary>
        /// Get single store connection by specifying user ("user_id") and store connection ID ("user_store_id" - generated upon successful store connection). Note: Within response focus on the following properties: "scrape_status" and "credentials_status". Possible values for "scrape_status": "Not defined""Pending" - (scraping request is in queue and waiting to be processed)"Scraping" - (scraping is in progress)"Done" - (scraping is finished)"Done With Warning" - (not all purchases were scraped)Possible values for "credentials_status":"Not defined""Verified" - (scraping bots are able to log in to store site)"Invalid" - (supplied user name or password are not valid)"Unknown" - (user name or password are not know)"Checking" - (credentials verification is in progress)
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetSingleStoresWrapper response from the API call</return>
        public GetSingleStoresWrapper StoresGetSingleStore(
                string userId,
                int id)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
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
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetSingleStoresWrapper>(response.Body);
        }

        /// <summary>
        /// Update username and/or password of existing store connection, for a specified user ID ("user_id") and user store ID ("user_store_id"  - generated on store connect).
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the UpdateStoreConnectionWrapper response from the API call</return>
        public UpdateStoreConnectionWrapper StoresUpdateStoreConnection(
                UpdateUserStoreRequest payload,
                string userId,
                int id)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
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
            HttpRequest request = Unirest.put(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json")
                .header("content-type", "application/json; charset=utf-8")
                .body(APIHelper.JsonSerialize(payload));

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<UpdateStoreConnectionWrapper>(response.Body);
        }

        /// <summary>
        /// Delete store connection for a specified user ("user_id") and specified store ("user_store_id"  - generated on store connect).
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="id">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the DeleteSingleStoreWrapper response from the API call</return>
        public DeleteSingleStoreWrapper StoresDeleteSingleStore(
                string userId,
                int id)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/stores/{id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
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
            HttpRequest request = Unirest.delete(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<DeleteSingleStoreWrapper>(response.Body);
        }

    }
} 