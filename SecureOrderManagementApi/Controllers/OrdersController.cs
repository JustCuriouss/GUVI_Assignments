using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureOrderManagementApi.Data;
using SecureOrderManagementApi.Models;

namespace SecureOrderManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var userName = User.Identity.Name;
            var orders = _context.Orders.Where(o => o.UserName == userName).ToList();
            return Ok(orders);
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            order.UserName = User.Identity.Name;
            order.TotalAmount = order.Quantity * order.UnitPrice;

            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(new { message = "Order placed successfully", orderId = order.Id });
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var userName = User.Identity.Name;
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }

            if (existingOrder.UserName != userName)
            {
                return Unauthorized("You do not have permission to delete this order.");
            }

            existingOrder.ProductName = updatedOrder.ProductName;
            existingOrder.Quantity = updatedOrder.Quantity;
            existingOrder.UnitPrice = updatedOrder.UnitPrice;
            existingOrder.TotalAmount = updatedOrder.Quantity * updatedOrder.UnitPrice;

            _context.SaveChanges();
            return Ok(new { message = "Order updated successfully" });
        }

        
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var userName = User.Identity.Name;
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            if (order.UserName != userName)
            {
                return Unauthorized("You do not have permission to delete this order.");
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok(new { message = "Order deleted successfully" });
        }
    }
}
