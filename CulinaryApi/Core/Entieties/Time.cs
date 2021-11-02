using System.Collections.Generic;

namespace CulinaryApi.Core.Entieties
{
    public class Time
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public virtual List<Recipe> Recipes { get; protected set; }
    }
}
