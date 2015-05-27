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
    public class UploadBarcodeResponse : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string barCode;
        private string barCodeType;
        private ProductData apiProduct;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("bar_code")]
        public string BarCode 
        { 
            get 
            {
                return this.barCode; 
            } 
            set 
            {
                this.barCode = value;
                onPropertyChanged("BarCode");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("bar_code_type")]
        public string BarCodeType 
        { 
            get 
            {
                return this.barCodeType; 
            } 
            set 
            {
                this.barCodeType = value;
                onPropertyChanged("BarCodeType");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("apiProduct")]
        public ProductData ApiProduct 
        { 
            get 
            {
                return this.apiProduct; 
            } 
            set 
            {
                this.apiProduct = value;
                onPropertyChanged("ApiProduct");
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