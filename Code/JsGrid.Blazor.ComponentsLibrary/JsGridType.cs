using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public enum JsGridType
    {
        /// <summary>
        /// simple text input
        /// </summary>
        [JsonValue("text")]
        Text,
        /// <summary>
        /// number input
        /// </summary>
        [JsonValue("number")]
        Number,
        /// <summary>
        /// select control
        /// </summary>
        [JsonValue("select")]
        Select,
        /// <summary>
        /// checkbox input
        /// </summary>
        [JsonValue("checkbox")]
        Checkbox,
        /// <summary>
        /// textarea control (renders textarea for inserting and editing and text input for filtering)
        /// </summary>
        [JsonValue("textarea")]
        Textarea,
        /// <summary>
        /// control field with delete and editing buttons for data rows, search and add buttons for filter and inserting row
        /// </summary>
        [JsonValue("control")]
        Control
    }
}