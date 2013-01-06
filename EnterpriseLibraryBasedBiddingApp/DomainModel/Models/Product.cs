using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using DomainModel.Properties;
using System.Linq;

namespace DomainModel
{
    [HasSelfValidation()]
    public partial class Product
    {
        #region Methods

        public ValidationResult AddBid(Bid bid)
        {
            var lastBid = Bids.LastOrDefault();
            if (bid.Sum < this.StartingPrice)
            {
                return new ValidationResult(Resources.BidSumMustBeGreaterThanProductStartingPrice, this, "StartingPrice", "", null);
            }
            Bids.Add(bid);
            return new ValidationResult(string.Empty, null, null, null, null);
        }

        #endregion Methods

        #region Validation

        [SelfValidation()]
        private void DoProductValidations(ValidationResults validationResults)
        {
            if (BidStartDate > BidEndDate)
            {
                validationResults.AddResult(new ValidationResult(Resources.ProductEndDateMustBeGreaterThanStartDateMessage, this, "BidEndDate", "", null));
            }
            if (BidStartDate < DateTime.Now)
            {
                validationResults.AddResult(new ValidationResult(Resources.ProductStartDateCantBeSetInPastTimeMessage, this, "BidStartDate", "", null));
            }
            if (BidEndDate < DateTime.Now)
            {
                validationResults.AddResult(new ValidationResult(Resources.ProductEndDateCantBeSetInPastTimeMessage, this, "BidEndDate", "", null));
            }
            if (StartingPrice<0)
            {
                validationResults.AddResult(new ValidationResult(Resources.ProductStartingPriceMustBeGreaterThanZeroMessage, this, "StartingPrice", "", null));
            }
            if (StartingPrice>999999)
            {
                validationResults.AddResult(new ValidationResult(Resources.ProductStartingPriceMustBeLowerThan1000000Message, this, "StartingPrice", "", null));
            }
        }

        #endregion Validation
    }
}