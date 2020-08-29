using System;
using System.Collections.Generic;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public class GridClientBuilder<T>
    {
        private readonly IGridFieldCollection<T> _fields;

        public GridClientBuilder(IGridFieldCollection<T> fields)
        {
            _fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }
        public GridClientBuilder(Action<IGridFieldCollection<T>> fieldAction)
        {
            throw new NotImplementedException();
            //_fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public IGridClient<T> Build()
        {
            throw new NotImplementedException();
        }

        public GridClientBuilder<T> WithStaticData(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}