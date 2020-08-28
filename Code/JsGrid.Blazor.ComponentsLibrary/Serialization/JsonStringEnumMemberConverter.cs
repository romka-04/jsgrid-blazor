using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
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

            switch (s_enumTypeCode)
            {
                // Switch cases ordered by expected frequency

                case TypeCode.Int32:
                    if (reader.TryGetInt32(out int int32))
                    {
                        return Unsafe.As<int, T>(ref int32);
                    }
                    break;
                case TypeCode.UInt32:
                    if (reader.TryGetUInt32(out uint uint32))
                    {
                        return Unsafe.As<uint, T>(ref uint32);
                    }
                    break;
                case TypeCode.UInt64:
                    if (reader.TryGetUInt64(out ulong uint64))
                    {
                        return Unsafe.As<ulong, T>(ref uint64);
                    }
                    break;
                case TypeCode.Int64:
                    if (reader.TryGetInt64(out long int64))
                    {
                        return Unsafe.As<long, T>(ref int64);
                    }
                    break;

                // When utf8reader/writer will support all primitive types we should remove custom bound checks
                // https://github.com/dotnet/runtime/issues/29000
                case TypeCode.SByte:
                    if (reader.TryGetInt32(out int byte8) && JsonHelpers.IsInRangeInclusive(byte8, sbyte.MinValue, sbyte.MaxValue))
                    {
                        sbyte byte8Value = (sbyte)byte8;
                        return Unsafe.As<sbyte, T>(ref byte8Value);
                    }
                    break;
                case TypeCode.Byte:
                    if (reader.TryGetUInt32(out uint ubyte8) && JsonHelpers.IsInRangeInclusive(ubyte8, byte.MinValue, byte.MaxValue))
                    {
                        byte ubyte8Value = (byte)ubyte8;
                        return Unsafe.As<byte, T>(ref ubyte8Value);
                    }
                    break;
                case TypeCode.Int16:
                    if (reader.TryGetInt32(out int int16) && JsonHelpers.IsInRangeInclusive(int16, short.MinValue, short.MaxValue))
                    {
                        short shortValue = (short)int16;
                        return Unsafe.As<short, T>(ref shortValue);
                    }
                    break;
                case TypeCode.UInt16:
                    if (reader.TryGetUInt32(out uint uint16) && JsonHelpers.IsInRangeInclusive(uint16, ushort.MinValue, ushort.MaxValue))
                    {
                        ushort ushortValue = (ushort)uint16;
                        return Unsafe.As<ushort, T>(ref ushortValue);
                    }
                    break;
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
    }

    internal static partial class JsonHelpers
    {
        /// <summary>
        /// Returns the span for the given reader.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<byte> GetSpan(this ref Utf8JsonReader reader)
        {
            return reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
        }

#if !BUILDING_INBOX_LIBRARY
        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is a valid Unicode scalar
        /// value, i.e., is in [ U+0000..U+D7FF ], inclusive; or [ U+E000..U+10FFFF ], inclusive.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidUnicodeScalar(uint value)
        {
            // By XORing the incoming value with 0xD800, surrogate code points
            // are moved to the range [ U+0000..U+07FF ], and all valid scalar
            // values are clustered into the single range [ U+0800..U+10FFFF ],
            // which allows performing a single fast range check.

            return IsInRangeInclusive(value ^ 0xD800U, 0x800U, 0x10FFFFU);
        }
#endif

        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is between
        /// <paramref name="lowerBound"/> and <paramref name="upperBound"/>, inclusive.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRangeInclusive(uint value, uint lowerBound, uint upperBound)
            => (value - lowerBound) <= (upperBound - lowerBound);

        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is between
        /// <paramref name="lowerBound"/> and <paramref name="upperBound"/>, inclusive.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRangeInclusive(int value, int lowerBound, int upperBound)
            => (uint)(value - lowerBound) <= (uint)(upperBound - lowerBound);

        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is between
        /// <paramref name="lowerBound"/> and <paramref name="upperBound"/>, inclusive.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRangeInclusive(long value, long lowerBound, long upperBound)
            => (ulong)(value - lowerBound) <= (ulong)(upperBound - lowerBound);

        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is between
        /// <paramref name="lowerBound"/> and <paramref name="upperBound"/>, inclusive.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRangeInclusive(JsonTokenType value, JsonTokenType lowerBound, JsonTokenType upperBound)
            => (value - lowerBound) <= (upperBound - lowerBound);

        /// <summary>
        /// Returns <see langword="true"/> if <paramref name="value"/> is in the range [0..9].
        /// Otherwise, returns <see langword="false"/>.
        /// </summary>
        public static bool IsDigit(byte value) => (uint)(value - '0') <= '9' - '0';

        /// <summary>
        /// Perform a Read() with a Debug.Assert verifying the reader did not return false.
        /// This should be called when the Read() return value is not used, such as non-Stream cases where there is only one buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadWithVerify(this ref Utf8JsonReader reader)
        {
            bool result = reader.Read();
            Debug.Assert(result);
        }

        /// <summary>
        /// Calls Encoding.UTF8.GetString that supports netstandard.
        /// </summary>
        /// <param name="bytes">The utf8 bytes to convert.</param>
        /// <returns></returns>
        internal static string Utf8GetString(ReadOnlySpan<byte> bytes)
        {
            return Encoding.UTF8.GetString(bytes
#if NETSTANDARD2_0 || NETFRAMEWORK
                        .ToArray()
#endif
                );
        }

        /// <summary>
        /// Emulates Dictionary.TryAdd on netstandard.
        /// </summary>
        internal static bool TryAdd<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
#if NETSTANDARD2_0 || NETFRAMEWORK
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }

            return false;
#else
            return dictionary.TryAdd(key, value);
#endif
        }

        internal static bool IsFinite(double value)
        {
#if BUILDING_INBOX_LIBRARY
            return double.IsFinite(value);
#else
            return !(double.IsNaN(value) || double.IsInfinity(value));
#endif
        }

        internal static bool IsFinite(float value)
        {
#if BUILDING_INBOX_LIBRARY
            return float.IsFinite(value);
#else
            return !(float.IsNaN(value) || float.IsInfinity(value));
#endif
        }
    }
}