using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataMapper.EntityFrameworkDataMapper
{
    class EfUserRatingPersistence : IUserRatingPersistence
    {
        public void Add(UserRating userRating)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.UserRatings.AddObject(userRating);
                context.SaveChanges();
            }
        }


        public IList<UserRating> GetAll()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.UserRatings.ToList();
            }
        }

        public void Update(UserRating userRating)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var dbe = context.UserRatings.First(u => u.Id == userRating.Id);
                dbe = userRating;
                context.UserRatings.ApplyCurrentValues(dbe);
                context.SaveChanges();
            }
        }
    }
}
