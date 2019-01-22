using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class UTrechCertuRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\UTrechCertuHalfWeekMenu.html");
            //IRestaurantService service = new UTrechCertuRestaurantService(new HttpServiceMock(() => data));
            //var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\UTrechCertuHalfWeekMenu_result.json");

            //AssertMenuCard(expectedMenuCard, menuCard);
        }
    }
}
