using System;
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

    public interface IGridField
    {
        public string Name { get; }
        public JsGridType Type { get; }
    }
}