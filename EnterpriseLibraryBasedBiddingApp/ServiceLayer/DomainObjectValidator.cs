using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    static class DomainObjectValidator
    {
        public static ValidationResults Validate<T>(T model)
        {
            return Validation.Validate<T>(model);
        }
    }
}
