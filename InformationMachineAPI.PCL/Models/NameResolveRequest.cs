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
    public class NameResolveRequest : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private List<string> nameStoreList;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("name_store_list")]
        public List<string> NameStoreList 
        { 
            get 
            {
                return this.nameStoreList; 
            } 
            set 
            {
                this.nameStoreList = value;
                onPropertyChanged("NameStoreList");
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