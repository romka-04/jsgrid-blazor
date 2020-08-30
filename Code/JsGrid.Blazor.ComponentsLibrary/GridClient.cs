namespace JsGrid.Blazor.ComponentsLibrary
{
    class GridClient<T> 
        : IGridClient<T>
    {
        public BaseField[] Fields { get; set; }
    }
}