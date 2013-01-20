using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using DomainModel;
using DomainModel.Properties;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for UserTests
    /// </summary>
    [TestClass]
    public class UserTests
    {
        public UserTests()
        {
            _userServices = new UserServices();
            _roleServices = new RoleServices();
            _userRatingServices = new UserRatingServices();
            _bidServices = new BidServices();
            _categoryServices = new CategoryServices();
            _productServices = new ProductServices();
        }

        private TestContext testContextInstance;
        private UserServices _userServices;
        private RoleServices _roleServices;
        private UserRatingServices _userRatingServices;
        private BidServices _bidServices;
        private CategoryServices _categoryServices;
        private ProductServices _productServices;

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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void InvalidUserNameTest()
        {
            var user = new User();
            user.Name = "";
            TestHelper.AssertExpectedMessage(Resources.InvalidUserName, _userServices.AddUser(user));
        }

        [TestMethod]
        public void InvalidUserPasswordTest()
        {
            var user = new User();
            user.Password = "";
            TestHelper.AssertExpectedMessage(Resources.InvalidUserPassword, _userServices.AddUser(user));
        }

        [TestMethod]
        public void UserNameToLongTest()
        {
            var user = new User();
            user.Name = "123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs";
            TestHelper.AssertExpectedMessage(Resources.UserNameToLong, _userServices.AddUser(user));
        }

        [TestMethod]
        public void UserPasswordToLongTest()
        {
            var user = new User();
            user.Password = "123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs123asd1231wdfs";
            TestHelper.AssertExpectedMessage(Resources.UserPasswordToLong, _userServices.AddUser(user));
        }

        [TestMethod]
        public void UserHasNoSuchProductTest()
        {
            var user = new User();
            var product = new Product();
            TestHelper.AssertExpectedMessage(Resources.UserHasNoSuchProduct, _userServices.EndProductBiddingByUser(user, product));
        }

        [TestMethod]
        public void UserMustHaveARoleTest()
        {
            var user = new User();
            TestHelper.AssertExpectedMessage(Resources.UserMustHaveARole, _userServices.AddUser(user));
        }

        [TestMethod]
        public void AddUserTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("ionel", "ionel1", role);
            Assert.IsTrue(_userServices.AddUser(user).Count == 0);
        }

        [TestMethod]
        public void ChangeUserRatingCheckTest()
        {
            var user = new User();
            var newuser = new User();
            var ratings = new FixupCollection<UserRating> { new UserRating(), new UserRating() };
            ratings[0].User = user;
            ratings[1].User = newuser;
            user.UserRatings = ratings;
            Assert.AreEqual(1, newuser.UserRatings.Count);
            user.UserRatings.Clear();
        }

        [TestMethod]
        public void UserBidsChangedTest()
        {
            var bids = new FixupCollection<Bid> { new Bid(), new Bid() };
            var newbids = new FixupCollection<Bid> { new Bid(), new Bid() };
            var user = new User();
            user.Bids = bids;
            user.Bids = newbids;
            Assert.AreEqual(2, user.Bids.Count);
        }

        [TestMethod]
        public void CheckMultipleRolesForUser()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("ionel", "ionel1", role);
            var newuser = new User("ionel", "ionel1", role);
            _userServices.AddUser(user);
            _userServices.AddUser(newuser);
            var roles = _roleServices.GetAllRoles();
            Assert.AreEqual(2, roles.First(r => r.Id == role.Id).Users.Count);
        }

        [TestMethod]
        public void UserProductsChangedTest()
        {
            var products = new FixupCollection<Product> { new Product(), new Product() };
            var newproducts = new FixupCollection<Product> { new Product(), new Product() };
            var user = new User();
            user.Products = products;
            user.Products = newproducts;
        }

        [TestMethod]
        public void InvalidUserAddTest()
        {
            Assert.IsTrue(_userServices.AddUser(new User()).Count > 0);
        }

        [TestMethod]
        public void ChangeUserRoleWithOldRoleUpdatedTEst()
        {
            var user = new User();
            user.Role = new Role();
            user.RoleId = 2;
            Assert.IsNull(user.Role);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            Assert.IsNotNull(_userServices.GetAllUsers());
        }

        [TestMethod]
        public void UnBanInvalidUser()
        {
            Assert.IsFalse(_userServices.UnBan(0));
        }

        [TestMethod]
        public void UserIsNullWhenBidUserIdIsSetTest()
        {
            var bid = new Bid();
            var user = new User();
            user.Id = 1;
            bid.User = user;
            bid.UserId = 3;
            Assert.IsNull(bid.User);
        }

        [TestMethod]
        public void UserBidsResetIfBidUserChangesTEsst()
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
        public void RatingsBanUserTest()
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
        public void NewUserReceivesBidOnBidUserChangeTest()
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
        public void ProductBiddingEndByUser()
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
        public void TooManyProductInBidByUser()
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
        public void AddingProductForBiddingAfterAProductHasExpiredTest()
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
        public void UserAddingProductForBidding()
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
        public void ProductMovesIfUserChangedTest()
        {
            var User = new User();
            var newUser = new User();
            var product = new Product();
            product.Id = 2;
            User.Products.Add(product);
            product.User = newUser;
            Assert.IsTrue(newUser.Products.Count == 1);
        }

        [TestMethod]
        public void ProductExUserIsEmptyIfUserChangedTest()
        {
            var User = new User();
            var newUser = new User();
            var product = new Product();
            product.Id = 2;
            User.Products.Add(product);
            product.User = newUser;
            Assert.IsTrue(User.Products.Count == 0);
        }
    }
}
