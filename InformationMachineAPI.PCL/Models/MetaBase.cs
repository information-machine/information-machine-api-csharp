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
    public class MetaBase : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? maxNumberOfRequestsPerDay;
        private int? remainingNumberOfRequest;
        private double? timeInEpochSecondTillReset;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("max_number_of_requests_per_day")]
        public int? MaxNumberOfRequestsPerDay 
        { 
            get 
            {
                return this.maxNumberOfRequestsPerDay; 
            } 
            set 
            {
                this.maxNumberOfRequestsPerDay = value;
                onPropertyChanged("MaxNumberOfRequestsPerDay");
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