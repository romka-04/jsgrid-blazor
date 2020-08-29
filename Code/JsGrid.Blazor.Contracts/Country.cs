namespace JsGrid.Blazor.Contracts
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region CTOR

        public Country()
        { }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}