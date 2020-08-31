using System;
using System.Linq.Expressions;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public static class GridFieldCollectionExtension
    {
        /// <summary>
        /// Adds numerical grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <typeparam name="TKey">The type of the field. Should be numeric only.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="constraint">The expressions that define the field.</param>
        /// <param name="columnName">The grid column name. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="sorter">Column sorter.</param>
        /// <param name="align">Text align.</param>
        public static void Add<T, TKey>(this IGridFieldCollection<T> collection, Expression<Func<T, TKey>> constraint,
                string columnName = null, bool readOnly = false, int? width = null, 
                SortingEnum sorter = SortingEnum.String, AlignEnum align = AlignEnum.None )
            where TKey : struct, IComparable
        {
            if (null == collection) throw new ArgumentNullException(nameof(collection));

            columnName ??= GetColumnName(constraint);

            var gridField = new NumberGridField(
                name:     columnName,
                readOnly: readOnly,
                width:    width,
                sorter:   sorter,
                align:    align);

            collection.Add(gridField);
            collection.AddExpression(constraint, columnName);
        }

        /// <summary>
        /// Adds numerical grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <typeparam name="TKey">The type of the field. Should be numeric only.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="constraint">The expressions that define the field.</param>
        /// <param name="columnName">The grid column name. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="sorter">Column sorter.</param>
        /// <param name="align">Text align.</param>
        public static void Add<T>(this IGridFieldCollection<T> collection, Expression<Func<T, bool>> constraint,
                string columnName = null, bool readOnly = false, int? width = null, 
                SortingEnum sorter = SortingEnum.String, AlignEnum align = AlignEnum.None )
        {
            columnName ??= GetColumnName(constraint);

            var gridField = new CheckboxGridField(
                name:     columnName,
                readOnly: readOnly,
                width:    width,
                align:    align
            );

            collection.Add(gridField);
            collection.AddExpression(constraint, columnName);
        }

        /// <summary>
        /// Adds text grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="constraint">The expressions that define the field.</param>
        /// <param name="columnName">The grid column name. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="autoSearch">When <c>true</c> triggers searching when the user presses `enter` key in the filter input.</param>
        public static void Add<T>(this IGridFieldCollection<T> collection, Expression<Func<T, string>> constraint,
                string columnName = null, bool readOnly = false, int? width = null, 
                bool autoSearch = false)
        {
            if (null == collection) throw new ArgumentNullException(nameof(collection));

            columnName ??= GetColumnName(constraint);

            var gridField = new TextGridField(
                name:     columnName,
                readOnly: readOnly,
                width:    width,
                autoSearch: autoSearch
            );

            collection.Add(gridField);
            collection.AddExpression(constraint, columnName);
        }

        public static void AddControl<T>(this IGridFieldCollection<T> collection)
        {
            // TODO: Implement this code.
        }

        private static string GetColumnName<T, TKey>(Expression<Func<T, TKey>> constraint)
        {
            if (null == constraint) throw new ArgumentNullException(nameof(constraint));

            var member = (MemberExpression)constraint.Body;
            return member.Member.Name;
        }
    }
}