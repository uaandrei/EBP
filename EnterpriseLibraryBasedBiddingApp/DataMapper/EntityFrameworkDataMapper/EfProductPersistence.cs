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
    }
}