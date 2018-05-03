using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
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
            Assert.AreEqual("Kometa Pub", menuCard.RestaurantName);
            Assert.AreEqual(5, menuCard.DayMenus.Count);

            AssertMondayFoods(menuCard.DayMenus[0]);
            AssertTuesdayFoods(menuCard.DayMenus[1]);
            AssertWednesdayFoods(menuCard.DayMenus[2]);
            AssertThursdayFoods(menuCard.DayMenus[3]);
            AssertFridayFoods(menuCard.DayMenus[4]);
        }

        private void AssertMondayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 23), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);

            AssertFood("POLÉVKA: Uzená s kroupami a zeleninou", 0, dayMenu.Foods[0]);
            AssertFood("Domažlické vepřové ragú s dušenou rýží", 84, dayMenu.Foods[1]);
            AssertFood("Kuřecí závitek s listovým špenátem, sušenými rajčaty a mozzarellou s vař. bramborem", 84, dayMenu.Foods[2]);
            AssertFood("Smažený hermelín s hranolkami a tatarkou", 84, dayMenu.Foods[3]);
            AssertFood("Zeleninový salát s vejcem, šunkou a sýrem, americký dresink, pečivo", 84, dayMenu.Foods[4]);
            AssertFood("200g Vykostěný vepřový kotlet na grilu s opečenou slaninou, sázeným vejcem a mačkaným bramborem s cibulkou", 115, dayMenu.Foods[5]);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 24), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);

            AssertFood("POLÉVKA: Květáková", 0, dayMenu.Foods[0]);
            AssertFood("Italské špagety ,,Ala Carbonara“ sypané sýrem", 84, dayMenu.Foods[1]);
            AssertFood("Segedínský guláš ,, Special“ s houskovým knedlíkem", 84, dayMenu.Foods[2]);
            AssertFood("Dřevorubecký vepřový steak s americkým bramborem", 84, dayMenu.Foods[3]);
            AssertFood("Kuřecí steak s rajčatovým concase a zeleninovým kuskusem", 84, dayMenu.Foods[4]);
            AssertFood("Řecký gyros marinovaný v česneku s hranolkami a tzatziki", 95, dayMenu.Foods[5]);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 25), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);

            AssertFood("POLÉVKA: Fazolová", 0, dayMenu.Foods[0]);
            AssertFood("Smažené žampiony s vařeným bramborem a tatarkou", 84, dayMenu.Foods[1]);
            AssertFood("Obalovaný losos s medem a hořčicí s bramborovou kaší", 84, dayMenu.Foods[2]);
            AssertFood("Pikantní kuřecí nudličky s dušenou rýží", 84, dayMenu.Foods[3]);
            AssertFood("Těstovinový salát s kuřecím masem, zeleninou, jogurtovým dresinkem a pečivem", 84, dayMenu.Foods[4]);
            AssertFood("Svíčková na smetaně s houskovým knedlíkem", 95, dayMenu.Foods[5]);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 26), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);

            AssertFood("POLÉVKA: Italská s těstovinou", 0, dayMenu.Foods[0]);
            AssertFood("Smažený holandský řízek se sýrem, bramborovou kaší a okurkou", 84, dayMenu.Foods[1]);
            AssertFood("Kuřecí prsa na grilu s dušenou rýží", 84, dayMenu.Foods[2]);
            AssertFood("Pikantní kovbojské fazole s grilovanou klobásou, chléb", 84, dayMenu.Foods[3]);
            AssertFood("Zeleninový salát se smaženými tvarůžkami, medovo-hořčičný dresink a pečivo", 84, dayMenu.Foods[4]);
            AssertFood("Jelení gulášek s cibulkou a houskovým knedlíkem", 105, dayMenu.Foods[5]);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 27), dayMenu.Date);
            Assert.AreEqual(6, dayMenu.Foods.Count);

            AssertFood("POLÉVKA: Kulajda", 0, dayMenu.Foods[0]);
            AssertFood("Smažená kuřecí kapsa se šunkou a sýrem, šťouchaný bramborem", 84, dayMenu.Foods[1]);
            AssertFood("Labužnická vepřová játra s dušenou rýží", 84, dayMenu.Foods[2]);
            AssertFood("Komeťácký flamendr s bramboráčky", 84, dayMenu.Foods[3]);
            AssertFood("Lívanečky s medovo-brusinkovou omáčkou, tvarohem a cukrem", 84, dayMenu.Foods[4]);
            AssertFood("Pečené kachní stehno s červeným zelím, 1/2 bramborový a 1/2houskový", 115, dayMenu.Foods[5]);
        }
    }
}
