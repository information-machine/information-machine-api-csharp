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
    public class ProductsController
    {
 

        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;

        /// <summary>
        /// Constructor with authentication and configuration parameters
        /// </summary>
        public ProductsController(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// You can query the IM product database by either product name or UPC/EAN/ISBN identifier. Note: If both parameters are specified, UPC/EAN/ISBN has higher priority.
        /// </summary>
        /// <param name="name">Optional parameter: TODO: type parameter description here</param>
        /// <param name="id">Optional parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <return>Returns the GetProductsWrapper response from the API call</return>
        public GetProductsWrapper ProductsGetProducts(
                string name = null,
                string id = null,
                int? page = null,
                int? perPage = null,
                bool? fullResp = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "name", name },
                    { "id", id },
                    { "page", page },
                    { "per_page", perPage },
                    { "full_resp", fullResp },
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
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductsWrapper>(response.Body);
        }

        /// <summary>
        /// Get details about a single product in the IM database by specifying a product ID.
        /// </summary>
        /// <param name="productId">Required parameter: TODO: type parameter description here</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <return>Returns the GetProductWrapper response from the API call</return>
        public GetProductWrapper ProductsGetProduct(
                string productId,
                bool? fullResp = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products/{product_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "product_id", productId }
                });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "full_resp", fullResp },
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
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductWrapper>(response.Body);
        }

        /// <summary>
        /// Get alternatives for a specified product. Note: Must specify both product ID and ID for desired alternative type.
        /// </summary>
        /// <param name="productId">Required parameter: TODO: type parameter description here</param>
        /// <param name="alternativeTypeId">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetProductAlternativesWrapper response from the API call</return>
        public GetProductAlternativesWrapper ProductsGetProductAlternatives(
                string productId,
                string alternativeTypeId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products/{product_id}/alternatives/{alternative_type_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "product_id", productId },
                    { "alternative_type_id", alternativeTypeId }
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
            if (response.Code == 404)
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductAlternativesWrapper>(response.Body);
        }

        /// <summary>
        /// Get all purchases a user has made for a product by specifying the associated product ID.
        /// </summary>
        /// <param name="productId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <return>Returns the GetProductPurchasesWrapper response from the API call</return>
        public GetProductPurchasesWrapper ProductsGetProductPurchases(
                string productId,
                int? page = null,
                int? perPage = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products/{product_id}/purchases");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "product_id", productId }
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
            if (response.Code == 404)
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductPurchasesWrapper>(response.Body);
        }

        /// <summary>
        /// Get prices (from available stores) for specified product IDs. Note: It is possible to query 20 product IDs per each request. Please separate IDs with commas (e.g.: “325365, 89300”).
        /// </summary>
        /// <param name="productIds">Optional parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetProductPricesWrapper response from the API call</return>
        public GetProductPricesWrapper ProductsGetProductPrices(
                string productIds = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products_prices");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "product_ids", productIds },
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
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductPricesWrapper>(response.Body);
        }

        /// <summary>
        /// Get product alternatives for a specified alternative type (e.g.: “type_id: 7” will display alternatives of the “general” type) for a list of products specified by product IDs. Note: See “Lookup” section, “product_alternative_type” for list of all possible alternative types.
        /// </summary>
        /// <param name="productIds">Optional parameter: TODO: type parameter description here</param>
        /// <param name="typeId">Optional parameter: TODO: type parameter description here</param>
        /// <return>Returns the GetProductsAlternativesWrapper response from the API call</return>
        public GetProductsAlternativesWrapper ProductsGetProductsAlternatives(
                string productIds = null,
                string typeId = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/products_alternatives");


            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "product_ids", productIds },
                    { "type_id", typeId },
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

            else if (response.Code == 404)
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetProductsAlternativesWrapper>(response.Body);
        }

        /// <summary>
        /// Get full history of products purchased by a specified user at connected stores, must define “user_id".
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <param name="foodOnly">Optional parameter: default:false (set true to list food products only)</param>
        /// <return>Returns the GetUserProducts response from the API call</return>
        public GetUserProducts ProductsGetUserProducts(
                string userId,
                int? page = null,
                int? perPage = null,
                bool? fullResp = null,
                bool? foodOnly = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/products");

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
                    { "full_resp", fullResp },
                    { "food_only", foodOnly },
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
                throw new APIException(@"Not found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetUserProducts>(response.Body);
        }

    }
} 