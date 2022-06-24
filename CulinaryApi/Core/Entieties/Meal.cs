namespace CulinaryApi.Core.Entieties
{
    public class Meal : Filter<Meal>
    {
        public Meal(string name) : base(name)
        { }
    }
}