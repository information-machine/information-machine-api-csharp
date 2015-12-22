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
    public class NameUpcRecord : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string name;
        private string store;
        private string resolveStatus;
        private List<string> upcs;

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
        [JsonProperty("store")]
        public string Store 
        { 
            get 
            {
                return this.store; 
            } 
            set 
            {
                this.store = value;
                onPropertyChanged("Store");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("resolve_status")]
        public string ResolveStatus 
        { 
            get 
            {
                return this.resolveStatus; 
            } 
            set 
            {
                this.resolveStatus = value;
                onPropertyChanged("ResolveStatus");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("upcs")]
        public List<string> Upcs 
        { 
            get 
            {
                return this.upcs; 
            } 
            set 
            {
                this.upcs = value;
                onPropertyChanged("Upcs");
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