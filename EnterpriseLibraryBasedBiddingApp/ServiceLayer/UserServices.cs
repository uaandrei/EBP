using DataMapper;
using DomainModel;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class UserServices
    {
        public void AddUser(User user)
        {
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddUser(user);
        }

        public IList<User> GetAllUsers()
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllUsers();
        }
    }
}