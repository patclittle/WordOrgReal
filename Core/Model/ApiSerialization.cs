namespace Core.Model
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Autofac;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class ApiSerialization
    {
        public static void ConfigureApiSerializerSettings(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new IgnoreInternalPropertiesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
        }

        public static bool IsInternal(MemberInfo member) => IgnoreInternalPropertiesContractResolver.ShouldIgnore(member);

        public class ListOfEnumConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        private class IgnoreInternalPropertiesContractResolver : CamelCasePropertyNamesContractResolver
        {
            public static bool ShouldIgnore(MemberInfo member)
            {
                if (member.GetCustomAttribute<HideFromApiAttribute>() != null)
                {
                    return true;
                }

                return false;
            }

            public override JsonContract ResolveContract(Type type)
            {
                string name = type.Name;
                if (type == typeof(CancellationToken))
                {
                    return null;
                }
                else
                {
                    return base.ResolveContract(type);
                }
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                property.Ignored |= ShouldIgnore(member);
                return property;
            }
        }
    }
}
