using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class MamutPubRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\MamutWholeWeekMenu.html");
            IRestaurantService service = new MamutPubRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\MamutWholeWeekMenu_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }
    }
}
