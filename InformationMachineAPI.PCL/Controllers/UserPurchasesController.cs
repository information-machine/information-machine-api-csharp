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
        /// Get specified purchase details
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="purchaseId">Required parameter: Example: </param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with purchase item details)</param>
        /// <return>Returns the GetSingleUserPurchaseWrapper response from the API call</return>
        public GetSingleUserPurchaseWrapper UserPurchasesGetSingleUserPurchase(string userId, string purchaseId, bool? fullResp = null)
        {
            Task<GetSingleUserPurchaseWrapper> t = UserPurchasesGetSingleUserPurchaseAsync(userId, purchaseId, fullResp);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get specified purchase details
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="purchaseId">Required parameter: Example: </param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with purchase item details)</param>
        /// <return>Returns the GetSingleUserPurchaseWrapper response from the API call</return>
        public async Task<GetSingleUserPurchaseWrapper> UserPurchasesGetSingleUserPurchaseAsync(string userId, string purchaseId, bool? fullResp = null)
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
                { "user-agent", "" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetSingleUserPurchaseWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get purchases made by a specified user [purchased product mode]
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="storeId">Optional parameter: Check Lookup/Stores section for ID of all stores. E.g., Amazon = 4, Walmart = 3.</param>
        /// <param name="foodOnly">Optional parameter: default:false [Filter out food purchase items.]</param>
        /// <param name="upcOnly">Optional parameter: default:false [Filter out purchase items with UPC.]</param>
        /// <param name="showProductDetails">Optional parameter: default:false [Show details of a purchased products (image, nutrients, ingredients, manufacturer, etc..)]</param>
        /// <param name="receiptsOnly">Optional parameter: default:false [Filter out purchases transcribed from receipts.]</param>
        /// <param name="upcResolvedAfter">Optional parameter: List only purchases having UPC resolved by IM after specified date. Expected format: "yyyy-MM-dd"</param>
        /// <param name="purchaseDateBefore">Optional parameter: Retrieve purchases made during and before specified date. Combined with "purchase_date_after" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="purchaseDateAfter">Optional parameter: Retrieve purchases made during and after specified date. Combined with "purchase_date_before" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="recordedAtBefore">Optional parameter: Retrieve purchases after it is created in our database. Combined with "recorded_at_after" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="recordedAtAfter">Optional parameter: Retrieve purchases after it is created in our database. Combined with "recorded_at_before" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <return>Returns the GetUserPurchaseHistoryWrapper response from the API call</return>
        public GetUserPurchaseHistoryWrapper UserPurchasesGetPurchaseHistoryUnified(
                string userId,
                int? storeId = null,
                bool? foodOnly = null,
                bool? upcOnly = null,
                bool? showProductDetails = true,
                bool? receiptsOnly = null,
                DateTime? upcResolvedAfter = null,
                DateTime? purchaseDateBefore = null,
                DateTime? purchaseDateAfter = null,
                DateTime? recordedAtBefore = null,
                DateTime? recordedAtAfter = null)
        {
            Task<GetUserPurchaseHistoryWrapper> t = UserPurchasesGetPurchaseHistoryUnifiedAsync(userId, storeId, foodOnly, upcOnly, showProductDetails, receiptsOnly, upcResolvedAfter, purchaseDateBefore, purchaseDateAfter, recordedAtBefore, recordedAtAfter);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get purchases made by a specified user [purchased product mode]
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="storeId">Optional parameter: Check Lookup/Stores section for ID of all stores. E.g., Amazon = 4, Walmart = 3.</param>
        /// <param name="foodOnly">Optional parameter: default:false [Filter out food purchase items.]</param>
        /// <param name="upcOnly">Optional parameter: default:false [Filter out purchase items with UPC.]</param>
        /// <param name="showProductDetails">Optional parameter: default:false [Show details of a purchased products (image, nutrients, ingredients, manufacturer, etc..)]</param>
        /// <param name="receiptsOnly">Optional parameter: default:false [Filter out purchases transcribed from receipts.]</param>
        /// <param name="upcResolvedAfter">Optional parameter: List only purchases having UPC resolved by IM after specified date. Expected format: "yyyy-MM-dd"</param>
        /// <param name="purchaseDateBefore">Optional parameter: Retrieve purchases made during and before specified date. Combined with "purchase_date_after" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="purchaseDateAfter">Optional parameter: Retrieve purchases made during and after specified date. Combined with "purchase_date_before" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="recordedAtBefore">Optional parameter: Retrieve purchases after it is created in our database. Combined with "recorded_at_after" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <param name="recordedAtAfter">Optional parameter: Retrieve purchases after it is created in our database. Combined with "recorded_at_before" date range can be defined. Expected format: yyyy-MM-dd<br />[e.g., 2015-04-18]</param>
        /// <return>Returns the GetUserPurchaseHistoryWrapper response from the API call</return>
        public async Task<GetUserPurchaseHistoryWrapper> UserPurchasesGetPurchaseHistoryUnifiedAsync(
                string userId,
                int? storeId = null,
                bool? foodOnly = null,
                bool? upcOnly = null,
                bool? showProductDetails = true,
                bool? receiptsOnly = null,
                DateTime? upcResolvedAfter = null,
                DateTime? purchaseDateBefore = null,
                DateTime? purchaseDateAfter = null,
                DateTime? recordedAtBefore = null,
                DateTime? recordedAtAfter = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/purchases_product_based");

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
                { "show_product_details", (null != showProductDetails) ? showProductDetails : true },
                { "receipts_only", receiptsOnly },
                { "upc_resolved_after", upcResolvedAfter },
                { "purchase_date_before", purchaseDateBefore },
                { "purchase_date_after", purchaseDateAfter },
                { "recorded_at_before", recordedAtBefore },
                { "recorded_at_after", recordedAtAfter },
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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetUserPurchaseHistoryWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 