using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public class GridClientBuilder<T>
    {
        private readonly GridFieldCollection<T> _fields;
        private readonly Action<IGridFieldCollection<T>> _fieldAction;
        private readonly List<T> _data = new List<T>();

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
            var gridClient = new GridClient<T>
            {
                Fields = BuildFields(),
                Data = BuildData()
            };
            return gridClient;
        }

        private BaseField[] BuildFields() => BuildFieldsEnumerable()?.ToArray();

        private IEnumerable<BaseField> BuildFieldsEnumerable()
        {
            foreach (IGridField gridField in _fields)
            {
                yield return gridField.BuildField();
            }
        }
        private object BuildData()
        {
            return _data.Any() 
                ? BuildData(_fields.FieldProxies, _data).ToArray()
                : null;
        }

        private IEnumerable<object> BuildData(IEnumerable<ObjectProxy<T>> fieldProxies, IEnumerable<T> data)
        {
            foreach (T dataPortion in data)
            {
                yield return BuildData(fieldProxies, dataPortion);
            }
        }

        private IDictionary<string, object> BuildData(IEnumerable<ObjectProxy<T>> fieldProxies, T data)
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
    }
}