using Business.Abstract;
using Core.Entity.Models;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public void AddRole(Role role)
        {
            _roleDal.Add(role);
        }

        public List<Role> GetAllRoles()
        {
            return _roleDal.GetAll();
        }

        public Role GetRole(int userId)
        {
           return _roleDal.GetUserRole(userId);
        }

        public void RemoveRole(Role role)
        {
            _roleDal.Delete(role);
        }

        public void UpdateRole(Role role)
        {
            _roleDal.Update(role);
        }
    }
}
