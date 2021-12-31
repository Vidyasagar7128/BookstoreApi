using BookstoreManager.Interfaces;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            try
            {
                var result = await this._userManager.AddUser(createUserModel);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Response = "Registration successful." });
                }
                else
                {
                    return this.BadRequest("Something went wrong!");
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Response = e.Message });
            }
        }
    }
}
