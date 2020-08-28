using System;
using System.Text.Json;
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
        public void JsonStringEnumMemberConverter_enum_to_json(string enumString, string serializedString, string serializedNumber)
        {
            MyCustomJsonStringEnumMemberEnum e = (MyCustomJsonStringEnumMemberEnum)Enum.Parse(typeof(MyCustomJsonStringEnumMemberEnum), enumString);
            string json = JsonSerializer.Serialize(e);
            json.Should().BeEquivalentTo(serializedString, "enum -> json issue");
        } 
        
        [Test]
        [TestCase("One", "\"one_\"", "0")]
        [TestCase("Two", "\"two_\"", "1")]
        [TestCase("Null", "null", "2")]
        public void JsonStringEnumMemberConverter_json_to_enum(string enumString, string serializedString, string serializedNumber)
        {
            MyCustomJsonStringEnumMemberEnum e = (MyCustomJsonStringEnumMemberEnum)Enum.Parse(typeof(MyCustomJsonStringEnumMemberEnum), enumString);

            MyCustomJsonStringEnumMemberEnum obj = JsonSerializer.Deserialize<MyCustomJsonStringEnumMemberEnum>(serializedString);
            obj.Should().Be(e, "json -> enum issue");
        }
        
        [Test]
        [TestCase("One", "\"one_\"", "0")]
        [TestCase("Two", "\"two_\"", "1")]
        [TestCase("Null", "null", "2")]
        public void JsonStringEnumMemberConverter_numeric_json_to_enum(string enumString, string serializedString, string serializedNumber)
        {
            var expected = (MyCustomJsonStringEnumMemberEnum)Enum.Parse(typeof(MyCustomJsonStringEnumMemberEnum), enumString);
            
            var actual = JsonSerializer.Deserialize<MyCustomJsonStringEnumMemberEnum>(serializedNumber);
            actual.Should().Be(expected);
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