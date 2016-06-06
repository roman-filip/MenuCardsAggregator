using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class MyFoodRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\MyFoodWholeWeekMenu.html");
            IRestaurantService service = new MyFoodRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("My Food", menuCard.RestaurantName);
            Assert.AreEqual(5, menuCard.DayMenus.Count);

            AssertMondayFoods(menuCard.DayMenus[0]);
            AssertTuesdayFoods(menuCard.DayMenus[1]);
            AssertWednesdayFoods(menuCard.DayMenus[2]);
            AssertThursdayFoods(menuCard.DayMenus[3]);
            AssertFridayFoods(menuCard.DayMenus[4]);
        }

        private void AssertMondayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 6), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 7), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 8), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 9), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 10), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);
        }
    }
}
