using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper
{
    public interface IRolesPersistence
    {
        void Add(Role role);

        IList<Role> GetAll();

        void Update(Role role);
    }
}
