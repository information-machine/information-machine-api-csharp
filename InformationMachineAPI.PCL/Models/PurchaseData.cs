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
    public class PurchaseData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private List<PurchasedItem> purchasedItems;
        private List<InvoiceData> invoices;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("purchased_items")]
        public List<PurchasedItem> PurchasedItems 
        { 
            get 
            {
                return this.purchasedItems; 
            } 
            set 
            {
                this.purchasedItems = value;
                onPropertyChanged("PurchasedItems");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("invoices")]
        public List<InvoiceData> Invoices 
        { 
            get 
            {
                return this.invoices; 
            } 
            set 
            {
                this.invoices = value;
                onPropertyChanged("Invoices");
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