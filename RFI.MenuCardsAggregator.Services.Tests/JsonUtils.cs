using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    public static class JsonUtils
    {
        public static string SerializeToJson(Object data)
        {
            var json = JsonConvert.SerializeObject(data, GetJsonSerializerSettings());
            return json;
        }

        public static T DeserializeFromJson<T>(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData, GetJsonSerializerSettings());
            return data;
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                Culture = CultureInfo.InvariantCulture,
                Formatting = Formatting.Indented
            };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }
    }
}
