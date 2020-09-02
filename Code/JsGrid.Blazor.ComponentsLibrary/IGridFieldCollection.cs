using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public interface IGridFieldCollection<T>
        : IEnumerable<IGridField>
    {
        IEnumerable<Func<T, object>> GetterList { get; }

        /// <summary>
        ///     Add new field to the grid collection
        /// </summary>
        void Add(IGridField field);

        void AddExpression<TKey>(Expression<Func<T, TKey>> constraint, string name);
    }

    public interface IGridField
    {
        public JsGridType Type { get; }
    }

    abstract class BaseGridField
        : IGridField
    {
        /// <summary>
        /// Name of the json object property.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Column title. Can be <c>null</c>.
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Column type. Defines the way to render the column.
        /// </summary>
        public JsGridType Type { get; }

        /// <summary>
        /// Column width.
        /// </summary>
        public int? Width { get; }

        protected BaseGridField(JsGridType type, string name, string title, int? width = null)
        {
            Type  = type;
            Name  = name;
            Title = title;
            Width = width;
        }
    }

    class NumberGridField
        : BaseGridField
    {
        /// <summary>
        /// uses sorter for numbers
        /// </summary>
        public SortingEnum Sorter { get; set; }
        /// <summary>
        /// text alignment
        /// </summary>
        public AlignEnum Align { get; set; }
        /// <summary>
        /// a boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }

        public NumberGridField(string name, string title, bool readOnly, int? width, SortingEnum sorter,
            AlignEnum align)
            : base(JsGridType.Number, name, title, width)
        {
            ReadOnly = readOnly;
            Sorter   = sorter;
            Align    = align;
        }
    }

    class TextGridField
        : BaseGridField
    {
        /// <summary>
        /// triggers searching when the user presses `enter` key in the filter input
        /// </summary>
        public bool AutoSearch { get; set; }
        /// <summary>
        /// a boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }

        public TextGridField(string name, string title, bool readOnly, int? width, bool autoSearch)
            : base(JsGridType.Text, name, title, width)
        {
            AutoSearch = autoSearch;
            ReadOnly   = readOnly;
        }
    }

    class CheckboxGridField
        : BaseGridField
    {
        /// <summary>
        /// text alignment
        /// </summary>
        public AlignEnum Align { get; set; }
        /// <summary>
        /// a boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }

        public CheckboxGridField(string name, string title, bool readOnly, int? width, AlignEnum align)
            : base(JsGridType.Checkbox, name, title, width)
        {
            ReadOnly = readOnly;
            Align    = align;
        }
    }

    class ControlGridField
        : IGridField
    {
        /// <summary>
        /// show/hide edit button
        /// </summary>
        public bool EditButton { get; set; }
        /// <summary>
        /// show/hide delete button
        /// </summary>
        public bool DeleteButton { get; set; }
        /// <summary>
        /// show/hide clear filter button
        /// </summary>
        public bool ClearFilterButton { get; set; }
        /// <summary>
        /// show/hide switching filtering/inserting button
        /// </summary>
        public bool ModeSwitchButton { get; set; }
        /// <summary>
        /// content alignment
        /// </summary>
        public AlignEnum Align { get; set; }
        /// <summary>
        /// column width
        /// </summary>
        public int? Width { get; set; }
        /// <summary>
        /// disable/enable filtering for column
        /// </summary>
        public bool Filtering { get; set; }
        /// <summary>
        /// disable/enable inserting for column
        /// </summary>
        public bool Inserting { get; set; }
        /// <summary>
        /// disable/enable editing for column
        /// </summary>
        public bool Editing { get; set; }
        /// <summary>
        /// disable/enable sorting for column
        /// </summary>
        public bool Sorting { get; set; }

        public JsGridType Type { get; } = JsGridType.Control;

        public ControlGridField(bool editButton, bool deleteButton, bool clearFilterButton, bool modeSwitchButton,
            AlignEnum align, int? width, 
            bool filtering, bool inserting, bool editing, bool sorting)
        {
            EditButton = editButton;
            DeleteButton = deleteButton;
            ClearFilterButton = clearFilterButton;
            ModeSwitchButton = modeSwitchButton;
            Align = align;
            Width = width;
            Filtering = filtering;
            Inserting = inserting;
            Editing = editing;
            Sorting = sorting;
        }
    }
}