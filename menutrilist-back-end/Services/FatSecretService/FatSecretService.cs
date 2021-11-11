using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Menutrilist.Contracts.V1.Responses;
using Menutrilist.Data;
using Menutrilist.Domain.FatSecret;
using Menutrilist.Helpers;
using Newtonsoft.Json;

namespace Menutrilist.Services
{
    public class FatSecretService : IFatSecretService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly MenutrilistContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public FatSecretService(IHttpClientFactory clientFactory, JwtOptions jwtOptions, MenutrilistContext context)
        {
            _clientFactory = clientFactory;
            _jwtOptions = jwtOptions;
            _context = context;
        }
        public async Task<dynamic> GetFood(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"https://platform.fatsecret.com/rest/server.api?method=food.get.v2&format=json&food_id={id}");
            
            request.Headers.Add("Authorization", $"Bearer {_jwtOptions.FatSecretJwt}");
            
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try {
                    var food = FoodResponse<List<FoodNutritionServing>>.FromJson(jsonResponse);

                    if (await _context.Foods.FindAsync(food.FoodFood.FoodId) == null)
                    {
                        var foodEntity = new Food
                        {
                            BrandName = food.FoodFood.BrandName,
                            FoodName = food.FoodFood.FoodName,
                            Id = food.FoodFood.FoodId,
                            FoodType = food.FoodFood.FoodType,
                            FoodUrl = food.FoodFood.FoodUrl,
                        };

                        await _context.Foods.AddAsync(foodEntity);

                        foreach (FoodNutritionServing foodNutritionServing in food.FoodFood.Servings.Serving)
                        {
                            foodNutritionServing.FoodId = food.FoodFood.FoodId;
                            await _context.FoodNutritionServings.AddAsync(foodNutritionServing);
                        }

                        await _context.SaveChangesAsync();
                    }
                    return food;
                }
                catch (JsonSerializationException)
                {
                    var food = FoodResponse<FoodNutritionServing>.FromJson(jsonResponse);
                    if (await _context.Foods.FindAsync(food.FoodFood.FoodId) == null)
                    {
                        var foodEntity = new Food
                        {
                            BrandName = food.FoodFood.BrandName,
                            FoodName = food.FoodFood.FoodName,
                            Id = food.FoodFood.FoodId,
                            FoodType = food.FoodFood.FoodType,
                            FoodUrl = food.FoodFood.FoodUrl
                        };

                        await _context.Foods.AddAsync(foodEntity);

                        var foodNutritionServing = food.FoodFood.Servings.Serving;
                        foodNutritionServing.FoodId = food.FoodFood.FoodId;
                        await _context.FoodNutritionServings.AddAsync(foodNutritionServing);

                        await _context.SaveChangesAsync();
                    }
                    return food;
                }
            }

            return null;
        }

        public async Task<dynamic> SearchFood(string keyword, int maxResult, int pageNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"https://platform.fatsecret.com/rest/server.api?method=foods.search&format=json&max_results={maxResult}&search_expression={keyword}&page_number={pageNumber}");
            
            request.Headers.Add("Authorization", $"Bearer {_jwtOptions.FatSecretJwt}");
            
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try {
                    return FoodSearchResponse<FoodSearchItem>.FromJson(jsonResponse);
                }
                catch (JsonSerializationException)
                {
                    return FoodSearchResponse<FoodSearchItem[]>.FromJson(jsonResponse);
                }
            }

            return null;
        }
    }
}