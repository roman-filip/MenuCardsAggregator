using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    public abstract class BaseRestaurantServiceTest
    {
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