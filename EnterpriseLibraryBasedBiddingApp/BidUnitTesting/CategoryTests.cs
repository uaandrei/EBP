using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using DomainModel;
using DomainModel.Properties;
using BiddingExceptions;
using DataMapper.EntityFrameworkDataMapper;

namespace BidUnitTesting
{
    /// <summary>
    /// Summary description for CategoryTests
    /// </summary>
    [TestClass]
    public class CategoryTests
    {
        private CategoryServices _categoryServices;

        public CategoryTests()
        {
            _userServices = new UserServices();
            _roleServices = new RoleServices();
            _userRatingServices = new UserRatingServices();
            _bidServices = new BidServices();
            _productServices = new ProductServices();
            _categoryServices = new CategoryServices();
        }

        private TestContext testContextInstance;
        private BidServices _bidServices;
        private UserRatingServices _userRatingServices;
        private RoleServices _roleServices;
        private UserServices _userServices;
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
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}

        //
        // Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}

        #endregion

        [TestMethod]
        public void AddParentCategoryTest()
        {
            var audioCategory = new Category("Audio");
            var parentCateg = new Category("Electronics");
            _categoryServices.AddCategory(audioCategory);
            _categoryServices.AddCategory(parentCateg);
            Assert.IsTrue(_categoryServices.SetParentCategoryFor(parentCateg, audioCategory).Count == 0);
            Assert.IsTrue(audioCategory.ParentCategory == parentCateg);
        }

        [TestMethod]
        public void DeleteCategoryWithParentFailTest()
        {
            var audioCategory = new Category("Audio");
            var category = new Category("Electronics");
            _categoryServices.AddCategory(category);
            _categoryServices.AddCategory(audioCategory);
            _categoryServices.SetParentCategoryFor(category, audioCategory);
            Assert.IsTrue(_categoryServices.DeleteCategory(category).Count > 0);
            Assert.IsTrue(audioCategory.ParentCategory == category);
        }

        [TestMethod]
        public void GetAddedCategory()
        {
            var category = new Category("Electronics");
            _categoryServices.AddCategory(category);
            var categories = _categoryServices.GetAllCategories();
            var expectedCategory = categories.FirstOrDefault(c => c.Id == category.Id);
            Assert.IsNotNull(expectedCategory);
        }

        [TestMethod]
        public void CategoryAddWithEmptyNameFailTest()
        {
            var category = new Category("");
            TestHelper.AssertExpectedMessage(Resources.InvalidCategoryName, _categoryServices.AddCategory(category));
        }

        [TestMethod]
        public void CategoryAddTest()
        {
            var category = new Category("Electronics");
            Assert.IsTrue(_categoryServices.AddCategory(category).Count == 0);
            Assert.IsNotNull(_categoryServices.GetAllCategories().FirstOrDefault(c => c.Id == category.Id));
        }

        [TestMethod]
        public void GetAllCategoriesTest()
        {
            var categories = _categoryServices.GetAllCategories();
            Assert.IsNotNull(categories);
        }

        [TestMethod]
        public void GetAllCategoriesWithSomeCategoriesAddedTest()
        {
            var category = new Category("Electronics");
            _categoryServices.AddCategory(category);
            var categories = _categoryServices.GetAllCategories();
            var expectedCategory = categories.FirstOrDefault(c => c.Id == category.Id);
            Assert.IsNotNull(expectedCategory);
        }

        [TestMethod]
        public void DeleteASpecificCategoryTest()
        {
            var audioCategory = new Category("Audio");
            var todelete = new Category("delete");
            var videoCategory = new Category("Video");
            _categoryServices.AddCategory(audioCategory);
            _categoryServices.AddCategory(todelete);
            _categoryServices.AddCategory(videoCategory);
            Assert.IsTrue(_categoryServices.DeleteCategory(todelete).Count == 0);
            Assert.IsNull(_categoryServices.GetAllCategories().FirstOrDefault(c => c.Id == todelete.Id));
        }

        [TestMethod]
        public void CheckIfNewCategoryTransfersFromParentTest()
        {
            var Category1 = new Category();
            Category1.Id = 100;
            var Category = new Category();
            Category.Id = 4;
            var Parent = new Category();
            Category1.Id = 3;
            Parent.Categories.Add(Category);
            Category.ParentCategory = Category1;
            Assert.IsTrue(Category1.Categories.Count == 1);
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = _categoryServices.GetAllCategories();
            Assert.IsNotNull(categories);
        }

        [TestMethod]
        public void ChageCategoriesListForCategoryTest()
        {
            var Category = new List<Category>();
            var Category1 = new List<Category>();
            Category1.Add(new Category());
            var category = new Category();
            category.Categories = Category1;
            category.Categories = Category;
            category.Categories.Add(new Category());
            Assert.IsTrue(category.Categories.Count == 1);
        }

        [TestMethod]
        public void DeleteInexistendCategoryFailTest()
        {
            var category = new Category("Electronics");
            Assert.IsTrue(_categoryServices.DeleteCategory(category).Count == 1);
        }

        [TestMethod]
        public void AddCategoryWithNameOver20CharsFailTest()
        {
            var category = new Category("12345123451234512345_");
            TestHelper.AssertExpectedMessage(Resources.CategoryNameLength, _categoryServices.AddCategory(category));
        }

        [TestMethod]
        public void CategoryInvalidNameTest()
        {
            var category = new Category("");
            TestHelper.AssertExpectedMessage(Resources.InvalidCategoryName, _categoryServices.AddCategory(category));
        }

        [TestMethod]
        public void AddCategoryTest()
        {
            var item = new Category("xx");
            Assert.IsTrue(new CategoryServices().AddCategory(item).Count == 0);
        }

        [TestMethod]
        public void SetCategoriesForCategoryTest()
        {
            var category = new Category();
            var category1 = new Category();
            var category2 = new Category();
            category.Categories.Add(category1);
            Assert.IsTrue(category.Categories.Count == 1);
        }

        [TestMethod]
        public void DeleteCategoryFail()
        {
            Assert.IsTrue(_categoryServices.DeleteCategory(new Category()).Count == 1);
        }

        [TestMethod]
        public void ChageCategoryProductsTest()
        {
            var products = new FixupCollection<Product>() { new Product(), new Product() };
            var products1 = new FixupCollection<Product>() { new Product() };
            products1.Add(new Product());
            var category = new Category();
            category.Products = products1;
            category.Products = products;
            category.Products.Add(new Product());
            Assert.IsTrue(category.Products.Count == 3);
        }

        [TestMethod]
        public void AddCategoriesForCategoryTest()
        {
            var category = new Category();
            var category1 = new Category();
            var category2 = new Category();
            category.Categories.Add(category1);
            category.Categories.Add(category2);
            Assert.AreEqual(2, category.Categories.Count);
        }

        [TestMethod]
        public void DeleteEmptyCategoryFail()
        {
            var category = new Category();
            Assert.IsTrue(_categoryServices.DeleteCategory(category).Count == 1);
        }

        [TestMethod]
        public void ChangeCategoryForProductsWithNewCategoryUpdatedTest()
        {
            var category = new Category();
            var newCategory = new Category();
            var products = new FixupCollection<Product>() { new Product() };
            category.Products = products;
            newCategory.Products = products;
            Assert.IsTrue(newCategory.Products.Count == 1);
        }

        [TestMethod]
        public void ChangeCategoryForProductsWithOldCategoryUpdatedTest()
        {
            var category = new Category();
            var newCategory = new Category();
            var products = new FixupCollection<Product>() { new Product() };
            category.Products = products;
            newCategory.Products = products;
            Assert.IsTrue(category.Products.Count == 1);
        }

        [TestMethod]
        public void InvalidCategoryAddTest()
        {
            Assert.IsTrue(_categoryServices.AddCategory(new Category()).Count > 0);
        }

        [TestMethod]
        public void CategoryForProductsWithNewCategoryUpdatedTest()
        {
            var category = new Category();
            var newCategory = new Category();
            var newCategory2 = new Category();
            var products = new FixupCollection<Product>() { new Product() };
            category.Products = products;
            newCategory2 = category;
            newCategory.Products = products;
            Assert.IsTrue(newCategory.Products.Count == 1);
        }

        [TestMethod]
        public void CheckIfCategoryParentSwapsTest()
        {
            var Category1 = new Category();
            Category1.Id = 100;
            var Category = new Category();
            Category.Id = 4;
            var Parent = new Category();
            Category1.Id = 3;
            Parent.Categories.Add(Category);
            Category.ParentCategory = Category1;
            Assert.IsTrue(Parent.Categories.Count == 0);
        }

        [TestMethod]
        public void ChangeCategoriesForCategory()
        {
            var categories = new FixupCollection<Category> { new Category(), new Category() };
            var categories1 = new FixupCollection<Category> { new Category(), new Category(), new Category() };
            var category = new Category();
            category.Categories = categories;
            category.Categories = categories1;
            Assert.IsTrue(category.Categories.Count == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionAddCategory()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.AddCategory(new Category());
        }

        [TestMethod]
        public void CategoryWithNewCategoryUpdatedTest()
        {
            var newCategory = new Category();
            var newCategory2 = new Category();
            var products = new FixupCollection<Product>() { new Product() };
            newCategory2 = newCategory;
            newCategory.Products = products;
            Assert.IsTrue(newCategory.Products.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionUpdateCategory()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.Update(new Category());
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = new Category("Books");
            _categoryServices.AddCategory(category);
            category.Name = "Books & Audiobooks";
            _categoryServices.UpdateCategory(category);
            Assert.IsNotNull(_categoryServices.GetAllCategories().First(c => c.Name == category.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(PersistenceException))]
        public void PersistenceExceptionSetParentCategory()
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.SetParentCategoryFor(new Category(), new Category());
        }

        [TestMethod]
        public void AddInvalidUserTest()
        {
            Assert.IsTrue(_categoryServices.AddCategory(new Category()).Count > 0);
        }

        [TestMethod]
        public void UpdateInvalidUserTest()
        {
            Assert.IsTrue(_categoryServices.UpdateCategory(new Category()).Count > 0);
        }

        [TestMethod]
        public void SetParentCategoryForCategoryFail()
        {
            Assert.IsTrue(_categoryServices.SetParentCategoryFor(new Category(), new Category()).Count > 0);
        }
        [TestMethod]
        public void CategoryAddingForProduct()
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
    }
}
