using AutoMapper;
using Recipes.Contracts;
using Recipes.Domain.Entities;
using Recipes.Domain.ValueObjects;

namespace Recipes.Application.Shared.Mapper
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Title))
                .ForMember(x => x.Ingredients, y => y.MapFrom(x => x.Ingredients))
                .ForMember(x => x.HashTags, y => y.MapFrom(x => x.Hashtags));


            CreateMap<Ingredient, IngredientDto>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Quantity, y => y.MapFrom(x => x.Quantity));

            CreateMap<Hashtag, HashtagDto>()
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Title));
        }
    }
}
