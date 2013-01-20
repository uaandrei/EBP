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
    partial class Role
    {
        public Role()
        {
            this.Name = "";
            this.Description = "";
        }

        public Role(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        [SelfValidation]
        private void DoValidations(ValidationResults validationResults)
        {
            if (this.Name == "")
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RoleNameCannotBeEmpty));
            }
            if (this.Description == "")
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RoleDescriptionCannotBeEmpty));
            }
            if (this.Name.Length > 20)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RoleNameToLong));

            }
            if (this.Description.Length > 500)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.RoleDescriptionToLong));
            }
        }
    }
}
