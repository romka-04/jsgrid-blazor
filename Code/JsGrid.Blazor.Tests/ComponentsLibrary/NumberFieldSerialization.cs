using System.Text.Json;
using FluentAssertions;
using NUnit.Framework;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [TestFixture]
    public class NumberFieldSerialization
    {
        readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        [Test]
        public void Serialize()
        {
            // arrange
            var arg = new NumberField
            {
                Align = AlignEnum.Right,
                Name = "Name 1",
                ReadOnly = true,
                Sorter = SortingEnum.Number,
                Type = JsGridType.Number,
                Width = 150
            };
            // act
            var actual = JsonSerializer.Serialize(arg, _serializeOptions);
            // assert
            var expected = "{\"sorter\":\"number\",\"align\":\"right\",\"readOnly\":true,\"name\":\"Name 1\",\"type\":\"number\",\"width\":150}";
            actual.Should().Be(expected);
        }

        #region Test Helpers

        private NumberFieldSerializationFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new NumberFieldSerializationFixture();
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion
    }

    class NumberFieldSerializationFixture
    {
    }
}