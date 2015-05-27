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
    public class StoreInfo : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? id;
        private string name;
        private bool? restaurant;
        private bool? canScrape;

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
        [JsonProperty("restaurant")]
        public bool? Restaurant 
        { 
            get 
            {
                return this.restaurant; 
            } 
            set 
            {
                this.restaurant = value;
                onPropertyChanged("Restaurant");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("can_scrape")]
        public bool? CanScrape 
        { 
            get 
            {
                return this.canScrape; 
            } 
            set 
            {
                this.canScrape = value;
                onPropertyChanged("CanScrape");
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