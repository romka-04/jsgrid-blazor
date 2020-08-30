namespace JsGrid.Blazor.ComponentsLibrary
{
    public class BaseField
    {
        public string Name { get; set; }
        public JsGridType Type { get; set; }
        public int Width { get; set; }
    }

    public class TextField
        : BaseField
    {
        /// <summary>
        /// triggers searching when the user presses `enter` key in the filter input
        /// </summary>
        public bool AutoSearch { get; set; }
        /// <summary>
        /// a boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }
    }

    public class NumberField
        : BaseField
    {
        public enum AlignEnum : byte
        {
            Right,
            Left,
        }
        /// <summary>
        /// uses sorter for numbers
        /// </summary>
        public string Sorter { get; set; } = "number";
        /// <summary>
        /// text alignment
        /// </summary>
        public AlignEnum Align { get; set; }
        /// <summary>
        /// a boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}