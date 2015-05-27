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
    public class UploadReceiptRequest : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string receiptId;
        private string image;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("receipt_id")]
        public string ReceiptId 
        { 
            get 
            {
                return this.receiptId; 
            } 
            set 
            {
                this.receiptId = value;
                onPropertyChanged("ReceiptId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("image")]
        public string Image 
        { 
            get 
            {
                return this.image; 
            } 
            set 
            {
                this.image = value;
                onPropertyChanged("Image");
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