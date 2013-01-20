using BiddingExceptions;
using DataMapper;
using DomainModel;
using DomainModel.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer
{
    public class UserServices
    {
        private ProductServices _productServices;

        public UserServices()
        {
            _productServices = new ProductServices();
        }

        public ValidationResults AddUser(User user)
        {
            var validationResults = DomainObjectValidator.Validate<User>(user);
            if (validationResults.Count == 0)
            {
                try
                {
                    DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddUser(user);
                }
                catch (PersistenceException e)
                {
                    validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, e.PersistenceMessage));
                }
            }
            return validationResults;
        }



        public int CalculateMaxNoOfProductsInBidding(int userId)
        {
            var userRatingService = new UserRatingServices();
            var averageRating = userRatingService.GetAverageRatingForUser(userId);
            if (averageRating < ConfigurationConstants.Items.MinRating)
            {
                return 0;
            }
            int maxProd = ConfigurationConstants.Items.MaxProductsInBiddingStateByUser;
            return (int)(maxProd + averageRating - ConfigurationConstants.Items.MinRating);
        }

        public ValidationResults AddProductForBiddingByUser(User user, Product product)
        {
            var validationResults = new ValidationResults();
            if (user.BanEndDate != null)
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserIsBanned));
            }
            if (user.Products != null)
            {
                if (user.Products.Where(p => p.IsAvailableForBidding()).Count() >= CalculateMaxNoOfProductsInBidding(user.Id))
                {
                    validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserCantHaveSomeManyProductInBidding));
                }
                var productsInCategory = _productServices.GetAllProducts(product.CategoryId);
                var nrProducts = productsInCategory == null ? 0 : productsInCategory.Count;
                if (nrProducts >= ConfigurationConstants.Items.MaxProductsInBiddingStateInCategory)
                {
                    validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.CategoryCantHaveSoManyProductInBdidding));
                }
            }
            if (validationResults.Count == 0)
            {
                validationResults.AddAllResults(_productServices.AddProduct(product));
            }
            return validationResults;
        }

        public IList<User> GetAllUsers()
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllUsers();
        }

        public IList<User> GetAllUsersWithBidsAndProducts()
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllUsersWithBidsAndProducts();
        }

        public ValidationResults EndProductBiddingByUser(User user, Product product)
        {
            var validationResults = new ValidationResults();
            if (user.Products.FirstOrDefault(p => p.Id == product.Id) != null)
            {
                _productServices.EndBiddingForProduct(product);
            }
            else
            {
                validationResults.AddResult(ValidationResultGenerator.GenerateNewValidationResultFor(this, Resources.UserHasNoSuchProduct));
            }
            return validationResults;
        }

        public void UpdateUserWithNavigationProperties(ref User user)
        {
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateUserWithNavigationProperties(ref user);
        }

        public void BanUser(int userId)
        {
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.Ban(userId);
        }

        public bool UnBan(int userId)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UnBan(userId);
        }

        public User GetUser(int userId)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.Get(userId);
        }

        public void UpdateUser(User user)
        {
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.Update(user);
        }
    }
}