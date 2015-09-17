/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;
using InformationMachineAPI.PCL.Controllers;

namespace InformationMachineAPI.PCL
{
    public partial class InformationMachineAPIClient
    {

        /// <summary>
        /// Singleton access to Lookup controller
        /// </summary>
        public LookupController Lookup
        {
            get
            {
                return LookupController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to Products controller
        /// </summary>
        public ProductsController Products
        {
            get
            {
                return ProductsController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserCarts controller
        /// </summary>
        public UserCartsController UserCarts
        {
            get
            {
                return UserCartsController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserManagement controller
        /// </summary>
        public UserManagementController UserManagement
        {
            get
            {
                return UserManagementController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserPurchases controller
        /// </summary>
        public UserPurchasesController UserPurchases
        {
            get
            {
                return UserPurchasesController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserScans controller
        /// </summary>
        public UserScansController UserScans
        {
            get
            {
                return UserScansController.Instance;
            }
        }

        /// <summary>
        /// Singleton access to UserStores controller
        /// </summary>
        public UserStoresController UserStores
        {
            get
            {
                return UserStoresController.Instance;
            }
        }

        /// <summary>
        /// Client constructor
        /// </summary>
        public InformationMachineAPIClient(string clientId, string clientSecret)
        {
            Configuration.ClientId = clientId;
            Configuration.ClientSecret = clientSecret;
        }
    }
}