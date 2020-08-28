using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    public sealed class JsonEnumConverterAttribute
        : JsonConverterAttribute
    {
        public override JsonConverter CreateConverter(Type typeToConvert)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(JsonStringEnumMemberConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                new object[] { },
                culture: null)!;

            return converter;
        }
    }
}