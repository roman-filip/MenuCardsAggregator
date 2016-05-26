using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    [TestClass]
    public class IqRestaurantServiceTest : BaseRestaurantServiceTest
    {
        [TestMethod]
        public async Task OneDayMenuTest()
        {
            var data = File.ReadAllText(@"TestData\IqOneDayMenu.txt");
            IRestaurantService service = new IqRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("IQ Restaurant", menuCard.RestaurantName);
            Assert.AreEqual(1, menuCard.DayMenus.Count);
            AssertMondayFoods(menuCard.DayMenus[0]);
        }

        [TestMethod]
        public async Task WholeWeekMenuTest()
        {
            var data = File.ReadAllText(@"TestData\IqWholeWeekMenu.txt");
            IRestaurantService service = new IqRestaurantService(new HttpServiceMock(() => data));
            var menuCard = await service.GetMenuCardAsync();

            Assert.IsNotNull(menuCard);
            Assert.AreEqual("IQ Restaurant", menuCard.RestaurantName);
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
            Assert.AreEqual(18, dayMenu.Foods.Count);

            AssertFood("Polévka: 0,33l Podkrkonošské kyselo", 22, dayMenu.Foods[0]);
            AssertFood("Polévka: 0,33l Polévka z domácího kuřete a kukuřice", 22, dayMenu.Foods[1]);
            AssertFood("320G Špagety s rajčatovou omáčkou a slaninou, sypané parmazánem", 79, dayMenu.Foods[2]);
            AssertFood("320G Italské smetanové rizoto se zeleninou a kuřecím masem", 79, dayMenu.Foods[3]);
            AssertFood("PIZZA 1/4: UZENINOVÁ (tomato, sýr, šunka, čabajka, salám, cibule)", 49, dayMenu.Foods[4]);
            AssertFood("PIZZA: MEXICANA (tomato, sýr, slanina, kukuřice, fazole, mexické papričky jalapeňos)", 87, dayMenu.Foods[5]);
            AssertFood("100G Koprová omáčka, vařené vejce/hovězí vařené, brambor/knedlík", 79, dayMenu.Foods[6]);
            AssertFood("120G Krůtí steak, zeleninový salát s kuskusem", 84, dayMenu.Foods[7]);
            AssertFood("120G Marinovaní rošťáci z vepřového masa, bramborová kaše, okurka", 79, dayMenu.Foods[8]);
            AssertFood("150G Smažené rybí filé, bramborová kaše", 84, dayMenu.Foods[9]);
            AssertFood("320G Zapečený květák s rajčaty a sýrem gouda, brambory s máslem", 79, dayMenu.Foods[10]);

            AssertWeekMenu(dayMenu, 11);
        }

        private void AssertTuesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 24), dayMenu.Date);
            Assert.AreEqual(18, dayMenu.Foods.Count);

            AssertFood("Polévka: 0,33l Marocká harira", 22, dayMenu.Foods[0]);
            AssertFood("Polévka: 0,33l Hovězí vývar s masem a vejcem", 22, dayMenu.Foods[1]);
            AssertFood("320G Kuřecí stehna s cizrnou, italské těstoviny", 84, dayMenu.Foods[2]);
            AssertFood("320G Penne s kuřecím masem, francouzskou zeleninou a jogurtovo-smetanovou omáčkou", 79, dayMenu.Foods[3]);
            AssertFood("PIZZA 1/4: CALABRIANA (tomato, sýr, šunka)", 49, dayMenu.Foods[4]);
            AssertFood("PIZZA: GARGANO (smetana, herkules, slanina, jarní cibulka, kukuřice)POLÉVKA ZDARMA", 87, dayMenu.Foods[5]);
            AssertFood("320G Hrachová kaše s opečenou klobásou a vídeňskou cibulkou, chléb", 84, dayMenu.Foods[6]);
            AssertFood("120G Kuřecí prsa plněná ricotou , rukolový salát s ředkví, hranolky/rýže", 89, dayMenu.Foods[7]);
            AssertFood("120G Vepřová panenka se špenátem a mozzaellou v listovém těst, šťouchané brambory sypané sušenými rajčaty a balkánským sýrem", 95, dayMenu.Foods[8]);
            AssertFood("100G Štěpánská hovězí pečeně, rýže/hranolky", 82, dayMenu.Foods[9]);
            AssertFood("100G Smažená sýrová jehla, vařené brambory/steakové hranolky, tatarka", 84, dayMenu.Foods[10]);

            AssertWeekMenu(dayMenu, 11);
        }

        private void AssertWednesdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 25), dayMenu.Date);
            Assert.AreEqual(19, dayMenu.Foods.Count);

            AssertFood("Polévka: 0,33l Staročeská houbová omáčka s bramborami", 22, dayMenu.Foods[0]);
            AssertFood("Polévka: 0,33l Francouzská s mušličkami", 22, dayMenu.Foods[1]);
            AssertFood("100G Grilovaný steak z lososa, bazalková omáčka, it. těstoviny, parmazán", 99, dayMenu.Foods[2]);
            AssertFood("320G Gnocchi s houbovou omáčkou a kuřecím masem", 84, dayMenu.Foods[3]);
            AssertFood("PIZZA 1/4: HUNGARIAN (tomato, sýr,čabajka, cibule)", 49, dayMenu.Foods[4]);
            AssertFood("PIZZA: PRATALINO (tomato, mozzarella, šunka, žampiony, parmazán)", 99, dayMenu.Foods[5]);
            AssertFood("130G Znojemské biftečky se sázeným vejce, rýže/steakové hranolky", 84, dayMenu.Foods[6]);
            AssertFood("120G Smažená kuřecí prsa v parmazánovém těstíčku, bramborová kaše", 84, dayMenu.Foods[7]);
            AssertFood("120G Milánská hovězí pečeně, těstoviny/rýže", 82, dayMenu.Foods[8]);
            AssertFood("320G Risoto s kachním masem, zdobené rukolou", 89, dayMenu.Foods[9]);
            AssertFood("320G Papas con chille (opečené brambůrky se sýrem, chilli papričkami a zeleninou)", 79, dayMenu.Foods[10]);
            AssertFood("320G Švestkové knedlíky z bramborového těsta s cukrem a mákem", 79, dayMenu.Foods[11]);

            AssertWeekMenu(dayMenu, 12);
        }

        private void AssertThursdayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 26), dayMenu.Date);
            Assert.AreEqual(19, dayMenu.Foods.Count);

            AssertFood("Polévka: 0,33l Čočková polévka s párkem", 22, dayMenu.Foods[0]);
            AssertFood("Polévka: 0,33l Zeleninový vývar s celestýnskými nudlemi", 22, dayMenu.Foods[1]);
            AssertFood("320G Kuřecí lasagne (česnek, zelenina, pomodoro, bazalka)", 84, dayMenu.Foods[2]);
            AssertFood("320G Linguine s cuketou a chilli (česnek, chilli paprička, rajčata, bazalka)", 82, dayMenu.Foods[3]);
            AssertFood("PIZZA 1/4: CALABRESE (šunka, anglická, paprika, olivy)", 49, dayMenu.Foods[4]);
            AssertFood("PIZZA: CAPRICIOSA (tomato, šunka, žampiony)POLÉVKA ZDARMA", 87, dayMenu.Foods[5]);
            AssertFood("120G Vídeňský hovězí plátek, restovaná cibulka, bramborová kaše", 84, dayMenu.Foods[6]);
            AssertFood("240G Pečené kuřecí stehnýnko s mandlovou nádivkou, vařené brambory/rýže", 79, dayMenu.Foods[7]);
            AssertFood("300G Steak z vepřové krkovice s grilovanou cibulkou, pečená skýva chleba", 95, dayMenu.Foods[8]);
            AssertFood("100G Burrito de cerdo (kuřecí maso, omáčka mole poblano)", 79, dayMenu.Foods[9]);
            AssertFood("320G Smažené žampiony, vařený brambor, tatarská omáčka", 79, dayMenu.Foods[10]);
            AssertFood("150G Thajský salát s grilovaným kachním masem (kachní prsa, mix listových salátů, špenát, hrachové lusky, cherry rajčata, chilli papričky)", 119, dayMenu.Foods[11]);

            AssertWeekMenu(dayMenu, 12);
        }

        private void AssertFridayFoods(DayMenu dayMenu)
        {
            Assert.AreEqual(new DateTime(2016, 5, 27), dayMenu.Date);
            Assert.AreEqual(18, dayMenu.Foods.Count);

            AssertFood("Polévka: 0,33l Ukrajinský boršč", 25, dayMenu.Foods[0]);
            AssertFood("Polévka: 0,33l Kuřecí vývar s masem a skleněnými nudlemi", 22, dayMenu.Foods[1]);
            AssertFood("100G It. těstoviny s rukolovým pestem a kuřecími medailonky, parmazán", 84, dayMenu.Foods[2]);
            AssertFood("320G Melanzana siciliana (gril. lilek plněný směsí bolognese, perlové cibulky, zapečené parmazánem) špecle 3ks", 84, dayMenu.Foods[3]);
            AssertFood("PIZZA 1/4: FUMO PULCINO (smetana, uzený sýr, rajčata, olivy, šunka)", 49, dayMenu.Foods[4]);
            AssertFood("PIZZA: AL CAPONE (šunka, kuřecí prsa, hermelín, slanina)", 87, dayMenu.Foods[5]);
            AssertFood("120G Cikánská hovězí pečeně, rýže/brambory/těstoviny", 82, dayMenu.Foods[6]);
            AssertFood("150G Smažený kuřecí řízek XXL, bramborová kaše", 79, dayMenu.Foods[7]);
            AssertFood("120G Vepřový steak na grilu s česnekem a lutěnicí, steakové hranolky", 79, dayMenu.Foods[8]);
            AssertFood("130G Drůbeží játra na cibulce, rýže/hranolky", 76, dayMenu.Foods[9]);
            AssertFood("320G Zeleninové rizoto se žampiony, sýr gouda, salát", 79, dayMenu.Foods[10]);

            AssertWeekMenu(dayMenu, 11);
        }

        private void AssertWeekMenu(DayMenu dayMenu, int startIndex)
        {
            AssertWeekFood("100G Kuřecí kung-pao, nudle/rýže/steakové hranolky", 89, dayMenu.Foods[startIndex++]);
            AssertWeekFood("150G Pikantní mexický hamburger s papričkami jalapeňos, steakové hranolky, zelenina", 105, dayMenu.Foods[startIndex++]);
            AssertWeekFood("150G Marinovaný steak z tuňáka, vařené brambory, limetková omáčka", 139, dayMenu.Foods[startIndex++]);
            AssertWeekFood("150G Kuřecí flautas (dvě sm. tortilly plněné špenátem, sýrem, kuřecím, čínské zelí, rajčata)", 109, dayMenu.Foods[startIndex++]);
            AssertWeekFood("150G Steak z roštěné, špekové fazolky, česneková bageta", 139, dayMenu.Foods[startIndex++]);
            AssertWeekFood("150G Marinovaný kuřecí steak v zakysané smetaně, tandoori a česneku, podávané s francouzskou čočkou, steakové hranolky", 109, dayMenu.Foods[startIndex++]);
            AssertWeekFood("200G Kuřecí gyros, hranolky/čínské nudle/rýže, tzatziky", 99, dayMenu.Foods[startIndex]);
        }
    }
}
