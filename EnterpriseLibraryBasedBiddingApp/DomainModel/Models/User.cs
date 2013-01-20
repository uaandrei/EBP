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
    partial class User
    {
        public User()
        {
            this.Name = "";
            this.Password = "";
            this.BanEndDate = null;
        }

        public User(string name, string password, Role role)
        {
            this.Name = name;
            this.Password = password;
            this.RoleId = role.Id;
        }

        #region Validation

        [SelfValidation]
        private void DoSumValidations(ValidationResults validationResults)
        {
            if (string.IsNullOrEmpty(Name))
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.InvalidUserName));
            }
            if (string.IsNullOrEmpty(Password))
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.InvalidUserPassword));
            }
            if (Name.Length > 20)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserNameToLong));
            }
            if (Password.Length > 20)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserPasswordToLong));
            }
            if (RoleId==0)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserMustHaveARole));
            }
        }

        #endregion Validation
    }
}
