using System.Linq;
using NUnit.Framework;

namespace Rebalancing.Import.Tests
{
    public class ImportTests
    {
        [Test]
        public void TestImport()
        {
            //Arrange
            var filepath = "/Users/mclark/git/Rebalancing/Rebalancing.Import.Tests/sample_account_data.csv";

            FidelityCsvImporter importer = new FidelityCsvImporter();

            // Act
            var transactions = importer.Import(filepath).ToList();

            // Assert
            var expectedBuyTransactions = 2;
            var expectedSellTransactions = 2;
            var expectedNoneTransactions = 5;

            Assert.AreEqual(expectedBuyTransactions, transactions.Count(x => x.Action == Core.Action.Buy));
            Assert.AreEqual(expectedSellTransactions, transactions.Count(x => x.Action == Core.Action.Sell));
            Assert.AreEqual(expectedNoneTransactions, transactions.Count(x => x.Action == Core.Action.None));
        }

        [Test]
        public void TestImportOriginalFile()
        {
            //Arrange
            var filepath = "/Users/mclark/git/Rebalancing/Rebalancing.Import.Tests/History_for_Account_sample.csv";

            FidelityCsvImporter importer = new FidelityCsvImporter();

            // Act
            var transactions = importer.Import(filepath).ToList();

            //    // Assert
            var expectedBuyTransactions = 2;
            var expectedSellTransactions = 2;
            var expectedNoneTransactions = 5;

            Assert.AreEqual(expectedBuyTransactions, transactions.Count(x => x.Action == Core.Action.Buy));
            Assert.AreEqual(expectedSellTransactions, transactions.Count(x => x.Action == Core.Action.Sell));
            Assert.AreEqual(expectedNoneTransactions, transactions.Count(x => x.Action == Core.Action.None));
        }

        [Test]
        public void TestGetAction()
        {
            // Arrange
            var buySample1 = "REINVESTMENT as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)";
            var buySample2 = "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)";
            var sellSample1 = "YOU SOLD TO BUY TBCIX FIDELITY LOW PRICED STOCK (FLPSX) (Cash)";
            var sellSample2 = "YOU SOLD EXCHANGE FIDELITY LOW PRICED STOCK (FLPSX) (Cash)";
            var noneSample1 = "DIVIDEND RECEIVED FIDELITY LOW PRICED STOCK (FLPSX) (Cash)";
            var noneSample2 = "PARTIC CONTR CURRENT PARTIC CONTRB CURRENER57148665 (Cash)";
            var noneSample3 = "SHORT-TERM CAP GAIN as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)";
            var noneSample4 = "LONG-TERM CAP GAIN as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)";
            var noneSample5 = "";
            string noneSample6 = null;

            // Act
            var buyResult1 = FidelityTransactionExtensions.GetAction(buySample1);
            var buyResult2 = FidelityTransactionExtensions.GetAction(buySample2);
            var sellResult1 = FidelityTransactionExtensions.GetAction(sellSample1);
            var sellResult2 = FidelityTransactionExtensions.GetAction(sellSample2);
            var noneResult1 = FidelityTransactionExtensions.GetAction(noneSample1);
            var noneResult2 = FidelityTransactionExtensions.GetAction(noneSample2);
            var noneResult3 = FidelityTransactionExtensions.GetAction(noneSample3);
            var noneResult4 = FidelityTransactionExtensions.GetAction(noneSample4);
            var noneResult5 = FidelityTransactionExtensions.GetAction(noneSample5);
            var noneResult6 = FidelityTransactionExtensions.GetAction(noneSample6);

            // Assert
            Assert.AreEqual(Core.Action.Buy, buyResult1);
            Assert.AreEqual(Core.Action.Buy, buyResult2);
            Assert.AreEqual(Core.Action.Sell, sellResult1);
            Assert.AreEqual(Core.Action.Sell, sellResult2);
            Assert.AreEqual(Core.Action.None, noneResult1);
            Assert.AreEqual(Core.Action.None, noneResult2);
            Assert.AreEqual(Core.Action.None, noneResult3);
            Assert.AreEqual(Core.Action.None, noneResult4);
            Assert.AreEqual(Core.Action.None, noneResult5);
            Assert.AreEqual(Core.Action.None, noneResult6);
        }

        [Test]
        public void TestPortfolio() {

            // Arrange

            // Act

            // Assert


        }




        //[Test]
        //public void TestToTransaction()
        //{
        //    // Arrange



        //    var line1 = "Run Date,Action,Symbol,Security Description,Security Type,Quantity,Price ($),Commission ($),Fees ($),Accrued Interest ($),Amount ($),Settlement Date";
        //    var line2 = " 12/15/2020, REINVESTMENT as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash), TBCIX, T ROWE PRICE BLUE CHIP GRWTH CL I,Cash,0.134,161.49,,,,-21.7,";
        //    var line3 = " 12/15/2020, YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash), TBCIX, T ROWE PRICE BLUE CHIP GRWTH CL I,Cash,0.575,162.72,,,,-93.58,12/16/2020";
        //    var line4 = " 12/14/2020, YOU SOLD TO BUY TBCIX FIDELITY LOW PRICED STOCK (FLPSX) (Cash), FLPSX, FIDELITY LOW PRICED STOCK,Cash,-0.151,47.25,,,,7.15,12/15/2020";
        //    var line5 = " 11/09/2020, YOU SOLD EXCHANGE FIDELITY LOW PRICED STOCK (FLPSX) (Cash), FLPSX, FIDELITY LOW PRICED STOCK,Cash,-0.368,46.69,,,,17.18,11/09/2020";
        //    var line6 = " 12/11/2020, DIVIDEND RECEIVED FIDELITY LOW PRICED STOCK (FLPSX) (Cash), FLPSX, FIDELITY LOW PRICED STOCK,Cash,,,,,,17.18,";
        //    var line7 = " 10/13/2020, PARTIC CONTR CURRENT PARTIC CONTRB CURRENER57148665 (Cash), , No Description,Cash,,,,,,287.49,";
        //    var line8 = " 12/15/2020, SHORT-TERM CAP GAIN as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash), TBCIX, T ROWE PRICE BLUE CHIP GRWTH CL I,Cash,,,,,,0.02,";
        //    var line9 = " 12/15/2020, LONG-TERM CAP GAIN as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash), TBCIX, T ROWE PRICE BLUE CHIP GRWTH CL I,Cash,,,,,,21.7,";







        //    var t1 = new FidelityTransaction();



        //    // Act



        //    // Assert

        //}
    }
}