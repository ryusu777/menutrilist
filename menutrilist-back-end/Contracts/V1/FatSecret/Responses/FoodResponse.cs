using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Menutrilist.Contracts.V1.Responses
{
    public partial class FoodResponse<T>
    {
        [JsonProperty("food")]
        public FoodClass<T> FoodFood { get; set; }
    }

    public partial class FoodClass<T>
    {
        [JsonProperty("food_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FoodId { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        [JsonProperty("food_type")]
        public string FoodType { get; set; }

        [JsonProperty("food_url")]
        public Uri FoodUrl { get; set; }

        [JsonProperty("servings")]
        public Servings<T> Servings { get; set; }
    }
    public partial class Servings<T>
    {
        [JsonProperty("serving")]
        public T Serving { get; set; }
    }


    public partial class FoodResponse<T>
    {
        public static FoodResponse<T> FromJson(string json) => JsonConvert.DeserializeObject<FoodResponse<T>>(json, Converter.Settings);
    }
}
