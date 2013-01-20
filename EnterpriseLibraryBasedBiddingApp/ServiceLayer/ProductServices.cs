using BiddingExceptions;
using DataMapper;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductServices
    {
        public ValidationResults AddProduct(Product product)
        {
            var valid = DomainObjectValidator.Validate<Product>(product);
            if (valid.Count == 0)
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.AddProduct(product);
                var allProductsExceptThis = GetAllProducts().Where(p => p.Id != product.Id);
                var authenticityService = new ProductAuthenticityService();
                foreach (var item in allProductsExceptThis)
                {
                    var perc = authenticityService.GetSimilarityPercentage(product, item);
                    if (perc > 20)
                    {
                        LoggerService.Logger.LogWarn(string.Format("product with id {0} and {1} have a similarity of {2}%.", product.Id, item.Id, perc), null);
                    }
                }
            }
            return valid;
        }

        public ValidationResults AddBidForProduct(Product product, Bid bid)
        {
            var validationResults = new ValidationResults();
            validationResults.AddAllResults(product.AddBid(bid));
            validationResults.AddAllResults(DomainObjectValidator.Validate<Product>(product));
            if (validationResults.Count == 0)
            {
                DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBidForProduct(bid, product);
            }
            return validationResults;
        }

        public bool IsProductAvailable(Product product)
        {
            return product.IsAvailableForBidding();
        }

        public void EndBiddingForProduct(Product product)
        {
            if (product.Available)
            {
                product.Available = false;
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.UpdateProduct(product);
            }
        }

        public IList<Product> GetAllProducts()
        {
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetAll();
        }

        public void GetUpdatedProduct(ref Product product)
        {
            DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetUpdatedItem(ref product);
        }

        public IList<Product> GetAllProducts(int categoryId)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.GetAllProducts(categoryId);
        }
    }
}
