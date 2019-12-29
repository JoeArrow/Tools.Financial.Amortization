using System;
using System.Web.Script.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tools.Financial.Amortization.Test
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for ArrowUnitTestXML1
    /// </summary>

    [TestClass]
    public class Tools_Financial_Amortization_Test
    {
        private string cr = $"{Environment.NewLine}";
        private string crt = $"{Environment.NewLine}\t";
        private JavaScriptSerializer m_ser = new JavaScriptSerializer();

        public Tools_Financial_Amortization_Test() { }

        // ------------------------------------------------
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the 
        ///     current test run.
        ///</summary>

        public TestContext TestContext { set; get; }

        // ------------------------------------------------

        #region Additional test attributes
#pragma warning disable S125

        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }

#pragma warning restore S125
        #endregion

        // ------------------------------------------------

        [TestMethod]
        [DataRow("{'AnnualTax':600,'LoanAmount':10000,'DownPayment':500," +
                  "'InterestRate':0.074,'NumberOfPayments':60,'AnnualInsurancePmt':250}", 260.74D)]

        [DataRow("{'AnnualTax':600,'LoanAmount':10000,'DownPayment':500," +
                  "'InterestRate':7.4,'NumberOfPayments':60,'AnnualInsurancePmt':250}", 260.74D)]
        public void CalculatePayment_AmortSvc_Returns_The_Expected_Payment_Amount(string reqJson, double exp)
        {
            // -------
            // Arrange

            var sut = new AmortSvc();
            var expected = Convert.ToDecimal(exp);
            var req = m_ser.Deserialize<CalculatePaymentRequest>(reqJson);

            // ---
            // Log

            Console.WriteLine($"Expected Response:{crt}{expected}{cr}");

            // ---
            // Act

            var resp = sut.CalculatePayment(req);

            // ---
            // Log

            Console.WriteLine($"Response:{crt}{resp.PaymentAmount}");

            // ------
            // Assert

            Assert.AreEqual(expected, resp.PaymentAmount);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("{'AlternatePaymentAmt':1000,'AlternatePaymentNo':10,'AnnualTax':600," +
                  "'LoanAmount':10000,'DownPayment':500,'InterestRate':0.074," +
                  "'NumberOfPayments':60,'AnnualInsurancePmt':250}", 19)]

        [DataRow("{'AlternatePaymentAmt':1000,'AlternatePaymentNo':10,'AnnualTax':600," +
                  "'LoanAmount':10000,'DownPayment':500,'InterestRate':7.4," +
                  "'NumberOfPayments':60,'AnnualInsurancePmt':250}", 19)]

        [DataRow("{'AlternatePaymentAmt':0,'AlternatePaymentNo':0,'AnnualTax':600," +
                  "'LoanAmount':10000,'DownPayment':500,'InterestRate':0.074," +
                  "'NumberOfPayments':60,'AnnualInsurancePmt':250}", 60)]
        public void CalculateAmortization_AmortSvc_Returns_The_Expected_Amortization_Schedule(string reqJson, int expected)
        {
            // -------
            // Arrange

            var sut = new AmortSvc();
            var req = m_ser.Deserialize<CalculateAmortizationRequest>(reqJson);

            // ---
            // Log

            Console.WriteLine($"Request:{crt}{reqJson}{cr}Expected Number of Payments:{crt}{expected}{cr}");

            // ---
            // Act

            var resp = sut.CalculateAmortization(req);

            // ---
            // Log

            Console.WriteLine($"Response:{crt}{resp.PaymentDetails.Count}{cr}");
            Console.WriteLine($"Payment Details:{crt}{resp.ToLog()}{crt}");

            // ------
            // Assert

            Assert.AreEqual(expected, resp.PaymentDetails.Count, "Expected Payment Count");
        }
    }
}