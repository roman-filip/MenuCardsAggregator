using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class IqRestaurantServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var data = File.ReadAllText(@"TestData\IqOneDayMenu.txt");
            var service = new IqRestaurantService(new HttpServiceMock(() => data));
            var menuCard = service.GetMenuCard();

            Assert.IsNotNull(menuCard);
        }
    }
}
