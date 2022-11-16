using Recipes.Domain.Core;

namespace Recipes.Domain.Enumerations
{
    public class MealEnumeration : Enumeration<MealEnumeration>
    {
        public MealEnumeration Salad = new SaladMeal();
        public MealEnumeration Soup = new SoupMeal();
        public MealEnumeration Meat = new MeatMeal();
        public MealEnumeration Breakfast = new BreakfastMeal();
        public MealEnumeration SideDishes = new SideDishesMeal();
        public MealEnumeration Bakery = new BakeryMeal();
        public MealEnumeration Dessert = new DessertMeal();
        public MealEnumeration Pickless = new PicklessMeal();

        protected MealEnumeration(byte value, string name) 
            : base(value, name)
        {
        }

        private class SaladMeal : MealEnumeration
        {
            public SaladMeal()
                : base(0, "Салат")
            {

            }
        }

        private class SoupMeal : MealEnumeration
        {
            public SoupMeal()
                : base(1, "Суп")
            {

            }
        }

        private class MeatMeal : MealEnumeration
        {
            public MeatMeal()
                : base(2, "Мясные блюда")
            {

            }
        }

        private class BreakfastMeal : MealEnumeration
        {
            public BreakfastMeal()
                : base(3, "Завтрак/перекус")
            {

            }
        }

        private class SideDishesMeal : MealEnumeration
        {
            public SideDishesMeal()
                : base(4, "Гарниры")
            {

            }
        }

        private class BakeryMeal : MealEnumeration
        {
            public BakeryMeal()
                : base(5, "Выпечка")
            {

            }
        }
        
        private class DessertMeal : MealEnumeration
        {
            public DessertMeal()
                : base(6, "Десерты")
            {

            }
        }

        private class PicklessMeal : MealEnumeration
        {
            public PicklessMeal()
                : base(7, "Соления")
            {

            }
        }

    }
}
