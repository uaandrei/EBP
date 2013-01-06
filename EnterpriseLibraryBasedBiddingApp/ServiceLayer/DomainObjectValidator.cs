using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    class DomainObjectValidator
    {
        #region Singleton

        static DomainObjectValidator _domainObjectValidator;

        private DomainObjectValidator()
        {
        }

        public static DomainObjectValidator Instance
        {
            get
            {
                if (_domainObjectValidator == null)
                {
                    _domainObjectValidator = new DomainObjectValidator();
                }
                return _domainObjectValidator;
            }
        }

        #endregion Singleton

        #region Methods

        public ValidationResults Validate<T>(T model)
        {
            return Validation.Validate<T>(model);
        }

        #endregion Methods
    }
}
