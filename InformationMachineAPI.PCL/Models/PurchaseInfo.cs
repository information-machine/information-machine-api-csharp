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
    public class PurchaseInfo : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private long invoiceId;
        private long storeId;
        private string storeName;
        private string harvestedName;
        private double? quantity;
        private double? price;
        private double? unitPrice;
        private DateTime? purchaseDate;
        private DateTime? recordedAt;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("invoice_id")]
        public long InvoiceId 
        { 
            get 
            {
                return this.invoiceId; 
            } 
            set 
            {
                this.invoiceId = value;
                onPropertyChanged("InvoiceId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("store_id")]
        public long StoreId 
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
        [JsonProperty("store_name")]
        public string StoreName 
        { 
            get 
            {
                return this.storeName; 
            } 
            set 
            {
                this.storeName = value;
                onPropertyChanged("StoreName");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("harvested_name")]
        public string HarvestedName 
        { 
            get 
            {
                return this.harvestedName; 
            } 
            set 
            {
                this.harvestedName = value;
                onPropertyChanged("HarvestedName");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("quantity")]
        public double? Quantity 
        { 
            get 
            {
                return this.quantity; 
            } 
            set 
            {
                this.quantity = value;
                onPropertyChanged("Quantity");
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
        [JsonProperty("unit_price")]
        public double? UnitPrice 
        { 
            get 
            {
                return this.unitPrice; 
            } 
            set 
            {
                this.unitPrice = value;
                onPropertyChanged("UnitPrice");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("purchase_date")]
        public DateTime? PurchaseDate 
        { 
            get 
            {
                return this.purchaseDate; 
            } 
            set 
            {
                this.purchaseDate = value;
                onPropertyChanged("PurchaseDate");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recorded_at")]
        public DateTime? RecordedAt 
        { 
            get 
            {
                return this.recordedAt; 
            } 
            set 
            {
                this.recordedAt = value;
                onPropertyChanged("RecordedAt");
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