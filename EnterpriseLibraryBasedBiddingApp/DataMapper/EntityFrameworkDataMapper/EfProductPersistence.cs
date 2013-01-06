using System.Linq;

namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfProductPersistence : IProductPersistence
    {
        public void AddProduct(DomainModel.Product product)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }


        public void SaveProduct(DomainModel.Product product)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var target = context.Products.Where(p => p.Id == product.Id).First();
                target = product;
                context.SaveChanges();
            }
        }
    }
}