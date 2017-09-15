using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitLabApiClient.Internal.Http.Serialization
{
    internal sealed class RequestsJsonSerializer
    {
        static RequestsJsonSerializer() => JsonConvert.DefaultSettings = () =>
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new EmptyCollectionContractResolver()
            };

            settings.Converters.Add(new StringEnumConverter());
            return settings;
        };

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public T Deserialize<T>(string serializeJson) => JsonConvert.DeserializeObject<T>(serializeJson);
    }
}
