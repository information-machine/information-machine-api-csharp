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
    public class PurchaseItemProduct : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private int? id;
        private string name;
        private string upc;
        private int? categoryId;
        private List<string> plus;

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
        [JsonProperty("upc")]
        public string Upc 
        { 
            get 
            {
                return this.upc; 
            } 
            set 
            {
                this.upc = value;
                onPropertyChanged("Upc");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("category_id")]
        public int? CategoryId 
        { 
            get 
            {
                return this.categoryId; 
            } 
            set 
            {
                this.categoryId = value;
                onPropertyChanged("CategoryId");
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