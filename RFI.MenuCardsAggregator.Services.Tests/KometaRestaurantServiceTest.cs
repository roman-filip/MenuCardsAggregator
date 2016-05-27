using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class KometaRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\KometaWholeWeekMenu.html");
            IRestaurantService service = new KometaRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("IQ Restaurant", menuCard.RestaurantName);
            Assert.AreEqual(5, menuCard.DayMenus.Count);
        }
    }
}
