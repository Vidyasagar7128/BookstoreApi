using BookstoreManager.Interfaces;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromQuery] string email, [FromBody] string password)
        {
            try
            {
                var result = await this._userManager.Login(email, password);
                if (result.Contains("Login Successfully!"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();

                    string Name = database.StringGet("name");
                    string Email = database.StringGet("email");
                    string Mobile = database.StringGet("mobile");
                    CreateUserModel createUser = new CreateUserModel { Name = Name, Email = Email, Mobile = long.Parse(Mobile) };
                    return this.Ok(new { status = true, Response = result, Token = this._userManager.JwtToken(Email) });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Response = e.Message });
            }
        }
    }
}
