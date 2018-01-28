using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JacobZ.WebInterface.BangumiTv
{
    // 用来提取rating属性中分数的转换器
    internal class RatingConverter : ReadOnlyConverterBase<int[]>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = new uint[10];
            var counter = 0;
            foreach (JProperty item in JObject.Load(reader)["count"])
                array[counter++] = item.Value.Value<uint>();
            return array;
        }
    }

    // 转换星期，由于系统默认Sunday对应0，因此重新写了转换器
    internal class DayOfWeekConverter : ReadOnlyConverterBase<DayOfWeek>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ((long)reader.Value == 7)
                return DayOfWeek.Sunday;
            else return Enum.ToObject(typeof(DayOfWeek), reader.Value);
        }
    }

    // 生成键为对象，值为某一属性的字典的转换器
    public class ObjectToPropertyDictionaryConverter : ReadOnlyConverterBase
    {
        private string _property;
        public ObjectToPropertyDictionaryConverter(string property)=>_property = property;

        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsConstructedGenericType) return false;
            var genericDef = objectType.GetGenericTypeDefinition();
            return genericDef == typeof(IReadOnlyDictionary<,>) || genericDef == typeof(IDictionary<,>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objType = objectType.GenericTypeArguments[0];
            var propertyType = objectType.GenericTypeArguments[1];
            var dicType = typeof(Dictionary<,>).MakeGenericType(objType, propertyType);
            var dic = dicType.GetTypeInfo().DeclaredConstructors.First().Invoke(null) as IDictionary;
            
            JArray list = JArray.Load(reader);
            foreach(var item in list)
            {
                var obj = item.ToObject(objType);
                var property = item[_property].ToObject(propertyType);
                dic.Add(obj, property);
            }
            return dic;
        }
    }

    internal class BangumiContractResolver : DefaultContractResolver
    {
        public static readonly BangumiContractResolver Detailed = new BangumiContractResolver(false);
        public static readonly BangumiContractResolver Simple = new BangumiContractResolver(true);

        private bool _simple;

        public BangumiContractResolver(bool simple = true) => _simple = simple;

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(Subject) && property.PropertyName == "EpisodeCount")
                if(_simple) property.PropertyName = "eps";

            if (property.DeclaringType == typeof(Subject) && property.PropertyName == "Episodes")
                if (!_simple) property.PropertyName = "eps";

            return property;
        }
    }

    // TODO: 检查Json.Net的更新，这个类已经被加入代码
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class RoleTypeConverter : ReadOnlyConverterBase<RoleType>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var name = reader.Value as string;
            switch(name)
            {
                case "主角":
                    return RoleType.Lead;
                case "配角":
                    return RoleType.Supporting;
                case "客串":
                    return RoleType.Guest;
                default:
                    throw new JsonSerializationException("Unknown role type name");
            }
        }
    }

    internal class GenderConverter : ReadOnlyConverterBase<bool>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var gender = reader.Value as string;
            switch (gender)
            {
                case "男":
                    return false;
                case "女":
                    return true;
                default:
                    throw new JsonSerializationException("Unknown role type name");
            }

        }
    }

    internal class CollectionStatusConverter : ReadOnlyConverterBase<CollectionStatus>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return JObject.Load(reader)["type"].Value<CollectionStatus>();
        }
    }
}
