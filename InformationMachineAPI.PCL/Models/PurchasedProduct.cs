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
    public class PurchasedProduct : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? productId;
        private string date;
        private string store;
        private double? price;

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
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("date")]
        public string Date 
        { 
            get 
            {
                return this.date; 
            } 
            set 
            {
                this.date = value;
                onPropertyChanged("Date");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("store")]
        public string Store 
        { 
            get 
            {
                return this.store; 
            } 
            set 
            {
                this.store = value;
                onPropertyChanged("Store");
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