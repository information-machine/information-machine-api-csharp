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
    public class ConnectUserStoreRequest : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int storeId;
        private string username;
        private string password;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("store_id")]
        public int StoreId 
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
        [JsonProperty("username")]
        public string Username 
        { 
            get 
            {
                return this.username; 
            } 
            set 
            {
                this.username = value;
                onPropertyChanged("Username");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("password")]
        public string Password 
        { 
            get 
            {
                return this.password; 
            } 
            set 
            {
                this.password = value;
                onPropertyChanged("Password");
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