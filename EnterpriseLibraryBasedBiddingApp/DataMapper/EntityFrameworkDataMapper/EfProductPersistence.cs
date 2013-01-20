using BiddingExceptions;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfProductPersistence : IProductPersistence
    {
        public void AddProduct(DomainModel.Product product)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    context.Products.AddObject(product);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                LoggerService.Logger.LogError(e.Message, e);
                throw new PersistenceException("Adding product failed! Check log file.", e.Message, e);
            }
        }


        public void UpdateProduct(DomainModel.Product product)
        {
            try
            {
                using (var context = new BiddingDataModelContainer())
                {
                    var target = context.Products.Where(p => p.Id == product.Id).First();
                    target = product;
                    context.Products.ApplyCurrentValues(target);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                LoggerService.Logger.LogError(e.Message, e);
                throw new PersistenceException("Updating product failed! Check log file.", e.Message, e);
            }
        }

        public System.Collections.Generic.IList<DomainModel.Product> GetAll()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Products.Include("Bids").ToList();
            }
        }


        public void GetUpdatedItem(ref DomainModel.Product product)
        {
            var pid = product.Id;
            using (var context = new BiddingDataModelContainer())
            {
                product = context.Products.Include("Bids").First(p => p.Id == pid);
            }
        }


        public IList<Product> GetAllProducts(int categoryId)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var productList = new List<Product>();
                var categoryDb = context.Categories.Include("Categories").Include("Categories.Products").First(c => c.Id == categoryId);
                productList = context.Products.Where(p => p.CategoryId == categoryId).ToList();
                if (categoryDb.ParentCategory == null)
                {
                    foreach (var category in categoryDb.Categories)
                    {
                        foreach (var product in category.Products)
                        {
                            if (productList.FirstOrDefault(p => p.Id == product.Id) == null)
                            {
                                productList.Add(product);
                            }
                        }
                    }
                }
                return productList;
            }
        }
    }
}