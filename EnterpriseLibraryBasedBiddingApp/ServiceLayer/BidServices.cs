using DataMapper;
using DomainModel;
using DomainModel.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class BidServices
    {
        public ValidationResults AddBid(Bid bid)
        {
            var validationResults = new ValidationResults();
            var userServices = new UserServices();
            if (bid.UserId > 0 && userServices.GetUser(bid.UserId).BanEndDate != null)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserIsBanndAndCannotBid));
            }
            validationResults.AddAllResults(DomainObjectValidator.Validate<Bid>(bid));
            if (validationResults.Count == 0)
            {
                DataMapperFactoryMethod.GetCurrentFactory().BidFactory.AddBid(bid);
            }
            return validationResults;
        }

        public IList<Bid> GetAllBids()
        {
            return DataMapperFactoryMethod.GetCurrentFactory().BidFactory.GetAll();
        }
    }
}