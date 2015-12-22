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
    public class GetSingleStoresWrapper : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private UserStore result;
        private MetaBase meta;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("result")]
        public UserStore Result 
        { 
            get 
            {
                return this.result; 
            } 
            set 
            {
                this.result = value;
                onPropertyChanged("Result");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("meta")]
        public MetaBase Meta 
        { 
            get 
            {
                return this.meta; 
            } 
            set 
            {
                this.meta = value;
                onPropertyChanged("Meta");
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