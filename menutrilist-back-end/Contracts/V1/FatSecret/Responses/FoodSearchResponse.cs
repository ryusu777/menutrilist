using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Menutrilist.Contracts.V1.Responses
{
    public partial class FoodSearchResponse<T>
    {
        [JsonProperty("foods")]
        public Foods<T> Foods { get; set; }
    }

    public partial class Foods<T>
    {
        [JsonProperty("food")]
        public T Food { get; set; }

        [JsonProperty("max_results")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MaxResults { get; set; }

        [JsonProperty("page_number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PageNumber { get; set; }

        [JsonProperty("total_results")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotalResults { get; set; }
    }

    public partial class FoodSearchItem
    {
        [JsonProperty("food_description")]
        public string FoodDescription { get; set; }

        [JsonProperty("food_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FoodId { get; set; }

        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("food_type")]
        public string FoodType { get; set; }

        [JsonProperty("food_url")]
        public Uri FoodUrl { get; set; }

        [JsonProperty("brand_name", NullValueHandling = NullValueHandling.Ignore)]
        public string BrandName { get; set; }
    }

    public partial class FoodSearchResponse<T>
    {
        public static FoodSearchResponse<T> FromJson(string json) => JsonConvert.DeserializeObject<FoodSearchResponse<T>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FoodSearchResponse<dynamic> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this FoodResponse<dynamic> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}