using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet("{appuserid}")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders(int appUserId)
        {
            var result = await _orderService.GetAllAsync(appUserId);

            return Ok(result);
        }

        [HttpGet("order-detail/{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> PostOrder(OrderModel newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _orderService.CreateOrderAsync(newOrder);

            return Ok();
        }
    }
}
