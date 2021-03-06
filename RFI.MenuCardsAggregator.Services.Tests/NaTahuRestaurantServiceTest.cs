﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class NaTahuRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\NaTahuWholeWeekMenu.html");
            IRestaurantService service = new NaTahuRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\NaTahuWholeWeekMenu_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }

        [TestMethod]
        public async Task WholeWeekMenuTest2()
        {
            var data = File.ReadAllText(@"TestData\NaTahuWholeWeekMenu2.html");
            IRestaurantService service = new NaTahuRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\NaTahuWholeWeekMenu2_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }
    }
}
