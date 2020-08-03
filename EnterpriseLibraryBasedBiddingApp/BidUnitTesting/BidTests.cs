using BiddingExceptions;
using DataMapper.EntityFrameworkDataMapper;
using DomainModel;
using DomainModel.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Linq;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for BidValidation
    /// </summary>
    [TestClass]
    public class BidTests
    {
        private Bid _bid;
        private BidServices _bidServices;
        private CategoryServices _categoryServices;
        private ProductServices _productServices;
        private UserServices _userServices;
        private UserRatingServices _userRatingServices;
        public BidTests()
        {
            _bid = new Bid();
            _categoryServices = new CategoryServices();
            _bidServices = new BidServices();
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _userRatingServices = new UserRatingServices();
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
            _bid = new Bid(10, 10, 10);
            var res = Validation.Validate(_bid);
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

        [TestMethod]
        public void AddBidTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _productServices.AddProduct(product);
            var bid = new Bid(200, product.Id, user.Id);
            _bidServices.AddBid(bid);
            var bids = _bidServices.GetAllBids();
            var expected = bids.First(b => b.Id == bid.Id);
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void GetAllBidsTest()
        {
            Assert.IsNotNull(_bidServices.GetAllBids());
        }

        [TestMethod]
        public void BidSum0ValidationTest()
        {
            var bid = new Bid();
            bid.Sum = -10;
            TestHelper.AssertExpectedMessage(Resources.BidSumMustBeGreaterThan1Message, _bidServices.AddBid(bid));
        }

        [TestMethod]
        public void BidSum999999ValidationTest()
        {
            var bid = new Bid();
            bid.Sum = 98999999999;
            TestHelper.AssertExpectedMessage(Resources.BidSumMustBeSmallerThan999999Message, _bidServices.AddBid(bid));
        }

        [TestMethod]
        public void BidMustHaveUserTest()
        {
            var bid = new Bid();
            TestHelper.AssertExpectedMessage(Resources.BidMustHaveUser, _bidServices.AddBid(bid));
        }

        [TestMethod]
        public void BidMustHaveProductTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var bid = new Bid(10, 0, user.Id);
            TestHelper.AssertExpectedMessage(Resources.BidMustHaveProduct, _bidServices.AddBid(bid));
        }

        [TestMethod]
        public void UserIsBanndAndCannotBidTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 1m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            var bid = new Bid(200, 0, user.Id);
            TestHelper.AssertExpectedMessage(Resources.UserIsBanndAndCannotBid, _bidServices.AddBid(bid));
        }

        [TestMethod]
        public void CheckIfNewUserReceivesBidTest()
        {
            var bid = new Bid();
            bid.Id = 100;
            var user = new User();
            user.Id = 4;
            var newUser = new User();
            user.Id = 3;
            user.Bids.Add(bid);
            bid.User = newUser;
            Assert.IsTrue(newUser.Bids.Count == 1);
        }

        [TestMethod]
        public void InvalidBidAddTest()
        {
            Assert.IsTrue(_bidServices.AddBid(new Bid()).Count > 0);
        }

        [TestMethod]
        public void BidProductCheckIfNullOnChangeTest()
        {
            var bid = new Bid();
            var product = new Product();
            product.Id = 1;
            bid.Product = product;
            bid.ProductId = 3;
            Assert.IsNull(bid.Product);
        }

        [TestMethod]
        public void BidUserCheckIfNullOnChangeTest()
        {
            var bid = new Bid();
            var user = new User();
            user.Id = 1;
            bid.User = user;
            bid.UserId = 3;
            Assert.IsNull(bid.User);
        }

        [TestMethod]
        public void CheckIfBidChangesForUserSwapTest()
        {
            var bid = new Bid();
            bid.Id = 100;
            var user = new User();
            user.Id = 4;
            var newUser = new User();
            user.Id = 3;
            user.Bids.Add(bid);
            bid.User = newUser;
            Assert.IsTrue(user.Bids.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionBidAdd()
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Connection.Open();
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBid(new Bid());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionAddBidForProduct()
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Connection.Open();
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBidForProduct(new Bid(), new Product());
            }
        }

        [TestMethod]
        public void BidFactoryGetAll()
        {
            using (var context = new BiddingDataModelContainer())
            {
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory().BidFactory.GetAll();
            }
        }
    }
}