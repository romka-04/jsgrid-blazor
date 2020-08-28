namespace JsGrid.Blazor.Contracts
{
    public class Client
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public bool Married { get; set; }

        #region CTOR

        public Client()
        { }

        public Client(string name, int age, int countryId, string address, bool married)
        {
            Name = name;
            Age = age;
            CountryId = countryId;
            Address = address;
            Married = married;
        }

        #endregion
    }
}