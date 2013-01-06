using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace DataMapper.EntityFrameworkDataMapper
{
    internal class EfUserPersistence : IUserPersistence
    {
        public void AddUser(User user)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public IList<User> GetAllUsers()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Users.ToList();
            }
        }
    }
}