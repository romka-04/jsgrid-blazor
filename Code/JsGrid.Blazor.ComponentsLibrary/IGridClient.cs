namespace JsGrid.Blazor.ComponentsLibrary
{
    public interface IGridClient<T>
    {
        public JsGridSettings Settings { get; set; }
    }

    class GridClient<T>
        : IGridClient<T>
    {
        public JsGridSettings Settings { get; set; }
    }
}