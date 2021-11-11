using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Menutrilist.Domain.FatSecret
{
    public class FoodNutritionServing
    {
        public int Calcium { get; set; }
        public int Calories { get; set; }
        public float Carbohydrate { get; set; }
        public int Cholesterol { get; set; }
        public float Fat { get; set; }
        public float Fiber { get; set; }
        public float Iron { get; set; }
        [JsonProperty("monosaturated_fat")]
        public float MonosaturatedFat { get; set; }
        public int Sodium { get; set; }
        public float Sugar { get; set; }
        [JsonProperty("vitamin_a")]
        public int VitaminA { get; set; }
        [JsonProperty("vitamin_c")]
        public float VitaminC { get; set; }
        [JsonProperty("polyunsaturated_fat")]
        public float PolyunsaturatedFat { get; set; }
        public int Pottasium { get; set; }
        public float Protein { get; set; }
        [JsonProperty("saturated_fat")]
        public float SaturatedFat { get; set; }
        [JsonProperty("serving_description")]
        public string ServingDescription { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("serving_id")]
        public int ServingId { get; set; }
        [JsonProperty("serving_url")]
        public string ServingUrl { get; set; }
        [JsonProperty("metric_serving_amount")]
        public float MetricServingAmount { get; set; }
        [JsonProperty("metric_serving_unit")]
        public string MetricServingUnit { get; set; }
        [JsonProperty("measurement_description")]
        public string MeasurementDescription { get; set; }
        [JsonProperty("number_of_units")]
        public float NumberOfUnits { get; set; }
        [Required]
        public long FoodId { get; set; }

        [ForeignKey(nameof(FoodId))]
        [InverseProperty(nameof(Food.FoodNutritionServings))]
        public Food TheFood { get; set; }
    }
}