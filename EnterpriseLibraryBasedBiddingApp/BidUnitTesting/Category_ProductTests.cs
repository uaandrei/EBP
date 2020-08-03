using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModel;
using ServiceLayer;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for Category_ProductTests
    /// </summary>
    [TestClass]
    public class Category_ProductTests
    {
        public Category_ProductTests()
        {
            _categoryServices = new CategoryServices();
            _productServices = new ProductServices();
            _userServices = new UserServices();
        }

        private CategoryServices _categoryServices;
        private ProductServices _productServices;
        private TestContext testContextInstance;
        private UserServices _userServices;

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
        public void AddCategoryForProductTest()
        {
            var role = new Role("xx", "xx");
            new RoleServices().AddRole(role);
            var user = new User("adi", "123",role);
            _userServices.AddUser(user);
            var category = new Category("Audio");
            _categoryServices.AddCategory(category);
            var product = new Product(DateTime.Now.AddHours(1), DateTime.Now.AddDays(2), category.Id, 100, "IPhone headphones", "eur", "To expensive to buy", user.Id);
            _productServices.AddProduct(product);
            var products = _productServices.GetAllProducts();
            Assert.IsTrue(products.First(p => p.Id == product.Id).CategoryId == category.Id);
        }
    }
}
