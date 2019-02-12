using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class UEmilaRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task OneDayMenuTest()
        {
            var data = File.ReadAllText(@"TestData\UEmilaOneDayMenu.html");
            IRestaurantService service = new UEmilaRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\UEmilaOneDayMenu_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }

        [TestMethod]
        public async Task OneDayMenuTest2()
        {
            var data = File.ReadAllText(@"TestData\UEmilaOneDayMenu2.html");
            IRestaurantService service = new UEmilaRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\UEmilaOneDayMenu2_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }
    }
}
