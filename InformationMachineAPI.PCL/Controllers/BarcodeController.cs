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
    public class BarcodeController
    {
 

        //private fields for configuration

        //Id of your app 
        private string clientId;

        //Secret key which authorizes you to use this API 
        private string clientSecret;

        /// <summary>
        /// Constructor with authentication and configuration parameters
        /// </summary>
        public BarcodeController(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Upload a new product by barcode and associate it to a specified user.  Note: Execution might take up to 15 seconds, depending on whether barcode exists in database or IM service must gather data around uploaded barcode.  POST payload example: { "bar_code" : "021130126026", "bar_code_type" : "UPC-A" }
        /// </summary>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <param name="userId">Required parameter: ID of user in your system</param>
        /// <return>Returns the UploadBarcodeWrapper response from the API call</return>
        public UploadBarcodeWrapper BarcodeUploadBarcode(
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

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<UploadBarcodeWrapper>(response.Body);
        }

    }
} 