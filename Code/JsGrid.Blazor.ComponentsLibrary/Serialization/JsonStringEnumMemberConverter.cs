using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    class JsonStringEnumMemberConverter<T>
        : JsonConverter<T>
    where T : struct, Enum
    {
        private static readonly TypeCode s_enumTypeCode = Type.GetTypeCode(typeof(T));
        // Odd type codes are conveniently signed types (for enum backing types).
        private static readonly string? s_negativeSign = ((int)s_enumTypeCode % 2) == 0 ? null : NumberFormatInfo.CurrentInfo.NegativeSign;


        private readonly ConcurrentDictionary<string, string>? _nameCache;
        private readonly ConcurrentDictionary<T, string>? _enumToStringCache;
        private readonly ConcurrentDictionary<ValueTuple<string?>, T>? _stringToEnumCache;

        public JsonStringEnumMemberConverter()
        {
            _nameCache = new ConcurrentDictionary<string, string>();
            var enumType = typeof(T);
            var fields = enumType.GetFields();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<JsonStringEnumMemberAttribute>(false);

                if (attribute != null)
                {
                    _enumToStringCache ??= new ConcurrentDictionary<T, string>();
                    _stringToEnumCache ??= new ConcurrentDictionary<ValueTuple<string?>, T>();
                    var enumValue = (T)Enum.Parse(enumType, field.Name);
                    _enumToStringCache[enumValue] = attribute.Name;
                    _stringToEnumCache[ValueTuple.Create(attribute.Name)] = enumValue;
                }
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType token = reader.TokenType;

            if (token == JsonTokenType.String || token == JsonTokenType.Null)
            {
                // Try parsing case sensitive first
                string? enumString = reader.GetString();
                if (_stringToEnumCache != null && _stringToEnumCache.TryGetValue(ValueTuple.Create(enumString), out T value))
                {
                    return value;
                }
                else
                {
                    if (!Enum.TryParse(enumString, out value)
                        && !Enum.TryParse(enumString, ignoreCase: true, out value))
                    {
                        throw new InvalidOperationException($"Unable to deserialize value '{enumString}'");
                        return default;
                    }
                    return value;
                }
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (_enumToStringCache != null && _enumToStringCache.TryGetValue(value, out string? stringValue))
            {
                if (stringValue != null)
                {
                    writer.WriteStringValue(stringValue);
                }
                else
                {
                    writer.WriteNullValue();
                }
                return;
            }

            string original = value.ToString();
            if (_nameCache != null && _nameCache.TryGetValue(original, out string? transformed))
            {
                writer.WriteStringValue(transformed);
                return;
            }

            switch (s_enumTypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue(Unsafe.As<T, int>(ref value));
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue(Unsafe.As<T, uint>(ref value));
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(Unsafe.As<T, ulong>(ref value));
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue(Unsafe.As<T, long>(ref value));
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue(Unsafe.As<T, short>(ref value));
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue(Unsafe.As<T, ushort>(ref value));
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue(Unsafe.As<T, byte>(ref value));
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue(Unsafe.As<T, sbyte>(ref value));
                    break;
                default:
                    break;
            }
        }

        private static bool IsValidIdentifier(string value)
        {
            // Trying to do this check efficiently. When an enum is converted to
            // string the underlying value is given if it can't find a matching
            // identifier (or identifiers in the case of flags).
            //
            // The underlying value will be given back with a digit (e.g. 0-9) possibly
            // preceded by a negative sign. Identifiers have to start with a letter
            // so we'll just pick the first valid one and check for a negative sign
            // if needed.
            return (value[0] >= 'A' &&
                    (s_negativeSign == null || !value.StartsWith(s_negativeSign)));
        }
    }
}