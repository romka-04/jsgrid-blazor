using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [TestFixture]
    public class NumberGridFieldTests
    {
        readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        [Test]
        public void Serialize()
        {
            // arrange
            var arg = new NumberGridField(
                name: "Name 1",
                title: null,
                readOnly: true,
                width: 150,
                sorter: SortingEnum.Number,
                align: AlignEnum.Right
            );
            // act
            var actual = JsonSerializer.Serialize(arg, _serializeOptions);
            // assert
            var expected = "{\"sorter\":\"number\",\"align\":\"right\",\"readOnly\":true,\"name\":\"Name 1\",\"type\":\"number\",\"width\":150}";
            actual.Should().Be(expected);
        }

        [Test]
        public void Serialize_fix_issue_when_properties_derived_from_base_class_not_serialized()
        {
            // arrange
            IGridField arg = new NumberGridField(
                name: "Name 1",
                title: null,
                readOnly: true,
                width: 150,
                sorter: SortingEnum.Number,
                align: AlignEnum.Right
            );
            // act
            var actual = JsonSerializer.Serialize<object>(arg, _serializeOptions);
            // assert
            var expected = "{\"sorter\":\"number\",\"align\":\"right\",\"readOnly\":true,\"name\":\"Name 1\",\"type\":\"number\",\"width\":150}";
            actual.Should().Be(expected);
        }
    }
}