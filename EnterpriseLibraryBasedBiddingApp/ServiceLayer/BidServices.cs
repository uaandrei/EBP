using DataMapper;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer
{
    public class BidServices
    {
        public void AddBid(Bid bid)
        {
            DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBid(bid);
        }

        public ValidationResults ValidateBid(Bid bid)
        {
            return Validation.Validate<Bid>(bid);
        }
    }
}