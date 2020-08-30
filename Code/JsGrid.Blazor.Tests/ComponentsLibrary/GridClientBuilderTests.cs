using System;
using FluentAssertions;
using NUnit.Framework;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [TestFixture]
    public class GridClientBuilderTests
    {
        [Test]
        public void Build()
        {
            // arrange
            Action<IGridFieldCollection<TestClass>> fieldsDeclaration = collection =>
            {
                collection.Add(x => x.Name);
                collection.Add(x => x.Id);
            };
            var sut = _fixture.CreateSut(fieldsDeclaration);
            // act
            var actual = sut.Build();
            // assert
            var expected = new BaseField[]
            {
                new TextField{},
                new NumberField(), 
            };
            actual.Fields.Should().BeEquivalentTo(expected);
        }

        #region CTOR

        private GridClientBuilderTestFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new GridClientBuilderTestFixture();
        }
        [TearDown]
        public void TearDown()
        { }

        #endregion
    }

    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GridClientBuilderTestFixture
    {
        public GridClientBuilder<TestClass> CreateSut(Action<IGridFieldCollection<TestClass>> fields)
        {
            return new GridClientBuilder<TestClass>(fields);
        }
    }
}