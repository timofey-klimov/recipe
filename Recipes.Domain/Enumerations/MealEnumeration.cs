using Recipes.Domain.Core;

namespace Recipes.Domain.Enumerations
{
    public class MealEnumeration : Enumeration<MealEnumeration>
    {

        public static MealEnumeration Breakfast = new BreakfastMeal();

        public static MealEnumeration Dinner = new DinnerMeal();

        public static MealEnumeration Snack = new SnackMeal();

        public static MealEnumeration Evening = new EveningMeal();

        protected MealEnumeration(byte value, string name) 
            : base(value, name)
        {
        }

        private class BreakfastMeal : MealEnumeration
        {
            public BreakfastMeal() 
                : base(0, "Завтрак")
            {
            }
        }

        private class DinnerMeal : MealEnumeration
        {
            public DinnerMeal() 
                : base(1, "Обед")
            {
            }
        }

        private class SnackMeal : MealEnumeration
        {
            public SnackMeal()
                : base(2, "Полдник")
            {

            }
        }

        private class EveningMeal : MealEnumeration
        {
            public EveningMeal()
                : base(3, "Ужин")
            {

            }

        }
    }
}
