using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitLabApiClient.Internal.Http.Serialization
{
    internal sealed class CollectionToCommaSeparatedValuesConverter : JsonConverter
    {
        public override bool CanRead { get; } = false;

        public override bool CanConvert(Type objectType) =>
            objectType == typeof(IEnumerable<string>);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(string.Join(",", (IEnumerable<string>)value));

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            throw new NotSupportedException();
    }
}
