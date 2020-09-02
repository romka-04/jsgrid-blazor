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
        /// <param name="title">The grid column title. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="sorter">Column sorter.</param>
        /// <param name="align">Text align.</param>
        public static void Add<T, TKey>(this IGridFieldCollection<T> collection, Expression<Func<T, TKey>> constraint,
                string title = null, bool readOnly = false, int? width = null, 
                SortingEnum sorter = default, AlignEnum align = default)
            where TKey : struct, IComparable
        {
            if (null == collection) throw new ArgumentNullException(nameof(collection));

            // name of the field to get value in json object
            var name = GetPropertyName(constraint);

            var gridField = new NumberGridField(
                name: name, 
                title: title,
                readOnly: readOnly,
                width: width,
                sorter: sorter,
                align: align);

            collection.Add(gridField);
            collection.AddExpression(constraint, name);
        }

        /// <summary>
        /// Adds numerical grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="constraint">The expressions that define the field.</param>
        /// <param name="title">The grid column title. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="sorter">Column sorter.</param>
        /// <param name="align">Text align.</param>
        public static void Add<T>(this IGridFieldCollection<T> collection, Expression<Func<T, bool>> constraint,
                string title = null, bool readOnly = false, int? width = null, 
                SortingEnum sorter = default, AlignEnum align = default)
        {
            // name of the field to get value in json object
            var name = GetPropertyName(constraint);

            var gridField = new CheckboxGridField(
                name: name, 
                title: title,
                readOnly: readOnly,
                width: width,
                align: align
            );

            collection.Add(gridField);
            collection.AddExpression(constraint, name);
        }

        /// <summary>
        /// Adds text grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="constraint">The expressions that define the field.</param>
        /// <param name="title">The grid column title. Optional value.</param>
        /// <param name="readOnly">Defines whether input is readonly.</param>
        /// <param name="width">Column width. Default value is 50.</param>
        /// <param name="autoSearch">When <c>true</c> triggers searching when the user presses `enter` key in the filter input.</param>
        public static void Add<T>(this IGridFieldCollection<T> collection, Expression<Func<T, string>> constraint,
                string title = null, bool readOnly = false, int? width = null, 
                bool autoSearch = false)
        {
            if (null == collection) throw new ArgumentNullException(nameof(collection));

            // name of the field to get value in json object
            var name = GetPropertyName(constraint);

            var gridField = new TextGridField(
                name: name, 
                title: title,
                readOnly: readOnly,
                width: width,
                autoSearch: autoSearch
            );

            collection.Add(gridField);
            collection.AddExpression(constraint, name);
        }

        /// <summary>
        /// Adds control grid fields into the grid collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="collection">The collection to add a field.</param>
        /// <param name="editButton">show/hide edit button</param>
        /// <param name="deleteButton">show/hide delete button</param>
        /// <param name="clearFilterButton">show/hide clear filter button</param>
        /// <param name="modeSwitchButton">show/hide switching filtering/inserting button</param>
        /// <param name="align">content alignment</param>
        /// <param name="width">column width</param>
        /// <param name="filtering">disable/enable filtering for column</param>
        /// <param name="inserting">disable/enable inserting for column</param>
        /// <param name="editing">disable/enable editing for column</param>
        /// <param name="sorting">disable/enable sorting for column</param>
        public static void AddControl<T>(this IGridFieldCollection<T> collection, bool editButton = true,
            bool deleteButton = true, bool clearFilterButton = true, bool modeSwitchButton = true, 
            AlignEnum align = default, int? width = null, 
            bool filtering = false, bool inserting = false, bool editing = false, bool sorting = false
            )
        {
            var gridField = new ControlGridField(
                editButton,
                deleteButton,
                clearFilterButton,
                modeSwitchButton,
                align,
                width,
                filtering,
                inserting,
                editing,
                sorting
                );

            collection.Add(gridField);
        }

        private static string GetPropertyName<T, TKey>(Expression<Func<T, TKey>> constraint)
        {
            if (null == constraint) throw new ArgumentNullException(nameof(constraint));

            var member = (MemberExpression)constraint.Body;
            return member.Member.Name;
        }
    }
}