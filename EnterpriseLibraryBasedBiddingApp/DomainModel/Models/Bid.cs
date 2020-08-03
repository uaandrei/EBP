using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using DomainModel.Properties;

namespace DomainModel
{
    [HasSelfValidation]
    partial class Bid
    {
        #region CTOR

        public Bid()
        {

        }

        public Bid(decimal sum, int productId, int userId)
        {
            Sum = sum;
            ProductId = productId;
            UserId = userId;
        }
        #endregion CTOR

        #region Methods

        public override string ToString()
        {
            return string.Format("Bid[Sum:{0};User:{1};Product:{2}", Sum, User, Product);
        }

        #endregion Methods

        #region Validation

        [SelfValidation]
        private void DoSumValidations(ValidationResults validationResults)
        {
            if (Sum < 1)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.BidSumMustBeGreaterThan1Message));
            }
            if (Sum > 999999)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.BidSumMustBeSmallerThan999999Message));
            }
            if (UserId == 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.BidMustHaveUser));
            }
            if (ProductId == 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.BidMustHaveProduct));
            }
        }

        #endregion Validation
    }
}