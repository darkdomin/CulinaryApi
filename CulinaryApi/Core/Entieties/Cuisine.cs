namespace CulinaryApi.Core.Entieties
{
    public class Cuisine : Filter<Cuisine>
    {
        public Cuisine(string name) : base(name)
        { }
    }
}
