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
    public class ProductData : INotifyPropertyChanged 
    {
        // These fields hold the values for the public properties.
        private List<NutrientData> nutrients;
        private List<string> recipes;
        private List<string> plus;
        private int? visibilityCount;
        private double? score;
        private string amazonLink;
        private string manufacturer;
        private int? ingredientsCount;
        private string largeImage;
        private string smallImage;
        private double? servingSizeInGrams;
        private string servingSizeUnit;
        private string servingsPerContainer;
        private string servingSize;
        private string ingredients;
        private string weight;
        private string description;
        private string brand;
        private string upc;
        private List<string> tags;
        private string category;
        private int? categoryId;
        private string name;
        private int? id;

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("nutrients")]
        public List<NutrientData> Nutrients 
        { 
            get 
            {
                return this.nutrients; 
            } 
            set 
            {
                this.nutrients = value;
                onPropertyChanged("Nutrients");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("recipes")]
        public List<string> Recipes 
        { 
            get 
            {
                return this.recipes; 
            } 
            set 
            {
                this.recipes = value;
                onPropertyChanged("Recipes");
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
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("visibility_count")]
        public int? VisibilityCount 
        { 
            get 
            {
                return this.visibilityCount; 
            } 
            set 
            {
                this.visibilityCount = value;
                onPropertyChanged("VisibilityCount");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("score")]
        public double? Score 
        { 
            get 
            {
                return this.score; 
            } 
            set 
            {
                this.score = value;
                onPropertyChanged("Score");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("amazon_link")]
        public string AmazonLink 
        { 
            get 
            {
                return this.amazonLink; 
            } 
            set 
            {
                this.amazonLink = value;
                onPropertyChanged("AmazonLink");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("manufacturer")]
        public string Manufacturer 
        { 
            get 
            {
                return this.manufacturer; 
            } 
            set 
            {
                this.manufacturer = value;
                onPropertyChanged("Manufacturer");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("ingredients_count")]
        public int? IngredientsCount 
        { 
            get 
            {
                return this.ingredientsCount; 
            } 
            set 
            {
                this.ingredientsCount = value;
                onPropertyChanged("IngredientsCount");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("large_image")]
        public string LargeImage 
        { 
            get 
            {
                return this.largeImage; 
            } 
            set 
            {
                this.largeImage = value;
                onPropertyChanged("LargeImage");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("small_image")]
        public string SmallImage 
        { 
            get 
            {
                return this.smallImage; 
            } 
            set 
            {
                this.smallImage = value;
                onPropertyChanged("SmallImage");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("serving_size_in_grams")]
        public double? ServingSizeInGrams 
        { 
            get 
            {
                return this.servingSizeInGrams; 
            } 
            set 
            {
                this.servingSizeInGrams = value;
                onPropertyChanged("ServingSizeInGrams");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("serving_size_unit")]
        public string ServingSizeUnit 
        { 
            get 
            {
                return this.servingSizeUnit; 
            } 
            set 
            {
                this.servingSizeUnit = value;
                onPropertyChanged("ServingSizeUnit");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("servings_per_container")]
        public string ServingsPerContainer 
        { 
            get 
            {
                return this.servingsPerContainer; 
            } 
            set 
            {
                this.servingsPerContainer = value;
                onPropertyChanged("ServingsPerContainer");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("serving_size")]
        public string ServingSize 
        { 
            get 
            {
                return this.servingSize; 
            } 
            set 
            {
                this.servingSize = value;
                onPropertyChanged("ServingSize");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("ingredients")]
        public string Ingredients 
        { 
            get 
            {
                return this.ingredients; 
            } 
            set 
            {
                this.ingredients = value;
                onPropertyChanged("Ingredients");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("weight")]
        public string Weight 
        { 
            get 
            {
                return this.weight; 
            } 
            set 
            {
                this.weight = value;
                onPropertyChanged("Weight");
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
        [JsonProperty("brand")]
        public string Brand 
        { 
            get 
            {
                return this.brand; 
            } 
            set 
            {
                this.brand = value;
                onPropertyChanged("Brand");
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
        [JsonProperty("tags")]
        public List<string> Tags 
        { 
            get 
            {
                return this.tags; 
            } 
            set 
            {
                this.tags = value;
                onPropertyChanged("Tags");
            }
        }

        /// <summary>
        /// TODO: Write general description for this method
        /// </summary>
        [JsonProperty("category")]
        public string Category 
        { 
            get 
            {
                return this.category; 
            } 
            set 
            {
                this.category = value;
                onPropertyChanged("Category");
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