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
    public class ProductTimestamps : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private DateTime? upcResolvedAt;
        private DateTime? recordedAt;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("upc_resolved_at")]
        public DateTime? UpcResolvedAt 
        { 
            get 
            {
                return this.upcResolvedAt; 
            } 
            set 
            {
                this.upcResolvedAt = value;
                onPropertyChanged("UpcResolvedAt");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recorded_at")]
        public DateTime? RecordedAt 
        { 
            get 
            {
                return this.recordedAt; 
            } 
            set 
            {
                this.recordedAt = value;
                onPropertyChanged("RecordedAt");
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