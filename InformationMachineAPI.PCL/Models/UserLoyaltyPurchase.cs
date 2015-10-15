/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InformationMachineAPI.PCL;

namespace InformationMachineAPI.PCL.Models
{
    public class UserLoyaltyPurchase : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private UserStore userStore;
        private List<LoyaltyPurchaseItemData> purchaseItems;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("user_store")]
        public UserStore UserStore 
        { 
            get 
            {
                return this.userStore; 
            } 
            set 
            {
                this.userStore = value;
                onPropertyChanged("UserStore");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("purchase_items")]
        public List<LoyaltyPurchaseItemData> PurchaseItems 
        { 
            get 
            {
                return this.purchaseItems; 
            } 
            set 
            {
                this.purchaseItems = value;
                onPropertyChanged("PurchaseItems");
            }
        }

        /// <summary>
        /// Property changed event for observer pattern
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises event when a property is changed
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        protected void onPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
} 