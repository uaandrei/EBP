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
    /// Summary description for RoleTests
    /// </summary>
    [TestClass]
    public class RoleTests
    {
        public RoleTests()
        {
            _roleServices = new RoleServices();
        }

        private TestContext testContextInstance;
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
        public void RoleNameCannotBeEmptyTest()
        {
            var role = new Role("", "xx");
            TestHelper.AssertExpectedMessage(Resources.RoleNameCannotBeEmpty, _roleServices.AddRole(role));
        }

        [TestMethod]
        public void RoleDescriptionCannotBeEmptyTest()
        {
            var role = new Role("xx", "");
            TestHelper.AssertExpectedMessage(Resources.RoleDescriptionCannotBeEmpty, _roleServices.AddRole(role));
        }

        [TestMethod]
        public void RoleNameToLongTest()
        {
            var role = new Role("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "");
            TestHelper.AssertExpectedMessage(Resources.RoleNameToLong, _roleServices.AddRole(role));
        }

        [TestMethod]
        public void RoleDescriptionToLongTest()
        {
            var role = new Role("xx", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            TestHelper.AssertExpectedMessage(Resources.RoleDescriptionToLong, _roleServices.AddRole(role));
        }

        [TestMethod]
        public void AddRoleTest()
        {
            Assert.IsTrue(new RoleServices().AddRole(new Role("xx", "role description")).Count == 0);
        }

        [TestMethod]
        public void RoleUsersChangeForNewUserTest()
        {
            var roles = new FixupCollection<Role>() { new Role(), new Role() };
            var Users = new FixupCollection<User>() { new User(), new User() };
            var newUsers = new FixupCollection<User>() { new User() };
            roles[0].Users = Users;
            roles[1].Users = Users;
            roles[0].Users = newUsers;
            Assert.IsTrue(roles[0].Users.Count == 1);
        }

        [TestMethod]
        public void GetAddedRoleTest()
        {
            var role = new Role("x", "x");
            _roleServices.AddRole(role);
            Assert.IsNotNull(_roleServices.GetAllRoles().First(u => u.Id == role.Id));
        }

        [TestMethod]
        public void ChangeUsersForRoleFromListTest()
        {
            var roles = new FixupCollection<Role>() { new Role(), new Role() };
            var Users = new FixupCollection<User>() { new User(), new User() };
            var newUsers = new FixupCollection<User>() { new User(), new User() };
            roles[0].Users = Users;
            Users.Add(new User());
            var newRole = new Role();
            var newUser = new User();
            var newUser1 = new User();
            newUser.Role = newRole;
            newUser1.Role = roles[0];
            roles[1].Users.Add(new User());
            roles[0].Users = newUsers;
            roles[0].Users.Add(newUser);
            roles[0].Users = new FixupCollection<User> { newUser, newUser1 };
            Assert.AreEqual(4, Users.Count);
        }

        [TestMethod]
        public void TestAddedRoleName()
        {
            var role = new Role("bidder", "none");
            _roleServices.AddRole(role);
            Assert.AreEqual(role.Name, _roleServices.GetAllRoles().First(r => r.Id == role.Id).Name);
        }

        [TestMethod]
        public void AddedRoleDescriptionTst()
        {
            var role = new Role("bidder", "none");
            _roleServices.AddRole(role);
            Assert.AreEqual(role.Description, _roleServices.GetAllRoles().First(r => r.Id == role.Id).Description);
        }

        [TestMethod]
        public void InvalidRoleAddTest()
        {
            var role = new Role("", "");
            TestHelper.AssertExpectedMessage(Resources.RoleDescriptionCannotBeEmpty, _roleServices.AddRole(role));
        }

        [TestMethod]
        public void GetALlRolesTest()
        {
            Assert.IsNotNull(_roleServices.GetAllRoles());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateRoleFail()
        {
            _roleServices.UpdateUserRating(new Role());
        }

        [TestMethod]
        public void AddRoleWithEmptyDesriptionTest()
        {
            Assert.IsTrue(new RoleServices().AddRole(new Role("xx", "")).Count > 0);
        }

        [TestMethod]
        public void UpdateUserRatingTest()
        {
            var role = new Role("x", "x");
            _roleServices.AddRole(role);
            role.Name = "test";
            _roleServices.UpdateUserRating(role);
            Assert.IsTrue(_roleServices.GetAllRoles().First(u => u.Id == role.Id).Name == role.Name);
        }
    }
}
