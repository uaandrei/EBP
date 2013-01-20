using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EntityFrameworkDataMapper
{
    class EfRolePersistence:IRolesPersistence
    {
        public void Add(DomainModel.Role role)
        {
            using (var context = new BiddingDataModelContainer())
            {
                context.Roles.AddObject(role);
                context.SaveChanges();
            }
        }

        public IList<DomainModel.Role> GetAll()
        {
            using (var context = new BiddingDataModelContainer())
            {
                return context.Roles.Include("Users").ToList();
            }
        }

        public void Update(DomainModel.Role role)
        {
            using (var context = new BiddingDataModelContainer())
            {
                var dbe = context.Roles.First(u => u.Id == role.Id);
                dbe = role;
                context.Roles.ApplyCurrentValues(dbe);
                context.SaveChanges();
            }
        }
    }
}
