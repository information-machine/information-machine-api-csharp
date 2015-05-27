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
    public class PriceData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private List<PriceInfo> prices;
        private int? productId;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("prices")]
        public List<PriceInfo> Prices 
        { 
            get 
            {
                return this.prices; 
            } 
            set 
            {
                this.prices = value;
                onPropertyChanged("Prices");
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