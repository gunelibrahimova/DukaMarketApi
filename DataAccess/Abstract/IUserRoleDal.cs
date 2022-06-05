﻿using Core.DataAccess;
using Core.Entity.Models;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRoleDal : EfEntityRepositoryBase<UserRole>
    {
        List<UserRoleDTO> GetAllUserRole();
        UserRoleDTO GetUserRole(int id);
    }
}
