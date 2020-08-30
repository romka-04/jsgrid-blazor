namespace JsGrid.Blazor.ComponentsLibrary
{
    public interface IGridClient<T>
    {
        BaseField[] Fields { get; set; }
    }
}