using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public class GridClientBuilder<T>
    {
        private readonly GridFieldCollection<T> _fields;
        private readonly List<T> _data = new List<T>();

        private string _width = "100%";
        private string _height= "400px";

        internal GridClientBuilder(GridFieldCollection<T> fields)
        {
            _fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }
        public GridClientBuilder(Action<IGridFieldCollection<T>> fieldAction)
            : this(CreateCollection(fieldAction))
        { }

        private static GridFieldCollection<T> CreateCollection(Action<IGridFieldCollection<T>> fieldAction)
        {
            if (null == fieldAction) throw new ArgumentNullException(nameof(fieldAction));
            var collection = new GridFieldCollection<T>();
            fieldAction.Invoke(collection);
            return collection;
        }

        public IGridClient<T> Build()
        {
            var settings = new JsGridSettings
            {
                Fields = _fields.ToArray(),
                Data   = BuildData(),
                Width  = _width,
                Height = _height
            };
            var gridClient = new GridClient<T>
            {
                Settings = settings
            };
            return gridClient;
        }

        private object BuildData()
        {
            return _data.Any() 
                ? BuildData(_fields.FieldProxies, _data).ToArray()
                : null;
        }

        private IEnumerable<object> BuildData(IEnumerable<ValueProxy<T>> fieldProxies, IEnumerable<T> data)
        {
            foreach (T dataPortion in data)
            {
                yield return BuildData(fieldProxies, dataPortion);
            }
        }

        private IDictionary<string, object> BuildData(IEnumerable<ValueProxy<T>> fieldProxies, T data)
        {
            IDictionary<string, object> newValue = new ExpandoObject();
            foreach (var getterFunc in fieldProxies)
            {
                newValue[getterFunc.PropName] = getterFunc.GetValue(data);
            }

            return newValue;
        }

        public GridClientBuilder<T> WithStaticData(IEnumerable<T> data)
        {
            _data.AddRange(data);
            return this;
        }

        public GridClientBuilder<T> WithDimensions(string width, string height)
        {
            _width  = width;
            _height = height;
            return this;
        }
    }
}