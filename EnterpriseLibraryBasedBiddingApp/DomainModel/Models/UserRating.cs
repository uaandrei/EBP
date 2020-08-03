using DomainModel.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [HasSelfValidation]
    partial class UserRating
    {
        #region CTOR

        public UserRating(int userId, decimal rating, string description)
        {
            _userId = userId;
            this.Rating = rating;
            this.Description = description;
        }

        public UserRating()
        {
            Description = "";
        }

        #endregion CTOR

        #region Methods

        public static UserRating DefaultRating
        {
            get
            {
                return new UserRating
                {
                    Rating = 10,
                    Description = "Be first to rate this user"
                };
            }
        }

        #endregion Methods

        #region Validation

        [SelfValidation]
        private void DoSumValidations(ValidationResults validationResults)
        {
            if (_userId == 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RatingMustHaveAssociatedUser));
            }
            if (Rating <= 0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RatingMustBePositiveNumber));
            }
            if (Rating > 10)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RatingCannotBeMoreThan10));
            }
            if (Description == string.Empty)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RatingMustContainADescription));
            }
            if (Description.Length > 500)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RatingDescriptionToLong));
            }
        }

        #endregion Validation
    }
}
