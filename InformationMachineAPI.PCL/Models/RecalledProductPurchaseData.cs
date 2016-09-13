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
    public class RecalledProductPurchaseData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private long productId;
        private long id;
        private long storeId;
        private string storeName;
        private DateTime? orderPurchaseDate;
        private DateTime? orderRecordedAt;
        private string orderNumber;
        private string email;
        private string zip;
        private string userId;
        private string clientId;
        private DateTime? userCreatedAt;

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
        [JsonProperty("id")]
        public long Id 
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
        [JsonProperty("order_purchase_date")]
        public DateTime? OrderPurchaseDate 
        { 
            get 
            {
                return this.orderPurchaseDate; 
            } 
            set 
            {
                this.orderPurchaseDate = value;
                onPropertyChanged("OrderPurchaseDate");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("order_recorded_at")]
        public DateTime? OrderRecordedAt 
        { 
            get 
            {
                return this.orderRecordedAt; 
            } 
            set 
            {
                this.orderRecordedAt = value;
                onPropertyChanged("OrderRecordedAt");
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
        [JsonProperty("email")]
        public string Email 
        { 
            get 
            {
                return this.email; 
            } 
            set 
            {
                this.email = value;
                onPropertyChanged("Email");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("zip")]
        public string Zip 
        { 
            get 
            {
                return this.zip; 
            } 
            set 
            {
                this.zip = value;
                onPropertyChanged("Zip");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId 
        { 
            get 
            {
                return this.userId; 
            } 
            set 
            {
                this.userId = value;
                onPropertyChanged("UserId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId 
        { 
            get 
            {
                return this.clientId; 
            } 
            set 
            {
                this.clientId = value;
                onPropertyChanged("ClientId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("user_created_at")]
        public DateTime? UserCreatedAt 
        { 
            get 
            {
                return this.userCreatedAt; 
            } 
            set 
            {
                this.userCreatedAt = value;
                onPropertyChanged("UserCreatedAt");
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