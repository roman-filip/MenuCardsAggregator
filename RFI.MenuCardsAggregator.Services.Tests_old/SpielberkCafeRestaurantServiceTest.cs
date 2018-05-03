using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class SpielberkCafeRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\SpielberkCafeWholeWeekMenu.html");
            IRestaurantService service = new SpielberkCafeRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("Spielberk café", menuCard.RestaurantName);
            Assert.AreEqual(5, menuCard.DayMenus.Count);

            AssertMondayFoods(menuCard.DayMenus[0]);
            AssertTuesdayFoods(menuCard.DayMenus[1]);
            AssertWednesdayFoods(menuCard.DayMenus[2]);
            AssertThursdayFoods(menuCard.DayMenus[3]);
            AssertFridayFoods(menuCard.DayMenus[4]);
        }

        private void AssertMondayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 30), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);

            AssertFood("Zeleninový krém (7,9)", 15, dayMenu.Foods[0]);
            AssertFood("120g Kuřecí steak s pepřovou omáčkou a pečenými bramborami (1,3,7)", 89, dayMenu.Foods[1]);
            AssertFood("120g Vepřový flamendr se zeleninou a bramboráčky (1,3,7,10)", 89, dayMenu.Foods[2]);
            AssertFood("300g Špenátové tagliatelle s kuřecími kousky, sušenými rajčaty, rukolou a sýrem Gran Moravia (1,3,7)", 89, dayMenu.Foods[3]);
            AssertFood("200g FILÍROVANÝ HOVĚZÍ FLANK STEAK podávaný s pečenými bramborami,     fazolovými lusky s karotkou a bylinkovou omáčkou (7)", 129, dayMenu.Foods[4]);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 31), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);


            // TODO - fix tuesday food count

            AssertFood("Drůbeží vývar s bylinkovým svítkem (1,3,9)", 15, dayMenu.Foods[0]);
            AssertFood("120g Plněný smažený kuřecí řízek poličanem a sýrem gouda s bramborovou kaší a pikantním dipem (1,3,7)", 89, dayMenu.Foods[1]);
            AssertFood("250g SLANÉ PALAČINKY plněné kuřecím masem, zeleninou a pečenou cibulkou, ochucené sýrem niva s mrkvovým salátkem", 89, dayMenu.Foods[2]);
            AssertFood("300g Jarní salátek s ředkvičkou, hroznovým vínem a sýrem NIVA s dresingem se šalotkou a francouzskou hořčicí,", 89, dayMenu.Foods[3]);
            AssertFood("plátky bagetky (1,3,7,10)", 15, dayMenu.Foods[4]);  // TODO
            AssertFood("150g Okoun pečený na másle, servírovaný se šafránovou omáčkou a grilovanými cherry rajčaty, šťouchané brambory (4,7)", 129, dayMenu.Foods[5]);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 1), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);

            AssertFood("Hrachová s osmaženým chlebem (1,3,9)", 15, dayMenu.Foods[0]);
            AssertFood("120g Dušené hovězí na česneku, provoněná majoránkou na kroupovém rizotu s restovanou kořenovou zeleninou (1,9)", 89, dayMenu.Foods[1]);
            AssertFood("300g Francouzské brambory s okurkovým salátkem (1,3,7,12)", 89, dayMenu.Foods[2]);
            AssertFood("300g Penne con pollo - kuřecí maso, rajčatové pesto, mladá cibulka, sýr Grana Padano (1,3,7)", 89, dayMenu.Foods[3]);
            AssertFood("200g Filovaná plněná vepřová panenka sýrem cheddar se šťouchanými bramborami a slaninovou omáčkou (7,12)", 129, dayMenu.Foods[4]);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 2), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);

            AssertFood("Hovězí vývar se zeleninou a nudlemi (1,3,9)", 15, dayMenu.Foods[0]);
            AssertFood("120g Kuřecí roláda plněná jarní nádivkou se silnou šťávou a jasmínovou rýží (1,3,9)", 89, dayMenu.Foods[1]);
            AssertFood("150g Chicken Korma (prsa,jogurt,mandle,citron,česnek,kurkuma,…) s rýží basmati (7)", 89, dayMenu.Foods[2]);
            AssertFood("100g Smažené mozzarellové prsty v bylinkové strouhance na listovém salátu s bazalkovo česnekovým dipem a bagetkou (1,3,7)", 89, dayMenu.Foods[3]);
            AssertFood("120g Tuňák na špízu - cherry rajčata, sezam, vlažný salát z restovaných fazolových lusků, cukety a brambor (4,7,11,12)", 129, dayMenu.Foods[4]);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 3), dayMenu.Date);
            Assert.AreEqual(5, dayMenu.Foods.Count);

            AssertFood("Řecká fasolada (9)", 15, dayMenu.Foods[0]);
            AssertFood("120g Filírovaný krůtí steak podávaný s tagliatelli a omáčkou Quatro Formagio (1,3,7)", 89, dayMenu.Foods[1]);
            AssertFood("300g Bavorské karbanátky (kýta, šunka, Eidam, niva, žampiony) s bramborovou kaší a okurkem (1,3,7)", 89, dayMenu.Foods[2]);
            AssertFood("300g Kuřecí kousky se smetanovým špenátem, česnekem a bramborovými noky (1,3,7)", 89, dayMenu.Foods[3]);
            AssertFood("200g Pečený pstruh na tagliatellovém lůžku s ořechovým pestem a cherry rajčaty (1,3,4)", 129, dayMenu.Foods[4]);
        }
    }
}
