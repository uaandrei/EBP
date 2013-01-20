using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public static class ValidationResultGenerator
    {
        public static ValidationResult GenerateNewValidationResultFor(object target, string message)
        {
            return new ValidationResult(message, target, target.GetType().ToString(), "", null);
        }
    }
}
