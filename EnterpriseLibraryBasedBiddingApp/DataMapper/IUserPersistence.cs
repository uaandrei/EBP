using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface IUserPersistence
    {
        void AddUser(User user);

        IList<User> GetAllUsers();

        IList<User> GetAllUsersWithBidsAndProducts();
        
        void UpdateUserWithNavigationProperties(ref User user);

        IList<UserRating> GetRating(int userId);

        void Ban(int userId);

        bool UnBan(int userId);

        User Get(int userId);
        
        void Update(User user);
    }
}