using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using DomainModel;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for ProductAuthenticityTests
    /// </summary>
    [TestClass]
    public class ProductAuthenticityTests
    {
        public ProductAuthenticityTests()
        {
            _productAuthenticityService = new ProductAuthenticityService();
        }

        private TestContext testContextInstance;
        private ProductAuthenticityService _productAuthenticityService;

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
        public void ProductAuthenticity50SimilarityTest()
        {
            var product = new Product();
            product.Description = "This is just a test !";
            var product1 = new Product();
            product1.Description = "This is just";
            var expected = 50;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void ProductAuthenticity0SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B";
            var product1 = new Product();
            product1.Description = "C D E";
            var expected = 0;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity100SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B";
            var product1 = new Product();
            product1.Description = "A B";
            var expected = 100;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity33SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B C";
            var product1 = new Product();
            product1.Description = "B";
            var expected = 33;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity25SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B C D";
            var product1 = new Product();
            product1.Description = "B";
            var expected = 25;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity20SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B C d e f g h i j";
            var product1 = new Product();
            product1.Description = "B h";
            var expected = 20;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity10SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B C d e f g h i j";
            var product1 = new Product();
            product1.Description = "B";
            var expected = 10;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductsDescriptionDifferentTest()
        {
            var product = new Product();
            product.Description = "A";
            var product1 = new Product();
            product1.Description = "B";
            var expected = 0;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductAuthenticity40SimilarityTest()
        {
            var product = new Product();
            product.Description = "A B C d e f g h i j";
            var product1 = new Product();
            product1.Description = "d e f g";
            var expected = 40;
            var actual = _productAuthenticityService.GetSimilarityPercentage(product, product1);
            Assert.AreEqual(expected, actual);
        }
    }
}
