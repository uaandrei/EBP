using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using DomainModel;
using BiddingExceptions;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for FactoryMethodTests
    /// </summary>
    [TestClass]
    public class FactoryMethodTests
    {
        public FactoryMethodTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

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
        public void InvaliFactoryMethodTest()
        {
            ConfigurationManager.AppSettings["dataMapper"] = "tralala";
            try
            {
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory();
                Assert.Fail();
            }
            catch (Exception)
            {
            }
            ConfigurationManager.AppSettings["dataMapper"] = "ef";
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void AddInvalidCategoryFail()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.AddCategory(new Category());
        }

        [TestMethod]
        public void DMAllCategoriesTest()
        {
            Assert.IsNotNull(DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.GetAll());
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void UpdateInvalidCategoryFail()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.Update(new Category());
        }

        [TestMethod]
        public void DMAllProductsTest()
        {
            Assert.IsNotNull(DataMapper.DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetAll());
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionSetParentCategory()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.SetParentCategoryFor(new Category(), new Category());
        }

        [TestMethod]
        public void DMAllRolesTest()
        {
            Assert.IsNotNull(DataMapper.DataMapperFactoryMethod.GetCurrentFactory().RolesPersistence.GetAll());
        }
    }
}
