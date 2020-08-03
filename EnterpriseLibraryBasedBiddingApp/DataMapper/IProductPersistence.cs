using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface IProductPersistence
    {
        void AddProduct(Product product);

        void UpdateProduct(Product product);

        IList<Product> GetAll();

        void GetUpdatedItem(ref Product product);

        IList<Product> GetAllProducts(int categoryId);
    }
}