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
    public class NutrientInfo : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private double? recommendedDailyValue;
        private string preferredUnitOfMeasurement;
        private string unitOfMeasurement;
        private string description;
        private string name;
        private int? id;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recommended_daily_value")]
        public double? RecommendedDailyValue 
        { 
            get 
            {
                return this.recommendedDailyValue; 
            } 
            set 
            {
                this.recommendedDailyValue = value;
                onPropertyChanged("RecommendedDailyValue");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("preferred_unit_of_measurement")]
        public string PreferredUnitOfMeasurement 
        { 
            get 
            {
                return this.preferredUnitOfMeasurement; 
            } 
            set 
            {
                this.preferredUnitOfMeasurement = value;
                onPropertyChanged("PreferredUnitOfMeasurement");
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
        [JsonProperty("description")]
        public string Description 
        { 
            get 
            {
                return this.description; 
            } 
            set 
            {
                this.description = value;
                onPropertyChanged("Description");
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