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
    public class UserStore : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private long id;
        private long supermarketId;
        private UserData user;
        private string storeName;
        private string username;
        private string credentialsStatus;
        private string scrapeStatus;
        private string type;
        private bool? accountLocked;
        private string accountLockCode;
        private string unlockUrl;
        private string oauthProvider;
        private string oauthAuthorizationUrl;
        private DateTime? createdAt;
        private DateTime? updatedAt;
        private double? totalSpent;

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
        [JsonProperty("supermarket_id")]
        public long SupermarketId 
        { 
            get 
            {
                return this.supermarketId; 
            } 
            set 
            {
                this.supermarketId = value;
                onPropertyChanged("SupermarketId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("user")]
        public UserData User 
        { 
            get 
            {
                return this.user; 
            } 
            set 
            {
                this.user = value;
                onPropertyChanged("User");
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
        [JsonProperty("credentials_status")]
        public string CredentialsStatus 
        { 
            get 
            {
                return this.credentialsStatus; 
            } 
            set 
            {
                this.credentialsStatus = value;
                onPropertyChanged("CredentialsStatus");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("scrape_status")]
        public string ScrapeStatus 
        { 
            get 
            {
                return this.scrapeStatus; 
            } 
            set 
            {
                this.scrapeStatus = value;
                onPropertyChanged("ScrapeStatus");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("type")]
        public string Type 
        { 
            get 
            {
                return this.type; 
            } 
            set 
            {
                this.type = value;
                onPropertyChanged("Type");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("account_locked")]
        public bool? AccountLocked 
        { 
            get 
            {
                return this.accountLocked; 
            } 
            set 
            {
                this.accountLocked = value;
                onPropertyChanged("AccountLocked");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("account_lock_code")]
        public string AccountLockCode 
        { 
            get 
            {
                return this.accountLockCode; 
            } 
            set 
            {
                this.accountLockCode = value;
                onPropertyChanged("AccountLockCode");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("unlock_url")]
        public string UnlockUrl 
        { 
            get 
            {
                return this.unlockUrl; 
            } 
            set 
            {
                this.unlockUrl = value;
                onPropertyChanged("UnlockUrl");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("oauth_provider")]
        public string OauthProvider 
        { 
            get 
            {
                return this.oauthProvider; 
            } 
            set 
            {
                this.oauthProvider = value;
                onPropertyChanged("OauthProvider");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("oauth_authorization_url")]
        public string OauthAuthorizationUrl 
        { 
            get 
            {
                return this.oauthAuthorizationUrl; 
            } 
            set 
            {
                this.oauthAuthorizationUrl = value;
                onPropertyChanged("OauthAuthorizationUrl");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt 
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
        public DateTime? UpdatedAt 
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
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("total_spent")]
        public double? TotalSpent 
        { 
            get 
            {
                return this.totalSpent; 
            } 
            set 
            {
                this.totalSpent = value;
                onPropertyChanged("TotalSpent");
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