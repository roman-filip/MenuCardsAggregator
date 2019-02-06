using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
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
            IRestaurantService service = new UTrechCertuRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\UTrechCertuHalfWeekMenu_result.json");
            AdjustDates(expectedMenuCard);

            AssertMenuCard(expectedMenuCard, menuCard);
        }

        private void AdjustDates(MenuCard expectedMenuCard)
        {
            var date = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 3);
            expectedMenuCard.DayMenus.ForEach(dm =>
            {
                dm.Date = date;
                date = date.AddDays(1);
            });
        }
    }
}
