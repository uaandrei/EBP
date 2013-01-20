using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModel;
using ServiceLayer;
using DomainModel.Properties;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for UserRatingTests
    /// </summary>
    [TestClass]
    public class UserRatingTests
    {
        public UserRatingTests()
        {
            _userRatingServices = new UserRatingServices();
            _userServices = new UserServices();
            _categoryServices = new CategoryServices();
            _productServices = new ProductServices();
            _roleServices = new RoleServices();
        }

        private TestContext testContextInstance;
        private UserRatingServices _userRatingServices;
        private UserServices _userServices;
        private CategoryServices _categoryServices;
        private ProductServices _productServices;
        private RoleServices _roleServices;

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
        public void RatingMustHaveAssociatedUserTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(0, 20, "bla bla not good");
            TestHelper.AssertExpectedMessage(Resources.RatingMustHaveAssociatedUser, _userRatingServices.AddUserRating(userRating));
        }

        [TestMethod]
        public void RatingMustBePositiveNumberTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(0, -10, "bla bla not good");
            TestHelper.AssertExpectedMessage(Resources.RatingMustBePositiveNumber, _userRatingServices.AddUserRating(userRating));
        }

        [TestMethod]
        public void RatingCannotBeMoreThan10Test()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(0, 20, "bla bla not good");
            TestHelper.AssertExpectedMessage(Resources.RatingCannotBeMoreThan10, _userRatingServices.AddUserRating(userRating));
        }

        [TestMethod]
        public void RatingMustContainADescriptionTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(0, 20, "");
            TestHelper.AssertExpectedMessage(Resources.RatingMustContainADescription, _userRatingServices.AddUserRating(userRating));
        }

        [TestMethod]
        public void RatingDescriptionToLongTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(0, 20, "55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc55511aabbc");
            TestHelper.AssertExpectedMessage(Resources.RatingDescriptionToLong, _userRatingServices.AddUserRating(userRating));
        }

        [TestMethod]
        public void AddUserRatingTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(user.Id, 6.5m, "Not bad, but could have been better");
            Assert.IsTrue(_userRatingServices.AddUserRating(userRating).Count == 0);
        }

        [TestMethod]
        public void GetUserDefaultRatingTest()
        {
            var user = new User();
            var ratings = _userRatingServices.GetRatingsForUser(user.Id);
            foreach (var rating in ratings)
            {
                if (rating.Rating == UserRating.DefaultRating.Rating && rating.Id == 0)
                {
                    return;
                }
            }
            Assert.Fail("User does not have a default rating.");
        }

        [TestMethod]
        public void MultipleUserRatingTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 6.5m, "Not bad, but could have been better");
            var userRating2 = new UserRating(user.Id, 6.5m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            _userRatingServices.AddUserRating(userRating2);
            Assert.IsTrue(_userRatingServices.GetRatingsForUser(user.Id).Count == 2);
        }

        [TestMethod]
        public void AverageRatingForUserTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 6.5m, "Not bad, but could have been better");
            var userRating2 = new UserRating(user.Id, 6.3m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            _userRatingServices.AddUserRating(userRating2);
            var averageRating = _userRatingServices.GetAverageRatingForUser(user.Id);
            Assert.AreEqual(6.4m, averageRating);
        }

        [TestMethod]
        public void UserIsBannedAndCannotAddTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 1m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _productServices.AddProduct(product);
            user = _userServices.GetUser(user.Id);
            TestHelper.AssertExpectedMessage(Resources.UserIsBanned, _userServices.AddProductForBiddingByUser(user, product));
        }

        [TestMethod]
        public void BanUserByRatingTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 1m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            user = _userServices.GetUser(user.Id);
            Assert.IsNotNull(user.BanEndDate);
        }

        [TestMethod]
        public void GetAddedUserRatingTest()
        {
            var role = new Role("x", "x");
            _roleServices.AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(user.Id, 5, "none");
            _userRatingServices.AddUserRating(userRating);
            Assert.IsNotNull(_userRatingServices.GetAllUserRatings().First(u => u.Id == userRating.Id));
        }

        [TestMethod]
        public void UnBanUserFailTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 1m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            user = _userServices.GetUser(user.Id);
            Assert.IsFalse(_userServices.UnBan(user.Id));
        }

        [TestMethod]
        public void UnBanUserTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating1 = new UserRating(user.Id, 1m, "Not bad, but could have been better");
            _userRatingServices.AddUserRating(userRating1);
            user = _userServices.GetUser(user.Id);
            user.BanEndDate = DateTime.Now.AddDays(-1);
            _userServices.UpdateUser(user);
            Assert.IsTrue(_userServices.UnBan(user.Id));
        }

        [TestMethod]
        public void CheckUSerRatingUserEmptyOnIdChangeTest()
        {
            var rating = new UserRating();
            rating.User = new User();
            rating.UserId = 2;
            Assert.IsNull(rating.User);
        }

        [TestMethod]
        public void CheckIfNewUserForRatingsChangedTEst()
        {
            var user = new User();
            var rating = new UserRating();
            var newUser = new User();
            user.UserRatings.Add(rating);
            rating.User = newUser;
            Assert.AreEqual(newUser.UserRatings.Count, 1);
        }

        [TestMethod]
        public void CheckIfOldUserForRatingsChangedTEst()
        {
            var user = new User();
            var rating = new UserRating();
            var newUser = new User();
            user.UserRatings.Add(rating);
            rating.User = newUser;
            Assert.AreEqual(user.UserRatings.Count, 0);
        }

        [TestMethod]
        public void GetAllUserRatingTest()
        {
            Assert.IsNotNull(_userRatingServices.GetAllUserRatings());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateUserRatingFail()
        {
            _userRatingServices.UpdateUserRating(new UserRating());
        }

        [TestMethod]
        public void UpdateUserRatingTest()
        {
            var role = new Role("x", "x");
            _roleServices.AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var userRating = new UserRating(user.Id, 5, "none");
            _userRatingServices.AddUserRating(userRating);
            userRating.Rating = 10;
            _userRatingServices.UpdateUserRating(userRating);
            Assert.IsTrue(_userRatingServices.GetAllUserRatings().First(u => u.Id == userRating.Id).Rating == userRating.Rating);
        }
    }
}
