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
    public class GetSingleUserResponse : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string email;
        private string zip;
        private string userId;
        private string ownerAppId;
        private string createdAt;

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
        [JsonProperty("owner_app_id")]
        public string OwnerAppId 
        { 
            get 
            {
                return this.ownerAppId; 
            } 
            set 
            {
                this.ownerAppId = value;
                onPropertyChanged("OwnerAppId");
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