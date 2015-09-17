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
    public partial class UserPurchasesController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserPurchasesController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static UserPurchasesController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new UserPurchasesController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Get history of purchases made by a specified user from connected stores, must specify "user_id".
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="page">Optional parameter: default:1</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="purchaseDateFrom">Optional parameter: Define multiple date ranges by specifying "from" range date components, separated by comma ",". Equal number of "from" and "to" parameters is mandatory. Expected format: "yyyy-MM-dd, yyyy-MM-dd"e.g., "2015-04-18, 2015-06-25"</param>
        /// <param name="purchaseDateTo">Optional parameter: Define multiple date ranges by specifying "to" range date components, separated by comma ",". Equal number of "from" and "to" parameters is mandatory. Expected format: "yyyy-MM-dd, yyyy-MM-dd"e.g., "2015-04-28, 2015-07-05"</param>
        /// <param name="purchaseDateBefore">Optional parameter: Filter out purchases made before specified date. Expected format: yyyy-MM-dd[e.g., 2015-04-18]</param>
        /// <param name="purchaseDateAfter">Optional parameter: Filter out purchases made after specified date. Expected format: yyyy-MM-dd[e.g., 2015-04-18]</param>
        /// <param name="purchaseTotalFrom">Optional parameter: Define multiple total purchase price ranges by specifying "from" range price components, separated by comma ",". Equal number of "from" and "to" parameters is mandatory. Expected format: "X.YZ, X.YZ"e.g., "5.5, 16.5"</param>
        /// <param name="purchaseTotalTo">Optional parameter: Define multiple total purchase price ranges by specifying "to" range price components, separated by comma ",". Equal number of "from" and "to" parameters is mandatory. Expected format: "X.YZ, X.YZ"e.g., "5.7, 20"</param>
        /// <param name="purchaseTotalLess">Optional parameter: Filter out purchases with grand total price less than specified amount.</param>
        /// <param name="purchaseTotalGreater">Optional parameter: Filter out purchases with grand total price greater than specified amount.</param>
        /// <param name="fullResp">Optional parameter: default:false [Set true for response with purchase item details.]</param>
        /// <param name="foodOnly">Optional parameter: default:false [Filter out food purchase items.]</param>
        /// <param name="upcOnly">Optional parameter: default:false [Filter out purchase items with UPC.]</param>
        /// <return>Returns the GetAllUserPurchasesWrapper response from the API call</return>
        public GetAllUserPurchasesWrapper UserPurchasesGetAllUserPurchases(
                string userId,
                int? page = null,
                int? perPage = null,
                string purchaseDateFrom = null,
                string purchaseDateTo = null,
                string purchaseDateBefore = null,
                string purchaseDateAfter = null,
                string purchaseTotalFrom = null,
                string purchaseTotalTo = null,
                double? purchaseTotalLess = null,
                double? purchaseTotalGreater = null,
                bool? fullResp = null,
                bool? foodOnly = null,
                bool? upcOnly = null)
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
                    { "purchase_date_from", purchaseDateFrom },
                    { "purchase_date_to", purchaseDateTo },
                    { "purchase_date_before", purchaseDateBefore },
                    { "purchase_date_after", purchaseDateAfter },
                    { "purchase_total_from", purchaseTotalFrom },
                    { "purchase_total_to", purchaseTotalTo },
                    { "purchase_total_less", purchaseTotalLess },
                    { "purchase_total_greater", purchaseTotalGreater },
                    { "full_resp", fullResp },
                    { "food_only", foodOnly },
                    { "upc_only", upcOnly },
                    { "client_id", Configuration.ClientId },
                    { "client_secret", Configuration.ClientSecret }
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
                    { "client_id", Configuration.ClientId },
                    { "client_secret", Configuration.ClientSecret }
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

    }
} 