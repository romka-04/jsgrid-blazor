using System.Text.Json.Serialization;
using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public class BaseField
    {
        public string Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public JsGridType Type { get; set; }
        public int? Width { get; set; }
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

    public enum AlignEnum : byte
    {
        None,
        Right,
        Left,
        Center
    }

    public enum SortingEnum : byte
    {
        /// <summary>
        /// string sorter.
        /// </summary>
        [JsonValue("string")]
        String,
        /// <summary>
        /// Number sorter.
        /// </summary>
        [JsonValue("number")]
        Number,
        /// <summary>
        /// Date sorter.
        /// </summary>
        [JsonValue("date")]
        Date,
        /// <summary>
        /// Numbers are parsed before comparison.
        /// </summary>
        [JsonValue("numberAsString")]
        NumberAsString,
        /// <summary>
        /// Custom sorting strategy.
        /// </summary>
        [JsonValue("custom")]
        Custom
    }

    public class NumberField
        : BaseField
    {
        /// <summary>
        /// Sorting strategy.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortingEnum Sorter { get; set; }
        /// <summary>
        /// Text alignment.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AlignEnum Align { get; set; }
        /// <summary>
        /// A boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }
    }
    
    public class CheckboxField
        : BaseField
    {
        /// <summary>
        /// Text alignment.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public AlignEnum Align { get; set; }
        /// <summary>
        /// A boolean defines whether input is readonly (added in 'js-grid' v1.4)
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}