using System.Collections.Generic;

namespace CulinaryApi.Core.Entieties
{
    public class Meal
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public virtual List<Recipe> Recipes { get; protected set; }

        public Meal(string name)
        {
            Name = name;
        }
    }
}