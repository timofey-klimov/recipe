﻿using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class IngredientRepository : AggregateRootRepository<Ingredient, int>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
