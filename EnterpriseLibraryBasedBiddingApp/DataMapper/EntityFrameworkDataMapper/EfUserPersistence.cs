using DomainModel;
using System;
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
                context.Users.AddObject(user);
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

        public IList<User> GetAllUsersWithBidsAndProducts()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Users.Include("Bids").Include("Products").ToList();
            }
        }


        public void UpdateUserWithNavigationProperties(ref User user)
        {
            var uid = user.Id;
            using (var context = new BiddingDataModelContainer())
            {
                var userDb = context.Users.Include("Bids").Include("Products").First(u => u.Id == uid);
                user = userDb;
            }
        }

        public User Get(int userId)
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Users.First(u => u.Id == userId);
            }
        }

        public IList<UserRating> GetRating(int userId)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var user = context.Users.Include("UserRatings").FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    return user.UserRatings.ToList();
                }
                else
                {
                    return null;
                }
            }
        }


        public void Ban(int userId)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Users.First(u => u.Id == userId).BanEndDate = DateTime.Now.AddDays(ConfigurationConstants.Items.BanDays);
                context.SaveChanges();
            }
        }

        public bool UnBan(int userId)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var userDb = context.Users.Include("UserRatings").FirstOrDefault(u => u.Id == userId);
                if (userDb != null && userDb.BanEndDate <= DateTime.Now)
                {
                    userDb.BanEndDate = null;
                    var uid = 0;
                    while (userDb.UserRatings.Count > 0)
                    {
                        var item = userDb.UserRatings.First();
                        uid = item.UserId;
                        context.UserRatings.DeleteObject(item);
                        context.SaveChanges();
                    }
                    context.UserRatings.AddObject(new UserRating(uid, ConfigurationConstants.Items.MinRating, "Reset to min rating after unban"));
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void Update(User user)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var userDb = context.Users.First(u => u.Id == user.Id);
                userDb = user;
                context.Users.ApplyCurrentValues(userDb);
                context.SaveChanges();
            }
        }
    }
}