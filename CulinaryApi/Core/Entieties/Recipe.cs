using System;

namespace CulinaryApi.Core.Entieties
{
    public class Recipe : Identity
    {
        public string Name { get; protected set; }
        public string Grammar { get; protected set; }
        public string Execution { get; protected set; }
        public string Photo { get; protected set; }
        public string ShortDescription { get; protected set; }

        public int? CreateById { get; protected set; }
        public virtual User CreatedBy { get; protected set; }

        public int MealId { get; protected set; }
        public virtual Meal Meal { get; protected set; }
        public int CuisineId { get; protected set; }
        public virtual Cuisine Cuisine { get; protected set; }
        public int DifficultId { get; protected set; }
        public virtual Difficulty Difficult { get; protected set; }
        public int TimeId { get; protected set; }
        public virtual Time Time { get; protected set; }


        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException("Name cannot be empty");
        }

        public void SetGrammar(string grammar)
        {
            Grammar = grammar ?? throw new ArgumentNullException("Grammar cannot be empty");
        }

        public void SetExecution(string execution)
        {
            Execution = execution ?? throw new ArgumentNullException("Execution cannot be empty"); ;
        }

        public void SetPhoto(string photo)
        {
            Photo = photo ?? throw new ArgumentNullException("Photolink cannot be empty");
        }

        public void SetShortDescription(string shortDescription)
        {
            ShortDescription = shortDescription ?? throw new ArgumentNullException("Short description cannot be empty");
        }

        public void SetMealId(int mealId)
        {
            if(mealId <= 0)
            {
                throw new ArgumentOutOfRangeException($"Meal ID {mealId} cannot be lower than 1 ");   
            }

            MealId = mealId;
        }

        public void SetCuisineId(int cuisineId)
        {
            if (cuisineId <= 0)
            {
                throw new ArgumentOutOfRangeException($"Cuisine ID {cuisineId} cannot be lower than 1 ");
            }

            CuisineId = cuisineId;
        }

        public void SetDifficultId(int difficultId)
        {
            if (difficultId <= 0)
            {
                throw new ArgumentOutOfRangeException($"Difficulty ID {difficultId} cannot be lower than 1 ");
            }

            DifficultId = difficultId;
        }

        public void SetTimeId(int timeId)
        {
            if (timeId <= 0)
            {
                throw new ArgumentOutOfRangeException($"Time ID {timeId} cannot be lower than 1 ");
            }

            TimeId = timeId;
        }

        public void SetCreateBy(int? createById)
        {
            if (createById <= 0)
            {
                throw new ArgumentOutOfRangeException($"User ID {createById} cannot be lower than 1 ");
            }   

            CreateById = createById;
        }
    }
}
