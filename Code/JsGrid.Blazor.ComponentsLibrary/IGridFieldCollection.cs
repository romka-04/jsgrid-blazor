using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public interface IGridFieldCollection<T>
        : IEnumerable<IGridField>
    {
        /// <summary>
        ///     Add new field to the grid collection
        /// </summary>
        void Add(IGridField column);

        /// <summary>
        ///     Add new field to the grid collection
        /// </summary>
        /// <param name="constraint">Member of generic class</param>
        void Add<TKey>(Expression<Func<T, TKey>> constraint);

        /// <summary>
        ///     Add new field to the grid collection
        /// </summary>
        /// <param name="constraint">Member of generic class</param>
        /// <param name="columnName">Specify column internal static name, used for sorting and filtering</param>
        void Add<TKey>(Expression<Func<T, TKey>> constraint, string columnName);
    }

    class GridFieldCollection<T> 
        : IGridFieldCollection<T>
    {
        private readonly List<IGridField> _list;

        public void Add(IGridField column)
        {
            throw new NotImplementedException();
        }

        public void Add<TKey>(Expression<Func<T, TKey>> constraint)
        {
            throw new NotImplementedException();
        }

        public void Add<TKey>(Expression<Func<T, TKey>> constraint, string columnName)
        {
            var gridField = CastToGridField()
            throw new NotImplementedException();
        }

        /// <summary>
        /// Represents method for numeric types.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="constraint"></param>
        /// <param name="columnName"></param>
        private IGridField CastToGridField<TKey>(Expression<Func<T, TKey>> constraint, string columnName)
            where TKey : struct,
                IComparable,
                IComparable<T>,
                IConvertible,
                IEquatable<T>,
                IFormattable
        {
            return new TextGridField{ Name = columnName };
        }
        private IGridField CastToGridField(Expression<Func<T, string>> constraint, string columnName)
        {
            return new NumberGridFieldImpl{ Name = columnName };
        }

        public IEnumerator<IGridField> GetEnumerator()
            => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _list.GetEnumerator();
    }

    public interface IGridField
    {
        public string Name { get; }
        public JsGridType Type { get; }
        BaseField BuildField();
    }
    abstract class BaseGridField
        : IGridField
    {
        public string Name { get; set; }
        public JsGridType Type { get; }
        public abstract BaseField BuildField();
    }

    class NumberGridFieldImpl
        : BaseGridField
    {
        public override BaseField BuildField()
        {
            return new NumberField();
        }
    }

    class TextGridField
        : BaseGridField
    {
        public override BaseField BuildField()
        {
            return new TextField();
        }
    }
}