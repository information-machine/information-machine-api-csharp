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
    public partial class UserPurchasesController: BaseController
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
        /// <param name="storeId">Optional parameter: Check Lookup/Stores section for ID of all stores. E.g., Amazon = 4, Walmart = 3.</param>
        /// <param name="foodOnly">Optional parameter: default:false [Filter out food purchase items.]</param>
        /// <param name="upcOnly">Optional parameter: default:false [Filter out purchase items with UPC.]</param>
        /// <param name="showProductDetails">Optional parameter: default:false [Show details of a purchased products (image, nutrients, ingredients, manufacturer, etc..)]</param>
        /// <param name="receiptsOnly">Optional parameter: default:false [Filter out purchases transcribed from receipts.]</param>
        /// <param name="upcResolvedAfter">Optional parameter: List only purchases having UPC resolved by IM after specified date. Expected format: "yyyy-MM-dd"</param>
        /// <return>Returns the GetUserPurchaseHistoryWrapper response from the API call</return>
        public GetUserPurchaseHistoryWrapper UserPurchasesGetPurchaseHistoryUnified(
                string userId,
                int? storeId = null,
                bool? foodOnly = null,
                bool? upcOnly = null,
                bool? showProductDetails = null,
                bool? receiptsOnly = null,
                string upcResolvedAfter = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/purchase_history");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
            });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "store_id", storeId },
                { "food_only", foodOnly },
                { "upc_only", upcOnly },
                { "show_product_details", showProductDetails },
                { "receipts_only", receiptsOnly },
                { "upc_resolved_after", upcResolvedAfter },
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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetUserPurchaseHistoryWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
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
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/purchases/{purchase_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "purchase_id", purchaseId }
            });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "full_resp", fullResp },
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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetSingleUserPurchaseWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// This endpoint is obsolete. Consider using "users/{user_id}/purchase_history/".   Get history of purchases made by a specified user from connected stores, must specify "user_id".
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="storeId">Optional parameter: Check Lookup/Stores section for ID of all stores. E.g., Amazon = 4, Walmart = 3.</param>
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
        /// <param name="receiptsOnly">Optional parameter: default:false [Filter out purchases transcribed from receipts.]</param>
        /// <param name="upcResolvedAfter">Optional parameter: List only purchases having UPC resolved by IM after specified date. Expected format: "yyyy-MM-dd"</param>
        /// <return>Returns the GetAllUserPurchasesWrapper response from the API call</return>
        public GetAllUserPurchasesWrapper UserPurchasesGetAllUserPurchases(
                string userId,
                int? storeId = null,
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
                bool? upcOnly = null,
                bool? receiptsOnly = null,
                string upcResolvedAfter = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/purchases");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
            });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "store_id", storeId },
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
                { "receipts_only", receiptsOnly },
                { "upc_resolved_after", upcResolvedAfter },
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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetAllUserPurchasesWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

        /// <summary>
        /// This endpoint is obsolete. Consider using "users/{user_id}/purchase_history/".   Get history of loyalty purchases made by a specified user from connected stores, must specify "user_id".
        /// </summary>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <param name="storeId">Optional parameter: Check Lookup/Stores section for ID of all stores. E.g., Amazon = 4, Walmart = 3.</param>
        /// <param name="page">Optional parameter: default:1</param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="foodOnly">Optional parameter: default:false [Filter out food purchase items.]</param>
        /// <param name="upcOnly">Optional parameter: default:false [Filter out purchase items with UPC.]</param>
        /// <param name="upcResolvedAfter">Optional parameter: List only purchases having UPC resolved by IM after specified date. Expected format: "yyyy-MM-dd".</param>
        /// <param name="recordCreatedAfter">Optional parameter: List only purchases collected, i.e., database inserted by bots, after specified date. Not to be confused with purchase date (not existing for many loyalty purchases). Expected format: "yyyy-MM-dd"</param>
        /// <return>Returns the GetAllUserLoyaltyPurchasesWrapper response from the API call</return>
        public GetAllUserLoyaltyPurchasesWrapper UserPurchasesGetAllUserLoyaltyPurchases(
                string userId,
                int? storeId = null,
                int? page = null,
                int? perPage = null,
                bool? foodOnly = null,
                bool? upcOnly = null,
                string upcResolvedAfter = null,
                string recordCreatedAfter = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/loyalty_purchases");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
            });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "store_id", storeId },
                { "page", page },
                { "per_page", perPage },
                { "food_only", foodOnly },
                { "upc_only", upcOnly },
                { "upc_resolved_after", upcResolvedAfter },
                { "record_created_after", recordCreatedAfter },
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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if ((_response.StatusCode < 200) || (_response.StatusCode > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", _context);

            try
            {
                return APIHelper.JsonDeserialize<GetAllUserLoyaltyPurchasesWrapper>(_response.Body);
            }
            catch (Exception ex)
            {
                throw new APIException("Failed to parse the response: " + ex.Message, _context);
            }
        }

    }
} 