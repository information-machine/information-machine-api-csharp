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
    public class FdaRecallData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private FdaResult recall;
        private ProductData product;
        private List<RecalledProductPurchaseData> recallPurchases;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recall")]
        public FdaResult Recall 
        { 
            get 
            {
                return this.recall; 
            } 
            set 
            {
                this.recall = value;
                onPropertyChanged("Recall");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product")]
        public ProductData Product 
        { 
            get 
            {
                return this.product; 
            } 
            set 
            {
                this.product = value;
                onPropertyChanged("Product");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recall_purchases")]
        public List<RecalledProductPurchaseData> RecallPurchases 
        { 
            get 
            {
                return this.recallPurchases; 
            } 
            set 
            {
                this.recallPurchases = value;
                onPropertyChanged("RecallPurchases");
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