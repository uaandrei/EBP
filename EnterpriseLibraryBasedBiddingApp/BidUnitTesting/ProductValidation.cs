using DomainModel.Properties;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ServiceLayer;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for ProductValidation
    /// </summary>
    [TestClass]
    public class ProductValidation
    {
        private Product _product;
        private ProductServices _productServices;

        public ProductValidation()
        {
            //
            // TODO: Add constructor logic here
            //
            _product = new Product();
            _productServices = new ProductServices();
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
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _product.Bids.Clear();
        }
        //

        #endregion Additional test attributes

        private void AssertExpectedMessage(string expectedMessage)
        {
            var success = false;
            foreach (var item in _productServices.AddProduct(_product))
            {
                if (item.Message == expectedMessage)
                {
                    success = true;
                    break;
                }
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void ProductBidEndDateIsGreaterThanBidStartDateTest()
        {
            _product.BidStartDate = DateTime.Now;
            _product.BidEndDate = DateTime.Now.AddDays(-1);
            AssertExpectedMessage(Resources.ProductEndDateMustBeGreaterThanStartDateMessage);
        }

        [TestMethod]
        public void ProductBidStartDateCantBeInPastTimeTest()
        {
            _product.BidStartDate = DateTime.Now.AddDays(-1);
            AssertExpectedMessage(Resources.ProductStartDateCantBeSetInPastTimeMessage);
        }

        [TestMethod]
        public void ProductBidEndDateCantBeInPastTimeTest()
        {
            _product.BidEndDate = DateTime.Now.AddDays(-1);
            AssertExpectedMessage(Resources.ProductEndDateCantBeSetInPastTimeMessage);
        }

        [TestMethod]
        public void CannotAddBidIfSumIsLowerThanProductStartingPriceTest()
        {
            _product.StartingPrice = 110;
            var bid = new Bid(100);
            Assert.AreEqual(Resources.BidSumMustBeGreaterThanProductStartingPrice, _productServices.AddBidForProduct(_product, bid).Message);
        }

        [TestMethod]
        public void BidForProductTest()
        {
            _product.StartingPrice = 90;
            var bid = new Bid(99);
            Assert.AreEqual(string.Empty, _productServices.AddBidForProduct(_product,bid).Message);
        }

        [TestMethod]
        public void CannotSetNegativeStartingPriceForProduct()
        {
            _product.StartingPrice = -10;
            AssertExpectedMessage(Resources.ProductStartingPriceMustBeGreaterThanZeroMessage);
        }

        [TestMethod]
        public void ProductStartingPriceCannotBeHigherThan999999Test()
        {
            _product.StartingPrice = 1000000;
            AssertExpectedMessage(Resources.ProductStartingPriceMustBeLowerThan1000000Message);
        }
    }
}