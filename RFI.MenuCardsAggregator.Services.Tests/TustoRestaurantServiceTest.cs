using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class TustoRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            // TODO - extract common code from other test classes to the base class
            var data = File.ReadAllText(@"TestData\TustoWholeWeekMenu.html");
            IRestaurantService service = new TustoRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            var expectedMenuCard = LoadExpectedMenuCard(@"TestData\TustoWholeWeekMenu_result.json");

            AssertMenuCard(expectedMenuCard, menuCard);
        }
    }
}
