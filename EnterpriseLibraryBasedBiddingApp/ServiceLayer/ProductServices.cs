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
            var valid = DomainObjectValidator.Instance.Validate<Product>(product);
            if (valid.Count == 0)
            {
                DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.AddProduct(product);
            }
            return valid;
        }

        public ValidationResult AddBidForProduct(Product product, Bid bid)
        {
            var bidServices = new BidServices();
            var valid = product.AddBid(bid);
            if (valid.Message == string.Empty)
            {
                if (bidServices.ValidateBid(bid).Count == 0)
                {
                    DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBid(bid);
                    DataMapperFactoryMethod.GetCurrentFactory().ProductFactory.SaveProduct(product);
                }
            }
            return valid;
        }
    }
}
