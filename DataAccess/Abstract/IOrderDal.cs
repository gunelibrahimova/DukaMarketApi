using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal : EfEntityRepositoryBase<Order>
    {
        List<Order> GetOrder(int userId);
        List<OrderDTO> GetUserOrders(int userId);
    }
}
