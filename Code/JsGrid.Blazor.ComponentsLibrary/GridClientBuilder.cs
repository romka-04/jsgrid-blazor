using System;
using System.Collections.Generic;
using System.Linq;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public class GridClientBuilder<T>
    {
        private readonly IGridFieldCollection<T> _fields;
        private readonly Action<IGridFieldCollection<T>> _fieldAction;

        protected GridClientBuilder(IGridFieldCollection<T> fields)
        {
            _fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }
        public GridClientBuilder(Action<IGridFieldCollection<T>> fieldAction)
        {
            _fieldAction = fieldAction 
                ?? throw new ArgumentNullException(nameof(fieldAction));
        }

        public IGridClient<T> Build()
        {
            var gridClient = new GridClient<T>
            {
                Fields = BuildFields()
            };
            return gridClient;
        }

        private BaseField[] BuildFields() => BuildFieldsEnumerable()?.ToArray();

        private IEnumerable<BaseField> BuildFieldsEnumerable()
        {
            var collection = new GridFieldCollection<T>();
            _fieldAction.Invoke(collection);
            foreach (IGridField gridField in collection)
            {
                yield return gridField.BuildField();
            }
        }

        public GridClientBuilder<T> WithStaticData(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}