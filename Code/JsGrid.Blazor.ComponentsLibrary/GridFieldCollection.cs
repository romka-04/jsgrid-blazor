using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JsGrid.Blazor.ComponentsLibrary
{
    class GridFieldCollection<T> 
        : IGridFieldCollection<T>
    {
        private readonly List<IGridField> _list = new List<IGridField>();
        private readonly List<Func<T, object>> _getterList = new List<Func<T, object>>();
        private readonly List<ValueProxy<T>> _objectProxies = new List<ValueProxy<T>>();

        public IEnumerable<ValueProxy<T>> FieldProxies => _objectProxies;

        public void Add(IGridField field)
        {
            if (null == field) throw new ArgumentNullException(nameof(field));
            _list.Add(field);
        }

        public void AddExpression<TKey>(Expression<Func<T, TKey>> constraint, string name)
        {
            var objProxy = ValueProxy<T>.CreateFrom(constraint, name);
            _objectProxies.Add(objProxy);
           
        }

        public IEnumerable<Func<T, object>> GetterList => _getterList;

        public IEnumerator<IGridField> GetEnumerator()
            => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _list.GetEnumerator();
    }
}