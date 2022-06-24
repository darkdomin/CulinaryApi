using System;
using System.Collections.Generic;

namespace CulinaryApi.Core.Entieties
{
    public abstract class Filter<T> : Identity
    {
        public string Name { get; protected set; }

        public virtual List<Recipe> Recipes { get; protected set; }


        public Filter(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException("Name cannot be empty");
        }
    }
}
