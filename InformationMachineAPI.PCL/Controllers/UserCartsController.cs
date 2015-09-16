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
    public partial class UserCartsController
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
        /// Get all carts (including items in carts) associated with a specified user ID.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <return>Returns the GetCartsWrapper response from the API call</return>
        public GetCartsWrapper UserCartsGetCarts(
                string userId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts");

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
            HttpRequest request = Unirest.get(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetCartsWrapper>(response.Body);
        }

        /// <summary>
        /// IM API will generate Cart ID and return it in the response
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the AddCartWrapper response from the API call</return>
        public AddCartWrapper UserCartsCreateCart(
                string userId,
                AddCartRequest payload)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts");

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
                throw new APIException(@"Bad Request", 400);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<AddCartWrapper>(response.Body);
        }

        /// <summary>
        /// Get detailed information on a single user cart by specifying User ID and Cart ID. Cart items are included in response.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID if a cart</param>
        /// <return>Returns the GetCartWrapper response from the API call</return>
        public GetCartWrapper UserCartsGetCart(
                string userId,
                string cartId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "cart_id", cartId }
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
            HttpRequest request = Unirest.get(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<GetCartWrapper>(response.Body);
        }

        /// <summary>
        /// Add item/product to a cart, must specify product UPC and Cart ID.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID if a cart</param>
        /// <param name="payload">Required parameter: TODO: type parameter description here</param>
        /// <return>Returns the AddCartItemWrapper response from the API call</return>
        public AddCartItemWrapper UserCartsAddCartItem(
                string userId,
                string cartId,
                AddCartItemRequest payload)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "cart_id", cartId }
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
                throw new APIException(@"Bad Request", 400);

            else if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if (response.Code == 500)
                throw new APIException(@"Internal Server Error", 500);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<AddCartItemWrapper>(response.Body);
        }

        /// <summary>
        /// Use specified Cart ID to delete cart and all associated items in specified cart.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID if a cart</param>
        /// <return>Returns the DeleteCartWrapper response from the API call</return>
        public DeleteCartWrapper UserCartsDeleteCart(
                string userId,
                string cartId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts/{cart_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "cart_id", cartId }
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
            HttpRequest request = Unirest.delete(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<DeleteCartWrapper>(response.Body);
        }

        /// <summary>
        /// Remove item/product from a cart, must specify Cart and Cart Item ID.
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID if a cart</param>
        /// <param name="cartItemId">Required parameter: ID if a cart item</param>
        /// <return>Returns the DeleteCartItemWrapper response from the API call</return>
        public DeleteCartItemWrapper UserCartsRemoveCartItem(
                string userId,
                string cartId,
                string cartItemId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts/{cart_id}/items/{cart_item_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "cart_id", cartId },
                    { "cart_item_id", cartItemId }
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
            HttpRequest request = Unirest.delete(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<DeleteCartItemWrapper>(response.Body);
        }

        /// <summary>
        /// TODO: type endpoint description here
        /// </summary>
        /// <param name="userId">Required parameter: ID of a user</param>
        /// <param name="cartId">Required parameter: ID if a cart</param>
        /// <param name="storeId">Required parameter: ID if a store (check "Lookup" section, "v1/stores" endpoint)</param>
        /// <return>Returns the ExecuteCartWrapper response from the API call</return>
        public ExecuteCartWrapper UserCartsExecuteCart(
                string userId,
                string cartId,
                int storeId)
        {
            //the base uri for api requests
            string baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/users/{user_id}/carts/{cart_id}/stores/{store_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "cart_id", cartId },
                    { "store_id", storeId }
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
            HttpRequest request = Unirest.get(queryUrl)
                //append request with appropriate headers and parameters
                .header("user-agent", "IAMDATA V1")
                .header("accept", "application/json");

            //invoke request and get response
            HttpResponse<String> response = request.asString();

            //Error handling using HTTP status codes
            if (response.Code == 401)
                throw new APIException(@"Unauthorized", 401);

            else if (response.Code == 404)
                throw new APIException(@"Not Found", 404);

            else if (response.Code == 422)
                throw new APIException(@"Unprocessable Entity", 422);

            else if ((response.Code < 200) || (response.Code > 206)) //[200,206] = HTTP OK
                throw new APIException(@"HTTP Response Not OK", response.Code);

            return APIHelper.JsonDeserialize<ExecuteCartWrapper>(response.Body);
        }

    }
} 