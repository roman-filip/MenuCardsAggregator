using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
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

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("Na Tahu", menuCard.RestaurantName);
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
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("polévka - ČESNEČKA S OPÉKANÝM CHLEBEM", 0, dayMenu.Foods[0]);
            AssertFood("Smažený vepřový řízek s česnekem, brambory s máslem, okurek", 89, dayMenu.Foods[1]);
            AssertFood("Kuřecí prsa zapečená s čabajkou a sýrem, hranolky", 89, dayMenu.Foods[2]);
            AssertFood("Rizoto s kuřecím masem a zeleninou, strouhaný sýr, okurek", 89, dayMenu.Foods[3]);
            AssertFood("Moravský vepřový vrabec, zelí, houskový+bramborový knedlík", 89, dayMenu.Foods[4]);
            AssertFood("Míchaný zeleninový salát s tuňákem, šunkou a vejcem, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("1 ks (cca 1 kg) Pečené vepřové koleno, křen, hořčice, okurek, feferony, chléb", 139, dayMenu.Foods[6]);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 7), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("polévka - HOUBOVÁ S BRAMBOREM", 0, dayMenu.Foods[0]);
            AssertFood("Smažené kuřecí kuličky se sýrem v cornflakes, bramborová kaše", 89, dayMenu.Foods[1]);
            AssertFood("„DOMÁCÍ HAMBÁČ“ hranolky, tatarka", 89, dayMenu.Foods[2]);
            AssertFood("Špagety s pikantní kuřecí směsí a parmesanem", 89, dayMenu.Foods[3]);
            AssertFood("Hovězí guláš s cibulí, houskový knedlík", 89, dayMenu.Foods[4]);
            AssertFood("Míchaný zeleninový salát, Kuřecí prsa zapečená brokolicí a sýrem, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("1 ks (cca 1 kg) Pečené vepřové koleno, křen, hořčice, okurek, feferony, chléb", 139, dayMenu.Foods[6]);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 8), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("polévka - HRÁŠKOVÝ KRÉM", 0, dayMenu.Foods[0]);
            AssertFood("Steak z lososa s grilovanou zeleninou, šťouchané brambory se špenátem", 89, dayMenu.Foods[1]);
            AssertFood("Ďábelská kuřecí směs sypaná sýrem, bramboráčky", 89, dayMenu.Foods[2]);
            AssertFood("Znojemská hovězí pečeně, rýže", 89, dayMenu.Foods[3]);
            AssertFood("Vepřové maso v kapustě, brambory s cibulkou", 89, dayMenu.Foods[4]);
            AssertFood("Míchaný zeleninový salát s kuřecím masem, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("1 ks (cca 1 kg) Pečené vepřové koleno, křen, hořčice, okurek, feferony, chléb", 139, dayMenu.Foods[6]);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 9), dayMenu.Date);
            Assert.AreEqual(8, dayMenu.Foods.Count);

            AssertFood("polévka - HOVĚZÍ S MASEM A NUDLEMI", 0, dayMenu.Foods[0]);
            AssertFood("Smažený krůtí řízek, bramborový salát", 89, dayMenu.Foods[1]);
            AssertFood("Drůbeží játra na roštu, hranolky, tatarka", 89, dayMenu.Foods[2]);
            AssertFood("Kuřecí medailonky v bylinkovo-smetanové omáčce, žampionová rýže", 89, dayMenu.Foods[3]);
            AssertFood("Hovězí vařené maso, koprová omáčka, houskový knedlík", 89, dayMenu.Foods[4]);
            AssertFood("Míchaný zeleninový salát, kuřecí steak na grilu, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("1 ks (cca 1 kg) Pečené vepřové koleno, křen, hořčice, okurek, feferony, chléb", 139, dayMenu.Foods[6]);
            AssertFood("2ks Vařené vejce, koprová omáčka, vařené brambory", 89, dayMenu.Foods[7]);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 10), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("polévka - RUSKÝ BORŠČ", 0, dayMenu.Foods[0]);
            AssertFood("Smažený hermelín plněný vlašskými ořechy, brambory, tatarka", 89, dayMenu.Foods[1]);
            AssertFood("Maso „DVOU BAREV“ na grilu, sázené vejce, hranolky (kuřecí+vepřové)", 89, dayMenu.Foods[2]);
            AssertFood("Kuřecí závitek plněný brokolicí, žampiony, šunkou a mozarellou, rýže", 89, dayMenu.Foods[3]);
            AssertFood("„KATŮV ŠLEH“ americké brambory (pikantní vepřová směs)", 89, dayMenu.Foods[4]);
            AssertFood("Míchaný zeleninový salát, steak z lososa na roštu, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("1 ks (cca 1 kg) Pečené vepřové koleno, křen, hořčice, okurek, feferony, chléb", 139, dayMenu.Foods[6]);
        }
    }
}
