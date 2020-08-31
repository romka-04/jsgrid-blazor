using System;
using FluentAssertions;
using NUnit.Framework;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [TestFixture]
    public class GridClientBuilderTests
    {
        [Test]
        public void Build_Fields_should_choose_proper_grid_field_types()
        {
            // arrange
            Action<IGridFieldCollection<TestClass>> fieldsDeclaration = collection =>
            {
                collection.Add(x => x.Name);
                collection.Add(x => x.Name, "Last Name", readOnly: true, width: 150, autoSearch: true);
                collection.Add(x => x.Id);
                collection.Add(x => x.Id, "Test ID", readOnly: true, 150, SortingEnum.Number, AlignEnum.Right );
            };
            var sut = _fixture.CreateSut(fieldsDeclaration);
            // act
            var actual = sut.Build();
            // assert
            var expected = new BaseField[]
            {
                new TextField{ Name = "Name", AutoSearch = false, ReadOnly = false },
                new TextField{ Name = "Last Name", AutoSearch = true, ReadOnly = true, Width = 150 },
                new NumberField{ Name = "Id" }, 
                new NumberField{ Name = "Test ID", ReadOnly = true, Sorter = SortingEnum.Number, Align = AlignEnum.Right, Width = 150 }, 
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