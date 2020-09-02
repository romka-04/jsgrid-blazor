namespace JsGrid.Blazor.ComponentsLibrary.Events
{
    public class ItemUpdatingEventArgs
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
        /// <summary>
        /// shallow copy (not deep copy) of item before editing.
        /// </summary>
        public object PreviousItem { get; }
        /// <summary>
        /// Whether update should be cancelled or not.
        /// </summary>
        /// <remarks>
        /// <c>true</c> means that update should be cancelled; otherwise <c>false</c>.
        /// </remarks>
        public bool Cancel { get; private set; }

        public ItemUpdatingEventArgs(int itemIndex, object item, object previousItem)
        {
            ItemIndex    = itemIndex;
            Item         = item;
            PreviousItem = previousItem;
        }

        /// <summary>
        /// Cancel update (this allows to do a validation before performing the actual update).
        /// </summary>
        public void CancelUpdate()
        {
            Cancel = true;
        }
    }
}