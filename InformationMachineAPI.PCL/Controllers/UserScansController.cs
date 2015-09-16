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
    public partial class UserScansController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserScansController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        public static UserScansController Instance
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
        /// Upload a new product by barcode and associate it to a specified user.  Note: Execution might take up to 15 seconds, depending on whether barcode exists in database or IM service must gather data around uploaded barcode.  POST payload example: { "bar_code" : "021130126026", "bar_code_type" : "UPC-A" }
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: ID of user in your system</param>
        /// <return>Returns the UploadBarcodeWrapper response from the API call</return>
        public UploadBarcodeWrapper UserScansUploadBarcode(
                UploadBarcodeRequest payload,
                string userId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/barcode");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId }
                });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "client_id", Configuration.ClientId },
                    { "client_secret", Configuration.ClientSecret }
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

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<UploadBarcodeWrapper>(response.Body);
        }

        /// <summary>
        /// Upload a receipt with unique ID ("receipt_id") and associate it to a specified user using "user_id" parameter. Note: Uploaded receipt image should be Base 64 encoded. For testing purposes you can find our Base 64 encoded logo here: http://api.iamdata.co/images/base64/encoded_logo.txt
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the UploadReceiptWrapper response from the API call</return>
        public UploadReceiptWrapper UserScansUploadReceipt(
                UploadReceiptRequest payload,
                string userId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/v1/users/{user_id}/receipt");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId }
                });

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "client_id", Configuration.ClientId },
                    { "client_secret", Configuration.ClientSecret }
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

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<UploadReceiptWrapper>(response.Body);
        }

    }
} 