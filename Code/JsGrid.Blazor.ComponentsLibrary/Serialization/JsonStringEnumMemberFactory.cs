using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    public sealed class JsonStringEnumMemberFactory
        : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(JsonStringEnumMemberConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                new object[] {},
                culture: null)!;

            return converter;
        }
    }
}