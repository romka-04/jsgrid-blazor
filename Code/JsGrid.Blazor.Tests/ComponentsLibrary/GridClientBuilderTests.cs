using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json.Linq;
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
            Action<IGridFieldCollection<TestClass>> fieldsDeclaration = c =>
            {
                c.Add(x => x.Name);
                c.Add(x => x.Name, "Last Name", readOnly: true, width: 150, autoSearch: true);
                c.Add(x => x.Id);
                c.Add(x => x.Id, "Test ID", readOnly: true, 150, SortingEnum.Number, AlignEnum.Right );
                c.Add(x => x.IsEnabled);
                c.Add(x => x.IsEnabled, "Is Enabled", readOnly: true, width: 125, align: AlignEnum.Right);
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
                new CheckboxField{ Name = "IsEnabled" }, 
                new CheckboxField{ Name = "Is Enabled", ReadOnly = true, Align = AlignEnum.Right, Width = 125 }, 
            };
            actual.Fields.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void WithStaticData_should_properly_populate_data()
        {
            // arrange
            Action<IGridFieldCollection<TestClass>> fieldsDeclaration = c =>
            {
                c.Add(x => x.Name);
                c.Add(x => x.Name, "Last Name");
                c.Add(x => x.Id);
                c.Add(x => x.Id, "Test ID");
            };
            var staticData = new[]
            {
                new TestClass {Id = 1, Name = "Name 1", IsEnabled = true},
                new TestClass {Id = 2, Name = "Name 2", IsEnabled = false},
            };
            var sut = _fixture.CreateSut(fieldsDeclaration);
            // act
            var actual = sut
                .WithStaticData(staticData)
                .Build();
            // assert
            var expected = _fixture
                .WithItem(
                    new KeyValuePair<string,object>("Name", "Name 1"), new KeyValuePair<string, object>("Last Name", "Name 1"),
                    new KeyValuePair<string, object>("Id", 1), new KeyValuePair<string, object>("Test ID", 1)
                )
                .WithItem(
                    new KeyValuePair<string,object>("Name", "Name 2"), new KeyValuePair<string, object>("Last Name", "Name 2"),
                    new KeyValuePair<string, object>("Id", 2), new KeyValuePair<string, object>("Test ID", 2)
                )
                .CreateExpected();
            actual.Data.Should().BeEquivalentTo(expected);
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
        public bool IsEnabled { get; set; }
    }

    public class GridClientBuilderTestFixture
    {
        public GridClientBuilder<TestClass> CreateSut(Action<IGridFieldCollection<TestClass>> fields)
        {
            return new GridClientBuilder<TestClass>(fields);
        }

        public GridClientBuilderTestFixture WithItem(params KeyValuePair<string, object>[] elemProperties)
            => WithItem((IEnumerable<KeyValuePair<string, object>>) elemProperties);

        public GridClientBuilderTestFixture WithItem(IEnumerable<KeyValuePair<string, object>> elemProperties)
            => WithItem(new Dictionary<string, object>(elemProperties));

        
        private readonly List<JObject> _objects = new List<JObject>();
        
        public GridClientBuilderTestFixture WithItem(IDictionary<string, object> elemProperties)
        {
            var jObj = new JObject(
                elemProperties.Select(x => new JProperty(x.Key, x.Value))
                );
            _objects.Add(jObj);
            return this;
        }

        public JArray CreateExpected()
        {
            return new JArray(_objects);
        }
    }
}