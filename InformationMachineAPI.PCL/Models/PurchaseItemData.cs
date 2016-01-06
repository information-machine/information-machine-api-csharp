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
    public class PurchaseItemData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? id;
        private int? purchaseId;
        private string name;
        private double? quantity;
        private double? price;
        private double? discountedPrice;
        private string unitOfMeasurement;
        private string upc;
        private string upcResolvedAt;
        private ProductData product;

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
        [JsonProperty("purchase_id")]
        public int? PurchaseId 
        { 
            get 
            {
                return this.purchaseId; 
            } 
            set 
            {
                this.purchaseId = value;
                onPropertyChanged("PurchaseId");
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
        [JsonProperty("discounted_price")]
        public double? DiscountedPrice 
        { 
            get 
            {
                return this.discountedPrice; 
            } 
            set 
            {
                this.discountedPrice = value;
                onPropertyChanged("DiscountedPrice");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("unit_of_measurement")]
        public string UnitOfMeasurement 
        { 
            get 
            {
                return this.unitOfMeasurement; 
            } 
            set 
            {
                this.unitOfMeasurement = value;
                onPropertyChanged("UnitOfMeasurement");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("upc")]
        public string Upc 
        { 
            get 
            {
                return this.upc; 
            } 
            set 
            {
                this.upc = value;
                onPropertyChanged("Upc");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("upc_resolved_at")]
        public string UpcResolvedAt 
        { 
            get 
            {
                return this.upcResolvedAt; 
            } 
            set 
            {
                this.upcResolvedAt = value;
                onPropertyChanged("UpcResolvedAt");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product")]
        public ProductData Product 
        { 
            get 
            {
                return this.product; 
            } 
            set 
            {
                this.product = value;
                onPropertyChanged("Product");
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