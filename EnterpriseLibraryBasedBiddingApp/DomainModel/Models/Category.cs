using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using DomainModel.Properties;

namespace DomainModel
{
    [HasSelfValidation]
    partial class Category
    {
        #region CTOR

        public Category()
        {

        }

        public Category(string name)
        {
            this.Name = name;
        }

        #endregion CTOR

        #region Methods

        public override string ToString()
        {
            return string.Format("Categorty: name - {0}", this.Name);
        }

        #endregion Methods

        #region Validation

        [SelfValidation]
        private void DoCategoryValidations(ValidationResults validationResults)
        {
            if (Name.Trim() == string.Empty)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.InvalidCategoryName));
            }
            if (Name.Length>20)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.CategoryNameLength));
            }
        }

        #endregion Validation
    }
}
