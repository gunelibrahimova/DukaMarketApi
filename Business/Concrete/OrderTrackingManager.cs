using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderTrackingManager : IOrderTrackingManager
    {
        private readonly IOrderTrackingDal _orderTrackingDal;

        public OrderTrackingManager(IOrderTrackingDal orderTrackingDal)
        {
            _orderTrackingDal = orderTrackingDal;
        }

        public void Add(OrderTracking orderTracking)
        {
            _orderTrackingDal.Add(orderTracking);
        }

        public OrderTracking GetOrderTrackingById(int id)
        {
           return _orderTrackingDal.Get(x=>x.Id == id);
        }

        public void Remove(OrderTracking orderTracking)
        {
            _orderTrackingDal.Delete(orderTracking);
        }

        public void Update(OrderTracking orderTracking)
        {
            _orderTrackingDal.Update(orderTracking);
        }
    }
}
