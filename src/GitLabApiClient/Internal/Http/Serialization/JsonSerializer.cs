using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitLabApiClient.Internal.Http.Serialization
{
    internal sealed class RequestsJsonSerializer
    {
        private static readonly JsonSerializerSettings Settings;

        static RequestsJsonSerializer()
            => Settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new EmptyCollectionContractResolver(),
                Converters = { new StringEnumConverter() }
            };

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, Settings);

        public T Deserialize<T>(string serializeJson) => JsonConvert.DeserializeObject<T>(serializeJson, Settings);
    }
}
