using System;
using System.Reflection;

namespace Newtonsoft.Json.Converters
{
    /// <summary>
    /// 为只读转换器提供的基类
    /// </summary>
    public abstract class ReadOnlyConverterBase : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotSupportedException("Serializing is not usable in ReadOnlyConverter");
    }

    /// <summary>
    /// 为只读转换器提供的基类
    /// </summary>
    /// <typeparam name="T">转换器适用的类型</typeparam>
    public abstract class ReadOnlyConverterBase<T> : ReadOnlyConverterBase
    {
        public override bool CanConvert(Type objectType)
            => objectType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());
    }
}
