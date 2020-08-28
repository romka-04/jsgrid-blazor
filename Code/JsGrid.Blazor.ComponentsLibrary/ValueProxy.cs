using System;
using System.Linq.Expressions;

namespace JsGrid.Blazor.ComponentsLibrary
{
    class ValueProxy<T>
    {
        public static ValueProxy<T> CreateFrom<TKey>(Expression<Func<T, TKey>> constraint, string propName)
        {
            Func<T, TKey> paramExpression = constraint.Compile();
            Func<T, object> p = (T x) => (object)paramExpression(x);

            return new ValueProxy<T>(propName, p);
        }

        private readonly Func<T, object> Getter;
        public string PropName { get; }

        public ValueProxy(string propName, Func<T, object> getter)
        {
            PropName = propName;
            Getter   = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public object GetValue(T obj)
        {
            return Getter(obj);
        }
    }
}