using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menutrilist.Domain.FatSecret
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string FoodName { get; set; }
        public string BrandName { get; set; }
        public string FoodType { get; set; }
        public Uri FoodUrl { get; set; }

        [InverseProperty(nameof(FoodNutritionServing.TheFood))]
        public IEnumerable<FoodNutritionServing> FoodNutritionServings { get; set; }
    }
}