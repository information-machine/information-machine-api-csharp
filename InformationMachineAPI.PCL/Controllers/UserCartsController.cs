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
    public partial class UserCartsController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static UserCartsController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static UserCartsController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new UserCartsController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Get all cart(s) for a user
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <return>Returns the GetCartsWrapper response from the API call</return>
        public GetCartsWrapper UserCartsGetCarts(string userId)
        {
            Task<GetCartsWrapper> t = UserCartsGetCartsAsync(userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get all cart(s) for a user
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <return>Returns the GetCartsWrapper response from the API call</return>
        public async Task<GetCartsWrapper> UserCartsGetCartsAsync(string userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts");

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
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetCartsWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Generate URL for accessing cart in a specified store.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="storeId">Required parameter: ID of a store (check "Lookup" section, "v1/stores" endpoint)</param>
        /// <return>Returns the ExecuteCartWrapper response from the API call</return>
        public ExecuteCartWrapper UserCartsExecuteCart(string userId, Guid cartId, int storeId)
        {
            Task<ExecuteCartWrapper> t = UserCartsExecuteCartAsync(userId, cartId, storeId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Generate URL for accessing cart in a specified store.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="storeId">Required parameter: ID of a store (check "Lookup" section, "v1/stores" endpoint)</param>
        /// <return>Returns the ExecuteCartWrapper response from the API call</return>
        public async Task<ExecuteCartWrapper> UserCartsExecuteCartAsync(string userId, Guid cartId, int storeId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts/{cart_id}/stores/{store_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "cart_id", cartId },
                { "store_id", storeId }
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
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<ExecuteCartWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Remove item/product from a cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="cartItemId">Required parameter: ID of a cart item</param>
        /// <return>Returns the DeleteCartItemWrapper response from the API call</return>
        public DeleteCartItemWrapper UserCartsRemoveCartItem(string userId, Guid cartId, Guid cartItemId)
        {
            Task<DeleteCartItemWrapper> t = UserCartsRemoveCartItemAsync(userId, cartId, cartItemId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Remove item/product from a cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="cartItemId">Required parameter: ID of a cart item</param>
        /// <return>Returns the DeleteCartItemWrapper response from the API call</return>
        public async Task<DeleteCartItemWrapper> UserCartsRemoveCartItemAsync(string userId, Guid cartId, Guid cartItemId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts/{cart_id}/items/{cart_item_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "cart_id", cartId },
                { "cart_item_id", cartItemId }
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
            HttpRequest _request = ClientInstance.Delete(_queryUrl, _headers, null);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<DeleteCartItemWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Delete cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <return>Returns the DeleteCartWrapper response from the API call</return>
        public DeleteCartWrapper UserCartsDeleteCart(string userId, Guid cartId)
        {
            Task<DeleteCartWrapper> t = UserCartsDeleteCartAsync(userId, cartId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Delete cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <return>Returns the DeleteCartWrapper response from the API call</return>
        public async Task<DeleteCartWrapper> UserCartsDeleteCartAsync(string userId, Guid cartId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "cart_id", cartId }
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
            HttpRequest _request = ClientInstance.Delete(_queryUrl, _headers, null);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<DeleteCartWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Add item/product to a cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="payload">Required parameter: Post Payload example: { "upc" : "021130126026", "quantity" : 1 }</param>
        /// <return>Returns the AddCartItemWrapper response from the API call</return>
        public AddCartItemWrapper UserCartsAddCartItem(string userId, Guid cartId, AddCartItemRequest payload)
        {
            Task<AddCartItemWrapper> t = UserCartsAddCartItemAsync(userId, cartId, payload);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Add item/product to a cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <param name="payload">Required parameter: Post Payload example: { "upc" : "021130126026", "quantity" : 1 }</param>
        /// <return>Returns the AddCartItemWrapper response from the API call</return>
        public async Task<AddCartItemWrapper> UserCartsAddCartItemAsync(string userId, Guid cartId, AddCartItemRequest payload)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "cart_id", cartId }
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
                throw new APIException(@"Bad Request", _context);

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
                return APIHelper.JsonDeserialize<AddCartItemWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Get single user cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <return>Returns the GetCartWrapper response from the API call</return>
        public GetCartWrapper UserCartsGetCart(string userId, Guid cartId)
        {
            Task<GetCartWrapper> t = UserCartsGetCartAsync(userId, cartId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Get single user cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID of a cart</param>
        /// <return>Returns the GetCartWrapper response from the API call</return>
        public async Task<GetCartWrapper> UserCartsGetCartAsync(string userId, Guid cartId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "cart_id", cartId }
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
            if (_response.StatusCode == 401)
                throw new APIException(@"Unauthorized", _context);

            else if (_response.StatusCode == 404)
                throw new APIException(@"Not Found", _context);

            else if (_response.StatusCode == 422)
                throw new APIException(@"Unprocessable Entity", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<GetCartWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Create a new cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="payload">Required parameter: Example: </param>
        /// <return>Returns the AddCartWrapper response from the API call</return>
        public AddCartWrapper UserCartsCreateCart(string userId, AddCartRequest payload)
        {
            Task<AddCartWrapper> t = UserCartsCreateCartAsync(userId, payload);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// Create a new cart
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="payload">Required parameter: Example: </param>
        /// <return>Returns the AddCartWrapper response from the API call</return>
        public async Task<AddCartWrapper> UserCartsCreateCartAsync(string userId, AddCartRequest payload)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}/carts");

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
                throw new APIException(@"Bad Request", _context);

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
                return APIHelper.JsonDeserialize<AddCartWrapper>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 