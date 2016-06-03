using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class MamutPubRestaurantServiceTest:BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\MamutWholeWeekMenu.html");
            IRestaurantService service = new MamutPubRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("Mamut Pub", menuCard.RestaurantName);
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
            Assert.AreEqual(8, dayMenu.Foods.Count);

            AssertFood("pol&eacute;vka - MORAVSK&Aacute; CIBULAČKA S OP&Eacute;KAN&Yacute;M CHLEBEM", 0, dayMenu.Foods[0]);
            AssertFood("Kuřec&iacute; prsa marinovan&aacute; v bazalko-limetkov&eacute;m oleji, &scaron;ťouchan&eacute; brambory", 89, dayMenu.Foods[1]);
            AssertFood("Ď&aacute;belsk&aacute; vepřov&aacute; směs s fazolemi, americk&eacute; brambory", 89, dayMenu.Foods[2]);
            AssertFood("Rizoto s hověz&iacute;m masem a houbami sypan&eacute; parmesanem, okurek", 89, dayMenu.Foods[3]);
            AssertFood("Vepřov&aacute; pečeně &bdquo;PO SELSKU&ldquo; &scaron;pen&aacute;t, bramborov&eacute; noky s cibulkou", 89, dayMenu.Foods[4]);
            AssertFood("M&iacute;chan&yacute; zeleninov&yacute; sal&aacute;t, kuřec&iacute; medailonky, s&aacute;zen&eacute; vejce, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("&bdquo;KUŘEC&Iacute; FO&Scaron;NA&ldquo;(5ks zapečen&eacute; brambory s cibul&iacute;, slaninou a s&yacute;rem, dresing)", 89, dayMenu.Foods[6]);
            AssertFood("200g Vepřov&aacute; krkovička na grilu, barbecue om&aacute;čka, hranolky, tatarka", 119, dayMenu.Foods[7]);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 31), dayMenu.Date);
            Assert.AreEqual(9, dayMenu.Foods.Count);

            AssertFood("pol&eacute;vka - D&Yacute;ŇOV&Yacute; KR&Eacute;M S CORNFLAKES", 0, dayMenu.Foods[0]);
            AssertFood("Steak z lososa pečen&yacute; na čerstv&yacute;ch bylink&aacute;ch, op&eacute;kan&eacute; brambůrky", 89, dayMenu.Foods[1]);
            AssertFood("Vepřov&yacute; gyros, hranolky, tatarka", 89, dayMenu.Foods[2]);
            AssertFood("Kuřec&iacute; prsa zapečen&aacute; žampiony a mozarellou, bylinkov&aacute; r&yacute;že", 89, dayMenu.Foods[3]);
            AssertFood("Vepřov&aacute; plec na paprice, houskov&yacute; knedl&iacute;k", 89, dayMenu.Foods[4]);
            AssertFood("M&iacute;chan&yacute; zeleninov&yacute; sal&aacute;t, kuřec&iacute; prsa v těst&iacute;čku, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("&bdquo;KUŘEC&Iacute; FO&Scaron;NA&ldquo;(5ks zapečen&eacute; brambory s cibul&iacute;, slaninou a s&yacute;rem, dresing)", 89, dayMenu.Foods[6]);
            AssertFood("200g Vepřov&aacute; krkovička na grilu, barbecue om&aacute;čka, hranolky, tatarka", 119, dayMenu.Foods[7]);
            AssertFood("Vepřov&aacute; plec na paprice, těstoviny", 89, dayMenu.Foods[8]);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 1), dayMenu.Date);
            Assert.AreEqual(8, dayMenu.Foods.Count);

            AssertFood("pol&eacute;vka - SLEPIČ&Iacute; S MASEM A NUDLEMI", 0, dayMenu.Foods[0]);
            AssertFood("Smažen&eacute; kuřec&iacute; kuličky se s&yacute;rem v cornflakes, bramborov&aacute; ka&scaron;e", 89, dayMenu.Foods[1]);
            AssertFood("Vepřov&eacute; ražniči se &scaron;unkou, paprikou a slaninou, hranolky", 89, dayMenu.Foods[2]);
            AssertFood("Hověz&iacute; pečeně na houb&aacute;ch, r&yacute;že, okurek", 89, dayMenu.Foods[3]);
            AssertFood("&Scaron;pagety &bdquo;M&Aacute;RIO&ldquo; (kuřec&iacute; maso, &scaron;pen&aacute;t, česnek, smetana, parmesan)", 89, dayMenu.Foods[4]);
            AssertFood("M&iacute;chan&yacute; zeleninov&yacute; sal&aacute;t, filet z lososa na ro&scaron;tu, dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("&bdquo;ZBOJNICK&Yacute; TAL&Iacute;Ř&ldquo;(koleno, žebra, klob&aacute;s, křen, hořčice, okurek, feferony, chl&eacute;b)", 89, dayMenu.Foods[6]);
            AssertFood("200g Vepřov&aacute; krkovička na grilu, barbecue om&aacute;čka, hranolky, tatarka", 119, dayMenu.Foods[7]);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 2), dayMenu.Date);
            Assert.AreEqual(8, dayMenu.Foods.Count);

            AssertFood("pol&eacute;vka - UZEN&Aacute; S KROUPAMI A BRAMBOREM", 0, dayMenu.Foods[0]);
            AssertFood("Smažen&yacute; vepřov&yacute; ř&iacute;zek, bramborov&yacute; sal&aacute;t", 89, dayMenu.Foods[1]);
            AssertFood("Kuřec&iacute; medailonky s grilovanou zeleninou, op&eacute;kan&eacute; brambůrky", 89, dayMenu.Foods[2]);
            AssertFood("Zapečen&eacute; francouzsk&eacute; brambory s uzen&yacute;m masem a s&yacute;rem, okurek", 89, dayMenu.Foods[3]);
            AssertFood("Maďarsk&yacute; vepřov&yacute; gul&aacute;&scaron;, houskov&yacute; knedl&iacute;k", 89, dayMenu.Foods[4]);
            AssertFood("Sal&aacute;t &bdquo;CAESAR&ldquo; s grilovan&yacute;m kuřec&iacute;m masem, caesar dresing, pečivo", 89, dayMenu.Foods[5]);
            AssertFood("&bdquo;KUŘEC&Iacute; FO&Scaron;NA&ldquo;(5ks zapečen&eacute; brambory s cibul&iacute;, slaninou a s&yacute;rem, dresing)", 89, dayMenu.Foods[6]);
            AssertFood("200g Vepřov&aacute; krkovička na grilu, barbecue om&aacute;čka, hranolky, tatarka", 119, dayMenu.Foods[7]);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 6, 3), dayMenu.Date);
            Assert.AreEqual(8, dayMenu.Foods.Count);

            AssertFood("pol&eacute;vka - GUL&Aacute;&Scaron;OV&Aacute;", 0, dayMenu.Foods[0]);
            AssertFood("Pstruh &bdquo;PO MLYN&Aacute;ŘSKU&ldquo;,  &scaron;ťouchan&eacute; brambory se &scaron;pen&aacute;tem", 89, dayMenu.Foods[1]);
            AssertFood("Medailonky z vepřov&eacute; panenky v rokforov&eacute; om&aacute;čce, hranolky", 89, dayMenu.Foods[2]);
            AssertFood("Kuřec&iacute; prsa zapečen&aacute; s cherry rajčaty a s&yacute;rem, &scaron;unkov&aacute; r&yacute;že", 89, dayMenu.Foods[3]);
            AssertFood("Moravsk&eacute; uzen&eacute;, hrachov&aacute; ka&scaron;e, cibulka, okurek, chl&eacute;b", 89, dayMenu.Foods[4]);
            AssertFood("M&iacute;chan&yacute; zeleninov&yacute; sal&aacute;t s kuřec&iacute;m a krab&iacute;m masem, dresing", 89, dayMenu.Foods[5]);
            AssertFood("&bdquo;KUŘEC&Iacute; FO&Scaron;NA&ldquo; (5 ks zapečen&eacute; brambory s cibul&iacute;, slaninou a s&yacute;rem, dresing)", 89, dayMenu.Foods[6]);
            AssertFood("200g Vepřov&aacute; krkovička na grilu, barbecue om&aacute;čka, hranolky, tatarka", 119, dayMenu.Foods[7]);
        }
    }
}
