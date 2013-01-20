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
        #region CTOR

        public Product()
        {
            this.Description = "";
            this.Name = "";
            this.BidCurrency = "";
            this.Available = true;
        }

        public Product(DateTime biddingStartDate, DateTime biddingEndDate, int categoryId, decimal startingPrice, string name, string currency, string description, int ownerId)
        {
            this.BidCurrency = currency;
            this.BidEndDate = biddingEndDate;
            this.BidStartDate = biddingStartDate;
            this.CategoryId = categoryId;
            this.Description = description;
            this.Name = name;
            this.StartingPrice = startingPrice;
            this.Available = true;
            this.UserId = ownerId;
        }

        #endregion CTOR

        #region Methods

        public bool IsAvailableForBidding()
        {
            if (DateTime.Now > this.BidEndDate)
            {
                this.Available = false;
            }
            return this.Available;
        }

        public ValidationResults AddBid(Bid bid)
        {
            var validationResults = new ValidationResults();
            var lastBid = Bids.LastOrDefault();
            if (this.IsAvailableForBidding())
            {
                if (bid.Sum < this.StartingPrice)
                {
                    validationResults.AddResult(new ValidationResult(Resources.BidSumMustBeGreaterThanProductStartingPrice, this, "StartingPrice", "", null));
                }
                if (lastBid != null && bid.Sum < lastBid.Sum)
                {
                    validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.BidMustBeGreaterThanLastBid));
                }
                if (validationResults.Count == 0)
                {
                    Bids.Add(bid);
                }
            }
            else
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductNotAvailableForBidding));
            }
            return validationResults;
        }

        #endregion Methods

        #region Validation

        [SelfValidation()]
        private void DoProductValidations(ValidationResults validationResults)
        {
            if (BidStartDate > BidEndDate)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductEndDateMustBeGreaterThanStartDateMessage));
            }
            if (BidStartDate < DateTime.Now)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductStartDateCantBeSetInPastTimeMessage));
            }
            if (BidEndDate < DateTime.Now)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductEndDateCantBeSetInPastTimeMessage));
            }
            if (StartingPrice <= 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductStartingPriceMustBeGreaterThanZeroMessage));
            }
            if (StartingPrice > 999999)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductStartingPriceMustBeLowerThan1000000Message));
            }
            if (BidCurrency.Length > 10)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductBidCurrencyMaxLenght));
            }
            if (string.IsNullOrEmpty(BidCurrency.Trim()))
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductBidCurrencyInvalid));
            }
            if (CategoryId == 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductInvalidCategory));
            }
            if (Name.Length > 20)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductNameMaxLength));
            }
            if (Description == string.Empty)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.InvalidDescription));
            }
            if (Description.Length > 500)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.DescriptionToLong));
            }
            if (Name == string.Empty)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductNameInvalid));
            }
            if (this.UserId==0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.ProductMustHaveAnOwner));
            }
        }

        #endregion Validation
    }
}