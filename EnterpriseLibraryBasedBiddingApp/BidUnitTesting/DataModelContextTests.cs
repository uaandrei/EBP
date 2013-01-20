using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for DataModelContextTests
    /// </summary>
    [TestClass]
    public class DataModelContextTests
    {
        public DataModelContextTests()
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
        public void DataModelContainerWithConnectionStringTest()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BiddingDataModelContainer"].ConnectionString;
            var container = new DataMapper.EntityFrameworkDataMapper.BiddingDataModelContainer(connectionString);
            Assert.IsNotNull(container);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataModelContainerWithConnectionStringFail()
        {
            var connectionString = "fail";
            var container = new DataMapper.EntityFrameworkDataMapper.BiddingDataModelContainer(connectionString);
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void DataModelContainerWithEntityConnectionTest()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BiddingDataModelContainer"].ConnectionString;
            System.Data.EntityClient.EntityConnection con = new System.Data.EntityClient.EntityConnection(connectionString);
            var container = new DataMapper.EntityFrameworkDataMapper.BiddingDataModelContainer(con);
            Assert.IsNotNull(container);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataModelContainerWithEntityConnectionFail()
        {
            var connectionString = "empty";
            System.Data.EntityClient.EntityConnection con = new System.Data.EntityClient.EntityConnection(connectionString);
            var container = new DataMapper.EntityFrameworkDataMapper.BiddingDataModelContainer(con);
            Assert.IsNotNull(container);
        }
    }
}
