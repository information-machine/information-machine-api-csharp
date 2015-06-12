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
    public class UserPurchasesController
    {
 

        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;

        /// <summary>
        /// Constructor with authentication and configuration parameters
        /// </summary>
        public UserPurchasesController(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Get details about an identified purchase (specify "purchase_id") made my a specific user (specify "user_id").
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="purchaseId">Required parameter: TODO: type parameter description here</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with purchase item details)</param>
        /// <return>Returns the GetSingleUserPurchaseWrapper response from the API call</return>
        public GetSingleUserPurchaseWrapper UserPurchasesGetSingleUserPurchase(
                string userId,
                string purchaseId,
                bool? fullResp = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/purchases/{purchase_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "purchase_id", purchaseId }
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
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetSingleUserPurchaseWrapper>(response.Body);
        }

        /// <summary>
        /// Get full history of purchases made by a specified user from connected stores, must specify "user_id".
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: TODO: type parameter description here</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="purchaseDateBefore">Optional parameter: yyyy-MM-dd [e.g., 2015-04-18]</param>
        /// <param name="purchaseDateAfter">Optional parameter: yyyy-MM-dd [e.g., 2015-04-18]</param>
        /// <param name="purchaseTotalLess">Optional parameter: TODO: type parameter description here</param>
        /// <param name="purchaseTotalGreater">Optional parameter: TODO: type parameter description here</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with purchase item details)</param>
        /// <return>Returns the GetAllUserPurchasesWrapper response from the API call</return>
        public GetAllUserPurchasesWrapper UserPurchasesGetAllUserPurchases(
                string userId,
                int? page = null,
                int? perPage = null,
                string purchaseDateBefore = null,
                string purchaseDateAfter = null,
                double? purchaseTotalLess = null,
                double? purchaseTotalGreater = null,
                bool? fullResp = null)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/purchases");

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
                    { "purchase_date_before", purchaseDateBefore },
                    { "purchase_date_after", purchaseDateAfter },
                    { "purchase_total_less", purchaseTotalLess },
                    { "purchase_total_greater", purchaseTotalGreater },
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
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetAllUserPurchasesWrapper>(response.Body);
        }

    }
} 