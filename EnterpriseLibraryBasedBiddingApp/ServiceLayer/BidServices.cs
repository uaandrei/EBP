using DataMapper;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer
{
    public class BidServices
    {
        public ValidationResults AddBid(Bid bid)
        {
            var valid = ValidateBid(bid);
            if (valid.Count == 0)
            {
                DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBid(bid);
            }
            return valid;
        }

        public ValidationResults ValidateBid(Bid bid)
        {
            return Validation.Validate<Bid>(bid);
        }
    }
}