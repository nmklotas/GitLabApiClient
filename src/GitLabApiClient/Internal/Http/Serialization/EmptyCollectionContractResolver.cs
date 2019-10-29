using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GitLabApiClient.Internal.Http.Serialization
{
    internal sealed class EmptyCollectionContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            Predicate<object> shouldSerialize = property.ShouldSerialize;
            property.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !IsEmptyCollection(property, obj);
            return property;
        }

        private static bool IsEmptyCollection(JsonProperty property, object target)
        {
            object value = property.ValueProvider.GetValue(target);
            if (!(value is IEnumerable collection))
                return false;

            return !collection.GetEnumerator().MoveNext();
        }
    }
}
