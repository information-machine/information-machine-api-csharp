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
    public class FdaResult : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private string country;
        private string city;
        private string reasonForRecall;
        private string address1;
        private string address2;
        private string codeInfo;
        private string productQuantity;
        private string centerClassificationDate;
        private string distributionPattern;
        private string state;
        private string productDescription;
        private string reportDate;
        private string classification;
        private object openfda;
        private string recallNumber;
        private string recallingFirm;
        private string initialFirmNotification;
        private string eventId;
        private string productType;
        private string moreCodeInfo;
        private string recallInitiationDate;
        private string postalCode;
        private string voluntaryMandated;
        private string status;
        private string terminationDate;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("country")]
        public string Country 
        { 
            get 
            {
                return this.country; 
            } 
            set 
            {
                this.country = value;
                onPropertyChanged("Country");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("city")]
        public string City 
        { 
            get 
            {
                return this.city; 
            } 
            set 
            {
                this.city = value;
                onPropertyChanged("City");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("reason_for_recall")]
        public string ReasonForRecall 
        { 
            get 
            {
                return this.reasonForRecall; 
            } 
            set 
            {
                this.reasonForRecall = value;
                onPropertyChanged("ReasonForRecall");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("address_1")]
        public string Address1 
        { 
            get 
            {
                return this.address1; 
            } 
            set 
            {
                this.address1 = value;
                onPropertyChanged("Address1");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("address_2")]
        public string Address2 
        { 
            get 
            {
                return this.address2; 
            } 
            set 
            {
                this.address2 = value;
                onPropertyChanged("Address2");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("code_info")]
        public string CodeInfo 
        { 
            get 
            {
                return this.codeInfo; 
            } 
            set 
            {
                this.codeInfo = value;
                onPropertyChanged("CodeInfo");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_quantity")]
        public string ProductQuantity 
        { 
            get 
            {
                return this.productQuantity; 
            } 
            set 
            {
                this.productQuantity = value;
                onPropertyChanged("ProductQuantity");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("center_classification_date")]
        public string CenterClassificationDate 
        { 
            get 
            {
                return this.centerClassificationDate; 
            } 
            set 
            {
                this.centerClassificationDate = value;
                onPropertyChanged("CenterClassificationDate");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("distribution_pattern")]
        public string DistributionPattern 
        { 
            get 
            {
                return this.distributionPattern; 
            } 
            set 
            {
                this.distributionPattern = value;
                onPropertyChanged("DistributionPattern");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("state")]
        public string State 
        { 
            get 
            {
                return this.state; 
            } 
            set 
            {
                this.state = value;
                onPropertyChanged("State");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_description")]
        public string ProductDescription 
        { 
            get 
            {
                return this.productDescription; 
            } 
            set 
            {
                this.productDescription = value;
                onPropertyChanged("ProductDescription");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("report_date")]
        public string ReportDate 
        { 
            get 
            {
                return this.reportDate; 
            } 
            set 
            {
                this.reportDate = value;
                onPropertyChanged("ReportDate");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("classification")]
        public string Classification 
        { 
            get 
            {
                return this.classification; 
            } 
            set 
            {
                this.classification = value;
                onPropertyChanged("Classification");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("openfda")]
        public object Openfda 
        { 
            get 
            {
                return this.openfda; 
            } 
            set 
            {
                this.openfda = value;
                onPropertyChanged("Openfda");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recall_number")]
        public string RecallNumber 
        { 
            get 
            {
                return this.recallNumber; 
            } 
            set 
            {
                this.recallNumber = value;
                onPropertyChanged("RecallNumber");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recalling_firm")]
        public string RecallingFirm 
        { 
            get 
            {
                return this.recallingFirm; 
            } 
            set 
            {
                this.recallingFirm = value;
                onPropertyChanged("RecallingFirm");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("initial_firm_notification")]
        public string InitialFirmNotification 
        { 
            get 
            {
                return this.initialFirmNotification; 
            } 
            set 
            {
                this.initialFirmNotification = value;
                onPropertyChanged("InitialFirmNotification");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId 
        { 
            get 
            {
                return this.eventId; 
            } 
            set 
            {
                this.eventId = value;
                onPropertyChanged("EventId");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("product_type")]
        public string ProductType 
        { 
            get 
            {
                return this.productType; 
            } 
            set 
            {
                this.productType = value;
                onPropertyChanged("ProductType");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("more_code_info")]
        public string MoreCodeInfo 
        { 
            get 
            {
                return this.moreCodeInfo; 
            } 
            set 
            {
                this.moreCodeInfo = value;
                onPropertyChanged("MoreCodeInfo");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recall_initiation_date")]
        public string RecallInitiationDate 
        { 
            get 
            {
                return this.recallInitiationDate; 
            } 
            set 
            {
                this.recallInitiationDate = value;
                onPropertyChanged("RecallInitiationDate");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("postal_code")]
        public string PostalCode 
        { 
            get 
            {
                return this.postalCode; 
            } 
            set 
            {
                this.postalCode = value;
                onPropertyChanged("PostalCode");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("voluntary_mandated")]
        public string VoluntaryMandated 
        { 
            get 
            {
                return this.voluntaryMandated; 
            } 
            set 
            {
                this.voluntaryMandated = value;
                onPropertyChanged("VoluntaryMandated");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("status")]
        public string Status 
        { 
            get 
            {
                return this.status; 
            } 
            set 
            {
                this.status = value;
                onPropertyChanged("Status");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("termination_date")]
        public string TerminationDate 
        { 
            get 
            {
                return this.terminationDate; 
            } 
            set 
            {
                this.terminationDate = value;
                onPropertyChanged("TerminationDate");
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