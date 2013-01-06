using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for BidValidation
    /// </summary>
    [TestClass]
    public class BidValidation
    {
        private Bid _bid;

        public BidValidation()
        {
            //
            // TODO: Add constructor logic here
            //
            _bid = new Bid();
        }

        #region Additional test attributes

        //
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
        //

        #endregion Additional test attributes

        [TestMethod]
        public void BidSumTest()
        {
            _bid.Sum = 99;
            var res = Validation.Validate<Bid>(_bid);
            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void BidSumLowerBoundryTest()
        {
            _bid.Sum = 0;
            var res = Validation.Validate(_bid);
            Assert.IsTrue(res.Count > 0);
        }

        [TestMethod]
        public void BidSumUpperBoundryTest()
        {
            _bid.Sum = 1000000;
            var res = Validation.Validate(_bid);
            Assert.IsTrue(res.Count > 0);
        }
    }
}