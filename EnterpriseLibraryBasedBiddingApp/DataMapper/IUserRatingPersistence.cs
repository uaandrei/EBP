using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataMapper
{
    public interface IUserRatingPersistence
    {
        void Add(UserRating userRating);

        IList<UserRating> GetAll();

        void Update(UserRating userRating);
    }
}
