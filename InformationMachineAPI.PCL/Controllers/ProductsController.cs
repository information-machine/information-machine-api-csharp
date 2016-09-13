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
    public partial class ProductsController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static ProductsController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static ProductsController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new ProductsController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Get all food recalls for API owner.
        /// </summary>
        /// <return>Returns the GetFDARecallWrapper response from the API call</return>
        public GetFDARecallWrapper ProductsGetFoodRecalls()
        {
            Task<GetFDARecallWrapper> t = ProductsGetFoodRecallsAsync();
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get all food recalls for API owner.
        /// </summary>
        /// <return>Returns the GetFDARecallWrapper response from the API call</return>
        public async Task<GetFDARecallWrapper> ProductsGetFoodRecallsAsync()
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/food_recalls");

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
            if (_response.StatusCode == 404)
                throw new APIException(@"Not found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetFDARecallWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Search for product(s) by product name or UPC/EAN/ISBN
        /// </summary>
        /// <param name="name">Optional parameter: Product name (or part)</param>
        /// <param name="productIdentifier">Optional parameter: UPC/EAN/ISBN</param>
        /// <param name="page">Optional parameter: Example: </param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="requestData">Optional parameter: Additional request data sent by IM API customer. Expected format:"Key1:Value1;Key2:Value2"</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <param name="foodOnly">Optional parameter: List food products only</param>
        /// <return>Returns the GetProductsWrapper response from the API call</return>
        public GetProductsWrapper ProductsSearchProducts(
                string name = null,
                string productIdentifier = null,
                int? page = null,
                int? perPage = null,
                string requestData = null,
                bool? fullResp = null,
                bool? foodOnly = null)
        {
            Task<GetProductsWrapper> t = ProductsSearchProductsAsync(name, productIdentifier, page, perPage, requestData, fullResp, foodOnly);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Search for product(s) by product name or UPC/EAN/ISBN
        /// </summary>
        /// <param name="name">Optional parameter: Product name (or part)</param>
        /// <param name="productIdentifier">Optional parameter: UPC/EAN/ISBN</param>
        /// <param name="page">Optional parameter: Example: </param>
        /// <param name="perPage">Optional parameter: default:10, max:50</param>
        /// <param name="requestData">Optional parameter: Additional request data sent by IM API customer. Expected format:"Key1:Value1;Key2:Value2"</param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <param name="foodOnly">Optional parameter: List food products only</param>
        /// <return>Returns the GetProductsWrapper response from the API call</return>
        public async Task<GetProductsWrapper> ProductsSearchProductsAsync(
                string name = null,
                string productIdentifier = null,
                int? page = null,
                int? perPage = null,
                string requestData = null,
                bool? fullResp = null,
                bool? foodOnly = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/products");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "name", name },
                { "product_identifier", productIdentifier },
                { "page", page },
                { "per_page", perPage },
                { "request_data", requestData },
                { "full_resp", fullResp },
                { "food_only", foodOnly },
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
                throw new APIException(@"Not found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetProductsWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get product details by Informaton Machine Product ID
        /// </summary>
        /// <param name="productId">Required parameter: Example: </param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <return>Returns the GetProductWrapper response from the API call</return>
        public GetProductWrapper ProductsGetProduct(long productId, bool? fullResp = null)
        {
            Task<GetProductWrapper> t = ProductsGetProductAsync(productId, fullResp);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get product details by Informaton Machine Product ID
        /// </summary>
        /// <param name="productId">Required parameter: Example: </param>
        /// <param name="fullResp">Optional parameter: default:false (set true for response with nutrients)</param>
        /// <return>Returns the GetProductWrapper response from the API call</return>
        public async Task<GetProductWrapper> ProductsGetProductAsync(long productId, bool? fullResp = null)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v1/products/{product_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "product_id", productId }
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
                throw new APIException(@"Not found", _context);

            else if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetProductWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 