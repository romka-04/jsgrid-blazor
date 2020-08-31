using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    class JsonConverterEnumWithAttribute<T>
        : JsonConverter<T>
        where T : struct, Enum
    {
        private readonly JsonStringValueConverter.EnumConverterOptions _converterOptions;
        private readonly JsonNamingPolicy _namingPolicy;
        private readonly ConcurrentDictionary<string, string> _nameCache;

        public JsonConverterEnumWithAttribute(JsonStringValueConverter.EnumConverterOptions options)
            : this(options, namingPolicy: null)
        {
        }
        public JsonConverterEnumWithAttribute(JsonStringValueConverter.EnumConverterOptions options,
            JsonNamingPolicy namingPolicy)
        {
            _converterOptions = options;
            if (namingPolicy != null)
            {
                _nameCache = new ConcurrentDictionary<string, string>();
            }
            else
            {
                namingPolicy = JsonNamingPolicy.Default;
            }
            _namingPolicy = namingPolicy;
        }

        public override bool CanConvert(Type type)
        {
            return type.IsEnum;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}