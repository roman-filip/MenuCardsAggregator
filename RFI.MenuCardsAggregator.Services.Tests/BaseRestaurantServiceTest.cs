using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    public abstract class BaseRestaurantServiceTest
    {
        protected void AssertMenuCard(MenuCard expectedMenuCard, MenuCard actualMenuCard)
        {
            Assert.IsNotNull(expectedMenuCard);
            Assert.IsNotNull(actualMenuCard);
            Assert.AreEqual(expectedMenuCard.RestaurantName, actualMenuCard.RestaurantName);
            Assert.AreEqual(expectedMenuCard.RestaurantUri, actualMenuCard.RestaurantUri);
            Assert.AreEqual(expectedMenuCard.MenuImageUri, actualMenuCard.MenuImageUri);

            AssertDayMenus(expectedMenuCard.DayMenus, actualMenuCard.DayMenus);
        }

        protected void AssertDayMenus(List<DayMenu> expectedDayMenus, List<DayMenu> actualDayMenus)
        {
            Assert.IsNotNull(expectedDayMenus);
            Assert.IsNotNull(actualDayMenus);
            Assert.AreEqual(expectedDayMenus.Count, actualDayMenus.Count);

            for (int i = 0; i < expectedDayMenus.Count; i++)
            {
                AssertDayMenu(expectedDayMenus[i], actualDayMenus[i]);
            }
        }

        protected void AssertDayMenu(DayMenu expectedDayMenu, DayMenu actualDayMenu)
        {
            Assert.IsNotNull(expectedDayMenu);
            Assert.IsNotNull(actualDayMenu);
            Assert.AreEqual(expectedDayMenu.Date, actualDayMenu.Date);

            AssertFoods(expectedDayMenu.Foods, actualDayMenu.Foods);
        }

        protected void AssertFoods(List<Food> expectedFoods, List<Food> actualFoods)
        {
            Assert.IsNotNull(expectedFoods);
            Assert.IsNotNull(actualFoods);
            Assert.AreEqual(expectedFoods.Count, actualFoods.Count);

            for (int i = 0; i < expectedFoods.Count; i++)
            {
                AssertFood(expectedFoods[i], actualFoods[i]);
            }
        }

        protected void AssertFood(Food expectedFood, Food actualFood)
        {
            Assert.IsNotNull(expectedFood);
            Assert.IsNotNull(actualFood);
            Assert.AreEqual(expectedFood.Name, actualFood.Name);
            Assert.AreEqual(expectedFood.Price, actualFood.Price);
            Assert.AreEqual(expectedFood.IsWeekFood, actualFood.IsWeekFood);
            Assert.AreEqual(expectedFood.Weight, actualFood.Weight);
        }

        protected MenuCard LoadExpectedMenuCard(string path)
        {
            var menuCard = JsonUtils.DeserializeFromJson<MenuCard>(File.ReadAllText(path));
            return menuCard;
        }

        // TODO - remove methods below
        protected void AssertFood(string expectedFoodName, decimal excpectedPrice, Food actualFood)
        {
            AssertFood(expectedFoodName, excpectedPrice, false, actualFood);
        }

        protected void AssertWeekFood(string expectedFoodName, decimal excpectedPrice, Food actualFood)
        {
            AssertFood(expectedFoodName, excpectedPrice, true, actualFood);
        }

        private void AssertFood(string expectedFoodName, decimal excpectedPrice, bool expectedIsWeekFood, Food actualFood)
        {
            Assert.AreEqual(expectedFoodName, actualFood.Name);
            Assert.AreEqual(excpectedPrice, actualFood.Price);
            Assert.AreEqual(expectedIsWeekFood, actualFood.IsWeekFood);
        }
    }
}