using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using NUnit.Framework;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    [TestFixture]
    public class JsonStringEnumMemberConverterTests
    {
        [Test]
        [TestCase("One", "\"one_\"", "0")]
        [TestCase("Two", "\"two_\"", "1")]
        [TestCase("Null", "null", "2")]
        public void JsonStringEnumMemberConverter(string enumString, string serializedString, string serializedNumber)
        {
            MyCustomJsonStringEnumMemberEnum e = (MyCustomJsonStringEnumMemberEnum)Enum.Parse(typeof(MyCustomJsonStringEnumMemberEnum), enumString);
            string json = JsonSerializer.Serialize(e);
            json.Should().BeEquivalentTo(serializedString, "enum -> json issue");

            MyCustomJsonStringEnumMemberEnum obj = JsonSerializer.Deserialize<MyCustomJsonStringEnumMemberEnum>(serializedString);
            obj.Should().Be(e, "json -> enum issue");

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Clear();
            jsonOptions.Converters.Add(new JsonStringEnumMemberConverter<MyCustomJsonStringEnumMemberEnum>());
            obj = JsonSerializer.Deserialize<MyCustomJsonStringEnumMemberEnum>(serializedNumber, jsonOptions);
            obj.Should().Be(e, "parse number issue");
        }
    }

    [JsonEnumConverter]
    public enum MyCustomJsonStringEnumMemberEnum
    {
        [JsonStringEnumMember("one_")]
        One,
        [JsonStringEnumMember("two_")]
        Two,
        [JsonStringEnumMember(null)]
        Null
    }
}