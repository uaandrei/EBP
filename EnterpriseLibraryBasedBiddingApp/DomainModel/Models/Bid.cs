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
        public Bid(decimal sum)
        {
            Sum = sum;
        }
        #endregion CTOR

        #region Validation
        
        [SelfValidation]
        private void DoSumValidations(ValidationResults validationResults)
        {
            if (Sum < 1)
            {
                validationResults.AddResult(new ValidationResult(Resources.BidSumMustBeGreaterThan1Message, this, "Sum", "", null));
            }
            if (Sum > 999999)
            {
                validationResults.AddResult(new ValidationResult(Resources.BidSumMustBeSmallerThan999999Message, this, "Sum", "", null));
            }
        }

        #endregion Validation
    }
}