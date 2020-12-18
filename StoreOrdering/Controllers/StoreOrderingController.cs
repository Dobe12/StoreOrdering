using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StoreOrdering.Data;
using StoreOrdering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StoreOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreOrderingController : ControllerBase
    {
        private readonly StoreContext _context;

        public StoreOrderingController(StoreContext context)
        {
            _context = context;
        }

        private IQueryable<Order> AllOrders
        {
            get
            {
                return _context.Orders
                    .Include(o => o.Creator)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.Items);
            }
        }

        // GET api/[controller]/orders
        [HttpGet("orders")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var allOrders = await AllOrders.ToListAsync();

            if (allOrders == null)
            {
                NotFound("Orders not found");
            }

            return Ok(allOrders);
        }

        // GET api/[controller]/orders/{userId}
        [HttpGet("orders/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) //+identity check, but it simple example
            {
                NotFound("User not found");
            }

            var orders = await AllOrders.Where(o => o.CreatorId == userId).ToListAsync();

            if (orders == null)
            {
                NotFound("Orders not found");
            }

            return Ok(orders);
        }

        // Post api/[controller]/order/{userId}/{cartId}
        [HttpGet("order/{userId}/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Order(int userId, int cartId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                NotFound("Cart not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) // + identity check, but it simple example
            {
                NotFound("User not found");
            }

            var order = new Order()
            {
                Creator = user,
                Cart = cart
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Post api/[controller]/removeorder/{userId}/{orderId}
        [HttpGet("removeorder/{userId}/{orderId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveOrder(int userId, int orderId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) //+identity check, but it simple example
            {
                NotFound("User not found");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId 
                                                                       && o.CreatorId == user.Id);

            if (order == null)
            {
                NotFound("Order not found");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Post api/[controller]/changeorder/{userId}/{orderId}/{cartId}
        [HttpGet("changeorder/{userId}/{orderId}/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeOrder(int userId, int orderId, int cartId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) //+identity check, but it simple example
            {
                NotFound("User not found");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                NotFound("Cart not found");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId
                                                                       && o.CreatorId == user.Id);
            if (order == null)
            {
                NotFound("Order not found");
            }

            order.Cart = cart;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}