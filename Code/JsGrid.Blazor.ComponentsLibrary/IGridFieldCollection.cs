﻿using System;
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
        void Add(IGridField field);

        ///// <summary>
        /////     Add new field to the grid collection
        ///// </summary>
        ///// <param name="constraint">Member of generic class</param>
        //void Add<TKey>(Expression<Func<T, TKey>> constraint);

        ///// <summary>
        /////     Add new field to the grid collection
        ///// </summary>
        ///// <param name="constraint">Member of generic class</param>
        ///// <param name="columnName">Specify field internal static name, used for sorting and filtering</param>
        //void Add<TKey>(Expression<Func<T, TKey>> constraint, string columnName = null);
    }

    class GridFieldCollection<T> 
        : IGridFieldCollection<T>
    {
        private readonly List<IGridField> _list = new List<IGridField>();

        public void Add(IGridField field)
        {
            if (null == field) throw new ArgumentNullException(nameof(field));
            _list.Add(field);
        }

        public void Add<TKey>(Expression<Func<T, TKey>> constraint)
        {
            throw new NotImplementedException();
        }

        public void Add<TKey>(Expression<Func<T, TKey>> constraint, string columnName)
        {
            throw new NotImplementedException();
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
        public int? Width { get; }

        protected BaseGridField(string name, JsGridType type, int? width = null)
        {
            Name  = name;
            Type  = type;
            Width = width;
        }

        public abstract BaseField BuildField();
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

        public NumberGridField(string name, bool readOnly, int? width, SortingEnum sorter, AlignEnum align)
            : base(name, JsGridType.Number, width)
        {
            ReadOnly = readOnly;
            Sorter   = sorter;
            Align    = align;
        }

        public override BaseField BuildField()
        {
            return new NumberField
            {
                Name     = this.Name,
                ReadOnly = this.ReadOnly,
                Width    = this.Width, 
                Sorter   = this.Sorter,
                Align    = this.Align
            };
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

        public TextGridField(string name, bool autoSearch, bool readOnly, int? width)
            : base(name, JsGridType.Text, width)
        {
            AutoSearch = autoSearch;
            ReadOnly   = readOnly;
        }

        public override BaseField BuildField()
        {
            return new TextField
            {
                Name       = this.Name,
                ReadOnly   = this.ReadOnly,
                AutoSearch = this.AutoSearch,
                Width      = this.Width 
            };
        }
    }
}