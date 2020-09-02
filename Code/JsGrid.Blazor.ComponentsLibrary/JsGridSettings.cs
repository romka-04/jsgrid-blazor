namespace JsGrid.Blazor.ComponentsLibrary
{
    public class JsGridSettings
    {
        public bool Inserting { get; set; }
        public bool Editing { get; set; }
        public bool Sorting { get; set; }
        public bool Paging { get; set; }

        public object[] Fields { get; set; }
        public object Data { get; set; }

        public string Width { get; set; }
        public string Height { get; set; }
    }
}