using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ServiceLayer
{
    public class RoleServices
    {
        public ValidationResults AddRole(Role role)
        {
            var validationResults = new ValidationResults();
            validationResults.AddAllResults(DomainObjectValidator.Validate<Role>(role));
            if (validationResults.Count == 0)
            {
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory().RolesPersistence.Add(role);
            }
            return validationResults;
        }

        public IList<Role> GetAllRoles()
        {
            return DataMapper.DataMapperFactoryMethod.GetCurrentFactory().RolesPersistence.GetAll();
        }

        public void UpdateUserRating(Role role)
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().RolesPersistence.Update(role);
        }
    }
}
