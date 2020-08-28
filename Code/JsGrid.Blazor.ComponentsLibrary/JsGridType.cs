using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [JsonEnumConverter]
    public enum JsGridType
    {
        /// <summary>
        /// simple text input
        /// </summary>
        [JsonStringEnumMember("text")]
        Text,
        /// <summary>
        /// number input
        /// </summary>
        [JsonStringEnumMember("number")]
        Number,
        /// <summary>
        /// select control
        /// </summary>
        [JsonStringEnumMember("select")]
        Select,
        /// <summary>
        /// checkbox input
        /// </summary>
        [JsonStringEnumMember("checkbox")]
        Checkbox,
        /// <summary>
        /// textarea control (renders textarea for inserting and editing and text input for filtering)
        /// </summary>
        [JsonStringEnumMember("textarea")]
        Textarea,
        /// <summary>
        /// control field with delete and editing buttons for data rows, search and add buttons for filter and inserting row
        /// </summary>
        [JsonStringEnumMember("control")]
        Control
    }
}