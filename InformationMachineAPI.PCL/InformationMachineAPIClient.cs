/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;
using InformationMachineAPI.PCL.Controllers;
using InformationMachineAPI.PCL.Http.Client;

namespace InformationMachineAPI.PCL
{
    public partial class InformationMachineAPIClient
    {

        /// <summary>
        /// Singleton access to UserManagement controller
        /// </summary>
        public UserManagementController UserManagement
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.UserManagementController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserCarts controller
        /// </summary>
        public UserCartsController UserCarts
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.UserCartsController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to Products controller
        /// </summary>
        public ProductsController Products
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.ProductsController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to Lookup controller
        /// </summary>
        public LookupController Lookup
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.LookupController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserStores controller
        /// </summary>
        public UserStoresController UserStores
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.UserStoresController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserScans controller
        /// </summary>
        public UserScansController UserScans
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.UserScansController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserPurchases controller
        /// </summary>
        public UserPurchasesController UserPurchases
        {
            get
            {
                return InformationMachineAPI.PCL.Controllers.UserPurchasesController.Instance;
            }
        }

        /// <summary>
        /// The shared http client to use for all API calls
        /// </summary>
        public IHttpClient SharedHttpClient
        {
            get
            {
                return BaseController.ClientInstance;
            }
            set
            {
                BaseController.ClientInstance = value;
            }        
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public InformationMachineAPIClient() { }

        /// <summary>
        /// Client initialization constructor
        /// </summary>
        public InformationMachineAPIClient(string clientId, string clientSecret)
        {
            Configuration.ClientId = clientId;
            Configuration.ClientSecret = clientSecret;
        }
    }
}