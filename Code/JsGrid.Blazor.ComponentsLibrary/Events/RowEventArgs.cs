namespace JsGrid.Blazor.ComponentsLibrary.Events
{
    public class RowEventArgs
    {
        /// <summary>
        /// Item index.
        /// </summary>
        public int ItemIndex { get; }
        /// <summary>
        /// JsGrid item.
        /// </summary>
        /// <remarks>
        /// Important! This item represents the jsGrid row but <c>not</c> a object this row is build from.
        /// </remarks>
        public object Item { get; }

        public RowEventArgs(int itemIndex, object item)
        {
            ItemIndex = itemIndex;
            Item      = item;
        }
    }
}