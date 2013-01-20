using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModel;
using ServiceLayer;
using System.Linq;
using DomainModel.Properties;
using System.Configuration;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for ProductBiddingTests
    /// </summary>
    [TestClass]
    public class ProductBiddingTests
    {
        private ProductServices _productServices;
        private UserServices _userServices;
        private CategoryServices _categoryServices;

        public ProductBiddingTests()
        {
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _categoryServices = new CategoryServices();
            _userRatingServices = new UserRatingServices();
        }

        private TestContext testContextInstance;
        private UserRatingServices _userRatingServices;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            ConfigurationManager.AppSettings["dataMapper"] = "ef";
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ProductBiddingFinishedByExpirationTest()
        {
            var product = new Product();
            var endDate = DateTime.Now.AddDays(7);
            product.BidEndDate = endDate;
            while (_productServices.IsProductAvailable(product))
            {
                product.BidEndDate = product.BidEndDate.AddDays(-1);
            }
            Assert.IsTrue(!product.Available);
        }

        [TestMethod]
        public void ProductBiddingFinishedByUserTest()
        {
            var product = new Product();
            product.StartingPrice = 100;
            product.BidEndDate = DateTime.Now.AddDays(1);
            product.AddBid(new Bid(110, 1, 1));
            var i = 5;
            while (product.Bids.Last().Sum < 212)
            {
                product.AddBid(new Bid(i += 5, 1, 1));
            }
            product.Available = false;
            Assert.IsTrue(!_productServices.IsProductAvailable(product));
        }

        [TestMethod]
        public void BiddingForNotAvailableProduct()
        {
            var product = new Product();
            product.AddBid(new Bid(10, 1, 1));
            product.Available = false;
            TestHelper.AssertExpectedMessage(Resources.ProductNotAvailableForBidding, product.AddBid(new Bid(20, 1, 1)));
        }

        [TestMethod]
        public void UserEndingProductBiddingTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            product.User = user;
            _productServices.AddProduct(product);
            var bid = new Bid(990, product.Id, user.Id);
            _productServices.AddBidForProduct(product, bid);
            _userServices.EndProductBiddingByUser(user, product);
            var users = _userServices.GetAllUsersWithBidsAndProducts();
            var expected = users.First(u => u.Id == user.Id);
            var expiredProduct = expected.Products.First(p => p.Id == product.Id);
            Assert.IsTrue(expiredProduct.Available == false);
        }

        [TestMethod]
        public void UserCantHaveSomeManyProductInBiddingTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var category2 = new Category("Elect");
            _categoryServices.AddCategory(category2);
            var product1 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            var product2 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category2.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            var product3 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _userServices.AddProductForBiddingByUser(user, product1);
            _userServices.AddProductForBiddingByUser(user, product2);
            _userRatingServices.AddUserRating(new UserRating(user.Id, 3.0m, "test"));
            _userServices.UpdateUserWithNavigationProperties(ref user);
            TestHelper.AssertExpectedMessage(Resources.UserCantHaveSomeManyProductInBidding, _userServices.AddProductForBiddingByUser(user, product3));
        }

        [TestMethod]
        public void UserAddingProductForBiddingAfterAProductHasExpiredTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var category2 = new Category("Elect");
            _categoryServices.AddCategory(category2);
            var category3 = new Category("Elect");
            _categoryServices.AddCategory(category3);
            var product1 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            var product2 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category2.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            var product3 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category3.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _userServices.AddProductForBiddingByUser(user, product1);
            _userServices.AddProductForBiddingByUser(user, product2);
            _userServices.UpdateUserWithNavigationProperties(ref user);
            _userServices.EndProductBiddingByUser(user, product2);
            _productServices.GetUpdatedProduct(ref product2);
            _userServices.UpdateUserWithNavigationProperties(ref user);
            Assert.IsTrue(_userServices.AddProductForBiddingByUser(user, product3).Count == 0);
        }

        [TestMethod]
        public void AddProductForBiddingByUserTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product1 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            Assert.IsTrue(_userServices.AddProductForBiddingByUser(user, product1).Count == 0);
        }

        [TestMethod]
        public void CategoryCantHaveSoManyProductInBdidding()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product1 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            var product2 = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _userServices.AddProductForBiddingByUser(user,product1);
            TestHelper.AssertExpectedMessage(Resources.CategoryCantHaveSoManyProductInBdidding, _userServices.AddProductForBiddingByUser(user, product2));
        }
    }
}
