using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTrackingController : ControllerBase
    {
        private readonly IOrderTrackingManager _orderTrackingManager;

        public OrderTrackingController(IOrderTrackingManager orderTrackingManager)
        {
            _orderTrackingManager = orderTrackingManager;
        }


        [HttpPost("addOrderTracking")]
        public IActionResult AddOrderTracking(OrderTracking orderTracking )
        {
            _orderTrackingManager.Add(orderTracking);
            return Ok(new { status = 200, message = "Elave olundu" });
        }

        [HttpPut("update")]
        public IActionResult UpdateOrderTracking(OrderTracking orderTracking)
        {
            _orderTrackingManager.Update(orderTracking);
            return Ok(new { status = 200, message = "Update olundu" });
        }

        [HttpDelete("delete")]
        public IActionResult DeleteOrderTracking(OrderTracking orderTracking)
        {
            _orderTrackingManager.Remove(orderTracking);
            return Ok(new { status = 200, message = "Silindi" });
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetByIdOrderTracking(int id)
        {
            var orderTracking = _orderTrackingManager.GetOrderTrackingById(id);
            return Ok(new { status = 200, message = orderTracking });
        }

    }
}
