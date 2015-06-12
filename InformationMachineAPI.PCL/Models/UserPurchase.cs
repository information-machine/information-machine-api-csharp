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
    public class UserPurchase : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private UserStore userStore;
        private List<PurchaseItemData> purchaseItems;
        private int? id;
        private string date;
        private double? total;
        private double? totalWithoutTax;
        private double? tax;
        private string orderNumber;
        private string receiptId;
        private string receiptImageUrl;

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
        public List<PurchaseItemData> PurchaseItems 
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
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("id")]
        public int? Id 
        { 
            get 
            {
                return this.id; 
            } 
            set 
            {
                this.id = value;
                onPropertyChanged("Id");
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
        [JsonProperty("total")]
        public double? Total 
        { 
            get 
            {
                return this.total; 
            } 
            set 
            {
                this.total = value;
                onPropertyChanged("Total");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("total_without_tax")]
        public double? TotalWithoutTax 
        { 
            get 
            {
                return this.totalWithoutTax; 
            } 
            set 
            {
                this.totalWithoutTax = value;
                onPropertyChanged("TotalWithoutTax");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("tax")]
        public double? Tax 
        { 
            get 
            {
                return this.tax; 
            } 
            set 
            {
                this.tax = value;
                onPropertyChanged("Tax");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("order_number")]
        public string OrderNumber 
        { 
            get 
            {
                return this.orderNumber; 
            } 
            set 
            {
                this.orderNumber = value;
                onPropertyChanged("OrderNumber");
            }
        }

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
        [JsonProperty("receipt_image_url")]
        public string ReceiptImageUrl 
        { 
            get 
            {
                return this.receiptImageUrl; 
            } 
            set 
            {
                this.receiptImageUrl = value;
                onPropertyChanged("ReceiptImageUrl");
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