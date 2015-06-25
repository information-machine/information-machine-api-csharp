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
    public class Cart : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string cartId;
        private string cartName;
        private List<CartItem> cartItems;
        private string createdAt;
        private string updatedAt;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("cart_id")]
        public string CartId 
        { 
            get 
            {
                return this.cartId; 
            } 
            set 
            {
                this.cartId = value;
                onPropertyChanged("CartId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("cart_name")]
        public string CartName 
        { 
            get 
            {
                return this.cartName; 
            } 
            set 
            {
                this.cartName = value;
                onPropertyChanged("CartName");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("cart_items")]
        public List<CartItem> CartItems 
        { 
            get 
            {
                return this.cartItems; 
            } 
            set 
            {
                this.cartItems = value;
                onPropertyChanged("CartItems");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt 
        { 
            get 
            {
                return this.createdAt; 
            } 
            set 
            {
                this.createdAt = value;
                onPropertyChanged("CreatedAt");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt 
        { 
            get 
            {
                return this.updatedAt; 
            } 
            set 
            {
                this.updatedAt = value;
                onPropertyChanged("UpdatedAt");
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