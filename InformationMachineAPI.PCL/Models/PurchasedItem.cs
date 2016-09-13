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
    public class PurchasedItem : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private long productId;
        private string name;
        private List<PurchaseInfo> purchaseHistory;
        private ProductTimestamps productTimestamps;
        private ProductIdentifiers productIdentifiers;
        private ProductData productDetails;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_id")]
        public long ProductId 
        { 
            get 
            {
                return this.productId; 
            } 
            set 
            {
                this.productId = value;
                onPropertyChanged("ProductId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("name")]
        public string Name 
        { 
            get 
            {
                return this.name; 
            } 
            set 
            {
                this.name = value;
                onPropertyChanged("Name");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("purchase_history")]
        public List<PurchaseInfo> PurchaseHistory 
        { 
            get 
            {
                return this.purchaseHistory; 
            } 
            set 
            {
                this.purchaseHistory = value;
                onPropertyChanged("PurchaseHistory");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_timestamps")]
        public ProductTimestamps ProductTimestamps 
        { 
            get 
            {
                return this.productTimestamps; 
            } 
            set 
            {
                this.productTimestamps = value;
                onPropertyChanged("ProductTimestamps");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_identifiers")]
        public ProductIdentifiers ProductIdentifiers 
        { 
            get 
            {
                return this.productIdentifiers; 
            } 
            set 
            {
                this.productIdentifiers = value;
                onPropertyChanged("ProductIdentifiers");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_details")]
        public ProductData ProductDetails 
        { 
            get 
            {
                return this.productDetails; 
            } 
            set 
            {
                this.productDetails = value;
                onPropertyChanged("ProductDetails");
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