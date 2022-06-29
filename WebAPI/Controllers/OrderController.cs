using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [HttpPost("addorder")]
        public IActionResult AddOrder(OrderDTO model)
        {
            try
            {
                Order order = new()
                {
                    K205UserId = model.K205UserId,
                    IsDelivered = model.IsDelivered,
                    ProductId = model.ProductId,
                    OrderTrackingId = model.OrderTrackingId,
                    TotalPrice = model.TotalPrice,
                    TotalQuantity = model.TotalQuantity,
                };
                _orderManager.Add(order);

                return Ok(new { status = 200, message = "Sifarisiniz tamamlandi." });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 403, message = "Sifaris zamani xeta bas verdi!" });
            }
        }

        [HttpPut("update")]
         public IActionResult UpdateOrder(OrderDTO model)
         {
            try
            {
                Order order = new()
                {
                    K205UserId = model.K205UserId,
                    IsDelivered = model.IsDelivered,
                    ProductId = model.ProductId,
                    OrderTrackingId = model.OrderTrackingId,
                    TotalPrice = model.TotalPrice,
                    TotalQuantity = model.TotalQuantity,
                };
                _orderManager.Update(order);
                return Ok(new { status = 200, message = "Update olundu!" });
            }
            catch (Exception)
            {

                return Ok(new { status = 404, message = "Hele de islemirrr." });
            }
            
         }

        [HttpGet("getbtid/{id}")]
        public IActionResult GetByOrderId(int id)
        {
           var order = _orderManager.GetOrderById(id);
            return Ok(new {status = 200, message = order});
        }

        [HttpDelete("remove")]
        public IActionResult RemoveOrder(Order model)
        {
            _orderManager.Remove(model);
            return Ok(new { status = 200, message = "Silindi" });
        }

        [HttpGet("getall/{userId}")]
        public async Task<IActionResult> GetAllOrder(int userId)
        {
            var order = _orderManager.GetAll(userId);
            return Ok(new { status = 200, message = order });

        }




    }
}
