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
    public class ProductIdentifiers : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private List<string> upcs;
        private List<string> plus;

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
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("plus")]
        public List<string> Plus 
        { 
            get 
            {
                return this.plus; 
            } 
            set 
            {
                this.plus = value;
                onPropertyChanged("Plus");
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