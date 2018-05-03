using System;
using System.IO;
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
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("Čočková polévka s cizrnou a kořenovou zeleninou", 39, dayMenu.Foods[0]);
            AssertFood("Kuřecí kaldoun s kořenovou zeleninou", 25, dayMenu.Foods[1]);
            AssertFood("Steak z kotlety s bylinkovým máslem a bramborovou kaší", 89, dayMenu.Foods[2]);
            AssertFood("Štěpánská hovězí pečeně s dušenou rýží", 99, dayMenu.Foods[3]);
            AssertFood("Pikantní Jalfrezi z batátů, květáku a manga s rýží basmati", 109, dayMenu.Foods[4]);
            AssertFood("Chřestový salát s brokolicí, pošírované vejce, bramborové purée", 159, dayMenu.Foods[5]);
            AssertFood("Tagliata z hovězího krku s chřestem, carpaccio z červené řepy, bazalkové pesto", 199, dayMenu.Foods[6]);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 7), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("Polévka z hlívy ústřičné", 39, dayMenu.Foods[0]);
            AssertFood("Celerový krém s chilli olejem", 25, dayMenu.Foods[1]);
            AssertFood("Kuřecí steak pečený na mátě s hráškovým bulgurem", 89, dayMenu.Foods[2]);
            AssertFood("Vepřová pečeně s medovou glazurou s mačkanými bramborami s petrželkou", 99, dayMenu.Foods[3]);
            AssertFood("Zapečené lilky s rajčaty a crémem fraiche, bylinková bageta", 109, dayMenu.Foods[4]);
            AssertFood("Chřestový salát s brokolicí, pošírované vejce, bramborové purée", 159, dayMenu.Foods[5]);
            AssertFood("Tagliata z hovězího krku s chřestem, carpaccio z červené řepy, bazalkové pesto", 199, dayMenu.Foods[6]);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 8), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("Kořenový krém s kurkumou", 39, dayMenu.Foods[0]);
            AssertFood("Hrachovka s uzeninou", 25, dayMenu.Foods[1]);
            AssertFood("Kuřecí steak marinovaný v bylinkách se zeleninovým kuskusem a tzatziky", 89, dayMenu.Foods[2]);
            AssertFood("Žebra pečená v B.B.Q. omáčce s grilovanou kukuřicí a šťouchaným bramborem", 99, dayMenu.Foods[3]);
            AssertFood("Pečená máslová dýně s mozzarellou a piniovými oříšky, míchaný salát", 109, dayMenu.Foods[4]);
            AssertFood("Chřestový salát s brokolicí, pošírované vejce, bramborové purée", 159, dayMenu.Foods[5]);
            AssertFood("Tagliata z hovězího krku s chřestem, carpaccio z červené řepy, bazalkové pesto", 199, dayMenu.Foods[6]);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 9), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("Pórkový krém se pažitkovým sýrem Gervais", 39, dayMenu.Foods[0]);
            AssertFood("Česnečka s vejcem a krutony", 25, dayMenu.Foods[1]);
            AssertFood("Kuřecí stehno pečené v tandoori s kořenovou zeleninou, hrášková rýže", 89, dayMenu.Foods[2]);
            AssertFood("Svíčková na smetaně s karlovarským knedlíkem", 99, dayMenu.Foods[3]);
            AssertFood("Zapečené rigatoni se sýrem", 109, dayMenu.Foods[4]);
            AssertFood("Chřestový salát s brokolicí, pošírované vejce, bramborové purée", 159, dayMenu.Foods[5]);
            AssertFood("Tagliata z hovězího krku s chřestem, carpaccio z červené řepy, bazalkové pesto", 199, dayMenu.Foods[6]);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 10), dayMenu.Date);
            Assert.AreEqual(7, dayMenu.Foods.Count);

            AssertFood("Gulášová polévka", 39, dayMenu.Foods[0]);
            AssertFood("Fazolová polévka dvou barev", 25, dayMenu.Foods[1]);
            AssertFood("Steak z kotlety se švestkovou omáčkou, šťouchané brambory s jarní cibulkou", 89, dayMenu.Foods[2]);
            AssertFood("Alsaský hrnec", 99, dayMenu.Foods[3]);
            AssertFood("Žemlovka s jablky a skořicí", 109, dayMenu.Foods[4]);
            AssertFood("Chřestový salát s brokolicí, pošírované vejce, bramborové purée", 159, dayMenu.Foods[5]);
            AssertFood("Tagliata z hovězího krku s chřestem, carpaccio z červené řepy, bazalkové pesto", 199, dayMenu.Foods[6]);
        }
    }
}
