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
    public class MetaPaged : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? page;
        private int? perPage;
        private int? totalCount;
        private string nextPage;
        private string lastPage;
        private int? maxNumberOfRequestsPerMinute;
        private int? remainingNumberOfRequest;
        private double? timeInEpochSecondTillReset;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("page")]
        public int? Page 
        { 
            get 
            {
                return this.page; 
            } 
            set 
            {
                this.page = value;
                onPropertyChanged("Page");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("per_page")]
        public int? PerPage 
        { 
            get 
            {
                return this.perPage; 
            } 
            set 
            {
                this.perPage = value;
                onPropertyChanged("PerPage");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("total_count")]
        public int? TotalCount 
        { 
            get 
            {
                return this.totalCount; 
            } 
            set 
            {
                this.totalCount = value;
                onPropertyChanged("TotalCount");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("next_page")]
        public string NextPage 
        { 
            get 
            {
                return this.nextPage; 
            } 
            set 
            {
                this.nextPage = value;
                onPropertyChanged("NextPage");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("last_page")]
        public string LastPage 
        { 
            get 
            {
                return this.lastPage; 
            } 
            set 
            {
                this.lastPage = value;
                onPropertyChanged("LastPage");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("max_number_of_requests_per_minute")]
        public int? MaxNumberOfRequestsPerMinute 
        { 
            get 
            {
                return this.maxNumberOfRequestsPerMinute; 
            } 
            set 
            {
                this.maxNumberOfRequestsPerMinute = value;
                onPropertyChanged("MaxNumberOfRequestsPerMinute");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("remaining_number_of_request")]
        public int? RemainingNumberOfRequest 
        { 
            get 
            {
                return this.remainingNumberOfRequest; 
            } 
            set 
            {
                this.remainingNumberOfRequest = value;
                onPropertyChanged("RemainingNumberOfRequest");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("time_in_epoch_second_till_reset")]
        public double? TimeInEpochSecondTillReset 
        { 
            get 
            {
                return this.timeInEpochSecondTillReset; 
            } 
            set 
            {
                this.timeInEpochSecondTillReset = value;
                onPropertyChanged("TimeInEpochSecondTillReset");
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