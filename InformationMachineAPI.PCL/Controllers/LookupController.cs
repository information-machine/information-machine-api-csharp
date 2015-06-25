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
    public class LookupController
    {
        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static LookupController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        public static LookupController GetInstance()
        {
            lock (syncObject)
            {
                if (null == instance)
                {
                    throw new Exception ("Please initialize before accessing the singleton instance");
                }
            }
            return instance;
        }

        /// <summary>
        /// Initialize instance with authentication and configuration parameters
        /// </summary>
        public static void Initialize(string clientId, string clientSecret)
        {
            lock (syncObject)
            {
                instance = new LookupController();
                instance.clientId = clientId;
                instance.clientSecret = clientSecret;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Get a list of all possible categories available for alternative product recommendations.
        /// </summary>
        /// <return>Returns the GetProductAlternativeTypesWrapper response from the API call</return>
        public GetProductAlternativeTypesWrapper LookupGetProductAlternativeTypes()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/product_alternative_types");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductAlternativeTypesWrapper>(response.Body);
        }

        /// <summary>
        /// Get a list of the units of measurement that could be associated with a product's nutrition facts.
        /// </summary>
        /// <return>Returns the GetUOMsWrapper response from the API call</return>
        public GetUOMsWrapper LookupGetUOMs()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/units_of_measurement");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetUOMsWrapper>(response.Body);
        }

        /// <summary>
        /// Get a list of all potential product categories.  Product categories follow a hierarchical structure that is defined through the "parent_id" field.
        /// </summary>
        /// <return>Returns the GetCategoriesWrapper response from the API call</return>
        public GetCategoriesWrapper LookupGetCategories()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/categories");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetCategoriesWrapper>(response.Body);
        }

        /// <summary>
        /// Get a list of all the nutrients that are available on a product's nutrition label.
        /// </summary>
        /// <return>Returns the GetNutrientsWrapper response from the API call</return>
        public GetNutrientsWrapper LookupGetNutrients()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/nutrients");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetNutrientsWrapper>(response.Body);
        }

        /// <summary>
        /// Get a list of all stores in the IM API infrastructure. This list is constantly expanding. For stores that have "can_scrape" flag set to "1", you can use the IM infrastructure to retrieve purchase history. To do this, connect the store using the endpoints under the "Users" section below.
        /// </summary>
        /// <return>Returns the GetStoresWrapper response from the API call</return>
        public GetStoresWrapper LookupGetStores()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/stores");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetStoresWrapper>(response.Body);
        }

        /// <summary>
        /// Get a list of all tags available through the IM infrastructure.
        /// </summary>
        /// <return>Returns the GetTagsWrapper response from the API call</return>
        public GetTagsWrapper LookupGetTags()
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/tags");


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
            if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetTagsWrapper>(response.Body);
        }

    }
} 