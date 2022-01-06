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
    public class AddressController : Controller
    {
        private readonly IAddressManager _addressManager;
        private readonly IAuth _auth;
        private StringValues head;
        public AddressController(IAddressManager addressManager,IAuth auth)
        {
            this._addressManager = addressManager;
            this._auth = auth;
        }

        /// <summary>
        /// Adding address to user
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>added or not</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAddressToUser([FromBody] AddressModel address)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                address.UserId = userId;
                int result = await this._addressManager.AddAddress(address);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Address added successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Adding address to user
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>added or not</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAddressToUser([FromBody] AddressModel address)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                address.UserId = userId;
                int result = await this._addressManager.UpdateAddress(address);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Address updated successfully." });
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

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAddressToUser([FromQuery] int addressId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._addressManager.DeleteAddress(userId, addressId);
                if (result == 1)
                {
                    return this.Ok(new { status = true, Message = "Address deleted successfully." });
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

        [HttpGet]
        [Route("address")]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int id = await this._auth.ValidateJwtToken(this.head);
                IEnumerable<AddressModel> result = await this._addressManager.ListAddress(id);
                if (result.Count() >= 1)
                {
                    return this.Ok(new { status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }
    }
}
