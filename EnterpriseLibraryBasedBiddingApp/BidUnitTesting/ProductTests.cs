using DomainModel.Properties;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ServiceLayer;
using BiddingExceptions;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for ProductValidation
    /// </summary>
    [TestClass]
    public class ProductTests
    {
        private Product _product;
        private ProductServices _productServices;
        private UserServices _userServices;
        private CategoryServices _categoryServices;
        private RoleServices _roleServies;

        public ProductTests()
        {
            _product = new Product();
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _categoryServices = new CategoryServices();
            _roleServies = new RoleServices();
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

        [TestMethod]
        public void ProductBidEndDateIsGreaterThanBidStartDateTest()
        {
            _product.BidStartDate = DateTime.Now;
            _product.BidEndDate = DateTime.Now.AddDays(-1);
            TestHelper.AssertExpectedMessage(Resources.ProductEndDateMustBeGreaterThanStartDateMessage, _productServices.AddProduct(_product));
        }

        [TestMethod]
        public void ProductBidStartDateCantBeInPastTimeTest()
        {
            _product.BidStartDate = DateTime.Now.AddDays(-1);
            TestHelper.AssertExpectedMessage(Resources.ProductStartDateCantBeSetInPastTimeMessage, _productServices.AddProduct(_product));
        }

        [TestMethod]
        public void ProductBidEndDateCantBeInPastTimeTest()
        {
            _product.BidEndDate = DateTime.Now.AddDays(-1);
            TestHelper.AssertExpectedMessage(Resources.ProductEndDateCantBeSetInPastTimeMessage, _productServices.AddProduct(_product));
        }

        [TestMethod]
        public void CannotAddBidIfSumIsLowerThanProductStartingPriceTest()
        {
            _product.StartingPrice = 110;
            _product.BidStartDate = DateTime.Now.AddHours(2);
            _product.BidEndDate = DateTime.Now.AddDays(2);
            var bid = new Bid(100, 1, 1);
            TestHelper.AssertExpectedMessage(Resources.BidSumMustBeGreaterThanProductStartingPrice, _productServices.AddBidForProduct(_product, bid));
        }

        [TestMethod]
        public void CheckIfProductMovesIfUserChangedTest()
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
        public void CheckIfProductExUserIsEmptyIfUserChangedTest()
        {
            var User = new User();
            var newUser = new User();
            var product = new Product();
            product.Id = 2;
            User.Products.Add(product);
            product.User = newUser;
            Assert.IsTrue(User.Products.Count == 0);
        }

        [TestMethod]
        public void BidForProductTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control 2", user.Id);
            _productServices.AddProduct(product);
            var bid = new Bid(990, product.Id, user.Id);
            Assert.IsTrue(_productServices.AddBidForProduct(product, bid).Count == 0);
            var expected = _productServices.GetAllProducts().First(p => p.Id == product.Id);
            Assert.IsTrue(expected.Bids.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void BidForProductWithInvalidUserFailTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control 2", user.Id);
            _productServices.AddProduct(product);
            var bid = new Bid(990, product.Id, 0);
            _productServices.AddBidForProduct(product, bid);
        }

        [TestMethod]
        public void BidMustBeGreaterThanLastBidTest()
        {
            var product = new Product();
            product.StartingPrice = 100;
            product.BidEndDate = DateTime.Now.AddDays(1);
            product.AddBid(new Bid(100, 1, 1));
            TestHelper.AssertExpectedMessage(Resources.BidMustBeGreaterThanLastBid, product.AddBid(new Bid(90, 1, 10)));
        }

        [TestMethod]
        public void ProductBidStartingPriceMustBeGreaterThan0Test()
        {
            _product.StartingPrice = -10;
            TestHelper.AssertExpectedMessage(Resources.ProductStartingPriceMustBeGreaterThanZeroMessage, _productServices.AddProduct(_product));
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void EndBiddingForInvalidProduct()
        {
            _productServices.EndBiddingForProduct(new Product());
        }

        [TestMethod]
        public void ProductStartingPriceCannotBeHigherThan999999Test()
        {
            _product.StartingPrice = 1000000;
            TestHelper.AssertExpectedMessage(Resources.ProductStartingPriceMustBeLowerThan1000000Message, _productServices.AddProduct(_product));
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var products = _productServices.GetAllProducts();
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void InvalidProductAddTest()
        {
            Assert.IsTrue(_productServices.AddProduct(new Product()).Count > 0);
        }

        [TestMethod]
        public void ProductBidCurrencyMaxLenghtTest()
        {
            var product = new Product();
            product.BidCurrency = "1111111111111111111111111111111111111";
            TestHelper.AssertExpectedMessage(Resources.ProductBidCurrencyMaxLenght, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void ProductEmptyBidCurrencyTest()
        {
            var product = new Product();
            product.BidCurrency = string.Empty;
            TestHelper.AssertExpectedMessage(Resources.ProductBidCurrencyInvalid, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void ProductInvalidCategoryTest()
        {
            var product = new Product();
            product.Category = new Category();
            TestHelper.AssertExpectedMessage(Resources.ProductInvalidCategory, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void ProductNameMaxLengthTest()
        {
            var product = new Product();
            product.Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            TestHelper.AssertExpectedMessage(Resources.ProductNameMaxLength, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void ProductNameInvalidTest()
        {
            var product = new Product();
            product.Name = "";
            TestHelper.AssertExpectedMessage(Resources.ProductNameInvalid, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void InvalidDescriptionTest()
        {
            var product = new Product();
            product.Description = "";
            TestHelper.AssertExpectedMessage(Resources.InvalidDescription, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void DescriptionToLongTest()
        {
            var product = new Product();
            product.Description = "12345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345123451234512345";
            TestHelper.AssertExpectedMessage(Resources.DescriptionToLong, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void IsProductAvailableTest()
        {
            var product = new Product();
            product.BidStartDate = DateTime.Now;
            product.BidEndDate = DateTime.Now.AddDays(2);
            Assert.IsTrue(_productServices.IsProductAvailable(product));
        }

        [TestMethod]
        public void IsProductAvailableForBiddingIfEndDateIsPassedFailTest()
        {
            var product = new Product();
            var date = DateTime.Now;
            product.BidStartDate = date.AddDays(-2);
            product.BidEndDate = date.AddDays(-1);
            Assert.IsTrue(!_productServices.IsProductAvailable(product));
        }

        [TestMethod]
        public void EndProductBiddingAvailabiliyTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id);
            _productServices.AddProduct(product);
            _productServices.EndBiddingForProduct(product);
            Assert.IsTrue(!_productServices.IsProductAvailable(product));
        }

        [TestMethod]
        public void BidForProductWithEndDatePassedTest()
        {
            var product = new Product();
            product.BidEndDate = DateTime.Now.AddDays(-1);
            TestHelper.AssertExpectedMessage(Resources.ProductNotAvailableForBidding, product.AddBid(new Bid(10, 1, 1)));
        }

        [TestMethod]
        public void ProductSetCategoryTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("adi", "123", role);
            _userServices.AddUser(user);
            var category = new Category("Audio");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddHours(1), DateTime.Now.AddDays(2), category.Id, 100, "IPhone headphones", "eur", "To expensive to buy", user.Id);
            _productServices.AddProduct(product);
            var products = _productServices.GetAllProducts();
            Assert.IsTrue(products.First(p => p.Id == product.Id).CategoryId == category.Id);
        }

        [TestMethod]
        public void ProductMustHaveAnOwnerTest()
        {
            var product = new Product();
            product.User = new User();
            TestHelper.AssertExpectedMessage(Resources.ProductMustHaveAnOwner, _productServices.AddProduct(product));
        }

        [TestMethod]
        public void AddProductTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("x", "x", role);
            _userServices.AddUser(user);
            var category = new Category("Elect");
            _categoryServices.AddCategory(category);
            Assert.IsTrue(new ProductServices().AddProduct(new Product(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), category.Id, 100, "Remote control", "eur", "Samsung remote control", user.Id)).Count == 0);
        }

        [TestMethod]
        public void ProductCheckIfCategoryIsNullAfterIdChangedTEst()
        {
            var product = new Product();
            product.Category = new Category();
            product.CategoryId = 3;
            Assert.IsNull(product.Category);
        }

        [TestMethod]
        public void ProductCheckIfUserIsNullAfterIdChangedTEst()
        {
            var product = new Product();
            product.User = new User();
            product.UserId = 3;
            Assert.IsNull(product.User);
        }

        [TestMethod]
        public void ProductBidsChangedTest()
        {
            var bids = new FixupCollection<Bid> { new Bid(), new Bid() };
            var bids1 = new FixupCollection<Bid> { new Bid(), new Bid(), new Bid() };
            var product = new Product();
            product.Bids = bids;
            product.Bids = bids1;
            Assert.IsTrue(product.Bids.Count == 3);
        }

        [TestMethod]
        public void CheckIfProductMovesIfCategoryChangedTest()
        {
            var category = new Category();
            var newCategory = new Category();
            var product = new Product();
            product.Id = 2;
            category.Products.Add(product);
            product.Category = newCategory;
            Assert.IsTrue(newCategory.Products.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersitenceExceptionUpdateProduct()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.UpdateProduct(new Product());
        }

        [TestMethod]
        public void CheckIfProductExCategoryIsEmptyIfCategoryChangedTest()
        {
            var category = new Category();
            var newCategory = new Category();
            var product = new Product();
            product.Id = 2;
            category.Products.Add(product);
            product.Category = newCategory;
            Assert.IsTrue(category.Products.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersitenceExceptionAddProduct()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.AddProduct(new Product());
        }

        [TestMethod]
        public void GetAllProductsFromCategoryIncludingParentTEst()
        {
            var category1 = new Category("Audio");
            var category2 = new Category("Video");
            var parentCateg = new Category("Electronics");
            var role = new Role("x", "x");
            _roleServies.AddRole(role);
            var user = new User("adi", "test", role);
            _userServices.AddUser(user);
            category1.ParentCategory = parentCateg;
            category2.ParentCategory = parentCateg;
            _categoryServices.AddCategory(parentCateg);
            _categoryServices.AddCategory(category2);
            _categoryServices.AddCategory(category1);
            var product = new Product(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                category2.Id,
                100,
                "Headset",
                "eur",
                "good stuff",
                user.Id);
            var product1 = new Product(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                parentCateg.Id,
                100,
                "Headset",
                "eur",
                "good stuff",
                user.Id);
            _productServices.AddProduct(product);
            _productServices.AddProduct(product1);
            Assert.IsNotNull(_productServices.GetAllProducts(parentCateg.Id));
        }
    }
}