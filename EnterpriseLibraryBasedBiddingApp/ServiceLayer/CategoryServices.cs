using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel;
using DataMapper;
using BiddingExceptions;

namespace ServiceLayer
{
    public class CategoryServices
    {
        public ValidationResults AddCategory(Category category)
        {
            var categoryValidation = DomainObjectValidator.Validate<Category>(category);
            if (categoryValidation.Count == 0)
            {
                try
                {
                    DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.AddCategory(category);
                }
                catch (PersistenceException e)
                {
                    categoryValidation.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, e.PersistenceMessage));
                }
            }
            return categoryValidation;
        }

        public IList<Category> GetAllCategories()
        {
                return DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.GetAll();
        }

        public ValidationResults DeleteCategory(Category category)
        {
            var validationResults = new ValidationResults();
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.Delete(category);
            }
            catch (PersistenceException e)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, e.PersistenceMessage));
            }
            return validationResults;
        }

        public ValidationResults UpdateCategory(Category category)
        {
            var validationResults = new ValidationResults();
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.Update(category);
            }
            catch (PersistenceException e)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, e.PersistenceMessage));
            }
            return validationResults;
        }

        public ValidationResults SetParentCategoryFor(Category parentCateg, Category category)
        {
            var validationResults = new ValidationResults();
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().CategoryFactory.SetParentCategoryFor(parentCateg, category);
                category.ParentCategory = parentCateg;
            }
            catch (PersistenceException e)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, e.PersistenceMessage));
            }
            return validationResults;
        }
    }
}
