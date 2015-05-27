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
    public class PriceInfo : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string storeIdentifier;
        private double? price;
        private int? storeId;
        private int? productId;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("store_identifier")]
        public string StoreIdentifier 
        { 
            get 
            {
                return this.storeIdentifier; 
            } 
            set 
            {
                this.storeIdentifier = value;
                onPropertyChanged("StoreIdentifier");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("price")]
        public double? Price 
        { 
            get 
            {
                return this.price; 
            } 
            set 
            {
                this.price = value;
                onPropertyChanged("Price");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("store_id")]
        public int? StoreId 
        { 
            get 
            {
                return this.storeId; 
            } 
            set 
            {
                this.storeId = value;
                onPropertyChanged("StoreId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_id")]
        public int? ProductId 
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