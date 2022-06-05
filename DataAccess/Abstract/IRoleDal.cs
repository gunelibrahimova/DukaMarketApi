using Core.DataAccess;
using Core.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRoleDal : EfEntityRepositoryBase<Role>
    {
        Role GetUserRole(int userId);
    }
}
