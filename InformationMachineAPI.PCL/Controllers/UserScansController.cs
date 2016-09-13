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
    public partial class UserScansController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserScansController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static UserScansController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new UserScansController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Upload a new product by barcode
        /// </summary>
        /// <param name="payload">Required parameter: POST payload example: { "bar_code" : "021130126026", "bar_code_type" : "UPC-A" }</param>
        /// <param name="userId">Required parameter: ID of user in your system</param>
        /// <return>Returns the UploadBarcodeWrapper response from the API call</return>
        public UploadBarcodeWrapper UserScansUploadBarcode(UploadBarcodeRequest payload, string userId)
        {
            Task<UploadBarcodeWrapper> t = UserScansUploadBarcodeAsync(payload, userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Upload a new product by barcode
        /// </summary>
        /// <param name="payload">Required parameter: POST payload example: { "bar_code" : "021130126026", "bar_code_type" : "UPC-A" }</param>
        /// <param name="userId">Required parameter: ID of user in your system</param>
        /// <return>Returns the UploadBarcodeWrapper response from the API call</return>
        public async Task<UploadBarcodeWrapper> UserScansUploadBarcodeAsync(UploadBarcodeRequest payload, string userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/barcode");

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

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<UploadBarcodeWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get status of uploaded receipt
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="receiptId">Required parameter: Example: </param>
        /// <return>Returns the UploadReceiptStatusWrapper response from the API call</return>
        public UploadReceiptStatusWrapper UserScansGetReceiptStatus(string userId, string receiptId)
        {
            Task<UploadReceiptStatusWrapper> t = UserScansGetReceiptStatusAsync(userId, receiptId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get status of uploaded receipt
        /// </summary>
        /// <param name="userId">Required parameter: Example: </param>
        /// <param name="receiptId">Required parameter: Example: </param>
        /// <return>Returns the UploadReceiptStatusWrapper response from the API call</return>
        public async Task<UploadReceiptStatusWrapper> UserScansGetReceiptStatusAsync(string userId, string receiptId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/receipt/{receipt_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "receipt_id", receiptId }
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
            if (_response.StatusCode == 400)
                throw new APIException(@"Bad request", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<UploadReceiptStatusWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Upload new product(s) by receipt
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the UploadReceiptWrapper response from the API call</return>
        public UploadReceiptWrapper UserScansUploadReceipt(UploadReceiptRequest payload, string userId)
        {
            Task<UploadReceiptWrapper> t = UserScansUploadReceiptAsync(payload, userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Upload new product(s) by receipt
        /// </summary>
        /// <param name="payload">Required parameter: Example: </param>
        /// <param name="userId">Required parameter: Example: </param>
        /// <return>Returns the UploadReceiptWrapper response from the API call</return>
        public async Task<UploadReceiptWrapper> UserScansUploadReceiptAsync(UploadReceiptRequest payload, string userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/users/{user_id}/receipt");

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

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            else if (_response.StatusCode == 500)
                throw new APIException(@"Internal Server Error", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<UploadReceiptWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 