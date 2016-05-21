using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class IqRestaurantServiceTest
    {
        [TestMethod]
        public void OneDayMenuTest()
        {
            var data = File.ReadAllText(@"TestData\IqOneDayMenu.txt");
            IRestaurantService service = new IqRestaurantService(new HttpServiceMock(() => data));
            var menuCard = service.GetMenuCard();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("IQ Restaurant", menuCard.RestaurantName);
            Assert.AreEqual(1, menuCard.DayMenus.Count);
            Assert.AreEqual(new DateTime(2016, 5, 23), menuCard.DayMenus[0].Date);

            Assert.AreEqual(18, menuCard.DayMenus[0].Foods.Count);

            AssertFood("Polévka: 0,33l Podkrkonošské kyselo", 22, false, menuCard.DayMenus[0].Foods[0]);
            AssertFood("Polévka: 0,33l Polévka z domácího kuřete a kukuřice", 22, false, menuCard.DayMenus[0].Foods[1]);
            AssertFood("320G Špagety s rajčatovou omáčkou a slaninou, sypané parmazánem", 79, false, menuCard.DayMenus[0].Foods[2]);
            AssertFood("320G Italské smetanové rizoto se zeleninou a kuřecím masem", 79, false, menuCard.DayMenus[0].Foods[3]);
            AssertFood("PIZZA 1/4: UZENINOVÁ (tomato, sýr, šunka, čabajka, salám, cibule)", 49, false, menuCard.DayMenus[0].Foods[4]);
            AssertFood("PIZZA: MEXICANA (tomato, sýr, slanina, kukuřice, fazole, mexické papričky jalapeňos)", 87, false, menuCard.DayMenus[0].Foods[5]);
            AssertFood("100G Koprová omáčka, vařené vejce/hovězí vařené, brambor/knedlík", 79, false, menuCard.DayMenus[0].Foods[6]);
            AssertFood("120G Krůtí steak, zeleninový salát s kuskusem", 84, false, menuCard.DayMenus[0].Foods[7]);
            AssertFood("120G Marinovaní rošťáci z vepřového masa, bramborová kaše, okurka", 79, false, menuCard.DayMenus[0].Foods[8]);
            AssertFood("150G Smažené rybí filé, bramborová kaše", 84, false, menuCard.DayMenus[0].Foods[9]);
            AssertFood("320G Zapečený květák s rajčaty a sýrem gouda, brambory s máslem", 79, false, menuCard.DayMenus[0].Foods[10]);
            AssertFood("100G Kuřecí kung-pao, nudle/rýže/steakové hranolky", 89, true, menuCard.DayMenus[0].Foods[11]);
            AssertFood("150G Pikantní mexický hamburger s papričkami jalapeňos, steakové hranolky, zelenina", 105, true, menuCard.DayMenus[0].Foods[12]);
            AssertFood("150G Marinovaný steak z tuňáka, vařené brambory, limetková omáčka", 139, true, menuCard.DayMenus[0].Foods[13]);
            AssertFood("150G Kuřecí flautas (dvě sm. tortilly plněné špenátem, sýrem, kuřecím, čínské zelí, rajčata)", 109, true, menuCard.DayMenus[0].Foods[14]);
            AssertFood("150G Steak z roštěné, špekové fazolky, česneková bageta", 139, true, menuCard.DayMenus[0].Foods[15]);
            AssertFood("150G Marinovaný kuřecí steak v zakysané smetaně, tandoori a česneku, podávané s francouzskou čočkou, steakové hranolky", 109, true, menuCard.DayMenus[0].Foods[16]);
            AssertFood("200G Kuřecí gyros, hranolky/čínské nudle/rýže, tzatziky", 99, true, menuCard.DayMenus[0].Foods[17]);
        }

        [TestMethod]
        public void WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\IqWholeWeekMenu.txt");
            IRestaurantService service = new IqRestaurantService(new HttpServiceMock(() => data));
            var menuCard = service.GetMenuCard();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("IQ Restaurant", menuCard.RestaurantName);
            Assert.AreEqual(5, menuCard.DayMenus.Count);
            Assert.AreEqual(new DateTime(2016, 5, 23), menuCard.DayMenus[0].Date);
            Assert.AreEqual(new DateTime(2016, 5, 24), menuCard.DayMenus[1].Date);
            Assert.AreEqual(new DateTime(2016, 5, 25), menuCard.DayMenus[2].Date);
            Assert.AreEqual(new DateTime(2016, 5, 26), menuCard.DayMenus[3].Date);
            Assert.AreEqual(new DateTime(2016, 5, 27), menuCard.DayMenus[4].Date);
        }

        private void AssertFood(string expectedFoodName, decimal excpectedPrice, bool expectedIsWeekFood, Food actualFood)
        {
            Assert.AreEqual(expectedFoodName, actualFood.Name);
            Assert.AreEqual(excpectedPrice, actualFood.Price);
            Assert.AreEqual(expectedIsWeekFood, actualFood.IsWeekFood);
        }
    }
}
