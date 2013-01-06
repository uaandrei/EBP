using DomainModel;
using System.Collections.Generic;

namespace DataMapper
{
    public interface IUserPersistence
    {
        void AddUser(User user);

        IList<User> GetAllUsers();
    }
}