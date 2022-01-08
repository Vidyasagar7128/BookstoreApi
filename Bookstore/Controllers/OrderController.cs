using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IAuth _auth;
        private StringValues head;
        public OrderController(IOrderManager orderManager, IAuth auth)
        {
            this._auth = auth;
            this._orderManager = orderManager;
        }

        /// <summary>
        /// Add USer Order
        /// </summary>
        /// <param name="order">passing order model</param>
        /// <returns>Add USer Order or not</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUSerOrder([FromBody] OrderModel order)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._orderManager.AddOrder(order, userId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Order placed successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// cancle order
        /// </summary>
        /// <param name="orderId">passing orderId</param>
        /// <returns>cancle order or not</returns>
        [HttpPut]
        [Route("cancle")]
        public async Task<IActionResult> CancleUSerOrder([FromQuery] int orderId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._orderManager.CancleOrder(userId, orderId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Order cancled successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// return all orders by user
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> ShowBooks()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                IEnumerable<OrderDetailsModel> result = await this._orderManager.ShowOrderList(userId);
                if (result.Count() >= 1)
                {
                    return this.Ok(new { status = true, Response = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }
    }
}
