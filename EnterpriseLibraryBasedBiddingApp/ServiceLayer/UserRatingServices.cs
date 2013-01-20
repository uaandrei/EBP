using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UserRatingServices
    {
        private UserServices _userServices;

        public UserRatingServices()
        {
            _userServices = new UserServices();
        }

        public ValidationResults AddUserRating(UserRating userRating)
        {
            var validationResults = DomainObjectValidator.Validate<UserRating>(userRating);
            if (validationResults.Count == 0)
            {
                DataMapper.DataMapperFactoryMethod.GetCurrentFactory().UserRatingFactory.Add(userRating);
                var averageRating = GetAverageRatingForUser(userRating.UserId);
                if (averageRating < ConfigurationConstants.Items.MinRating)
                {
                    _userServices.BanUser(userRating.UserId);
                }
            }
            return validationResults;
        }

        public IList<UserRating> GetRatingsForUser(int userId)
        {
            var ratings = new List<UserRating>();
            var userRatings = DataMapper.DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetRating(userId);
            if (userRatings != null && userRatings.Count > 0)
            {
                ratings.AddRange(userRatings);
            }
            else
            {
                ratings.Add(UserRating.DefaultRating);
            }
            return ratings;
        }

        public IList<UserRating> GetAllUserRatings()
        {
            return DataMapper.DataMapperFactoryMethod.GetCurrentFactory().UserRatingFactory.GetAll();
        }

        public void UpdateUserRating(UserRating userRating)
        {
            DataMapper.DataMapperFactoryMethod.GetCurrentFactory().UserRatingFactory.Update(userRating);
        }

        public decimal GetAverageRatingForUser(int userId)
        {
            var userRatings = GetRatingsForUser(userId);
            return userRatings.Average(ur => ur.Rating);
        }
    }
}
