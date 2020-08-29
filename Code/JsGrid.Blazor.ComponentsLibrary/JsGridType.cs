namespace JsGrid.Blazor.ComponentsLibrary
{
    public enum JsGridType
    {
        /// <summary>
        /// simple text input
        /// </summary>
        Text,
        /// <summary>
        /// number input
        /// </summary>
        Number,
        /// <summary>
        /// select control
        /// </summary>
        Select,
        /// <summary>
        /// checkbox input
        /// </summary>
        Checkbox,
        /// <summary>
        /// textarea control (renders textarea for inserting and editing and text input for filtering)
        /// </summary>
        Textarea,
        /// <summary>
        /// control field with delete and editing buttons for data rows, search and add buttons for filter and inserting row
        /// </summary>
        Control
    }
}