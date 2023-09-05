using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockCore.DTOs.Account;
using StockCore.Entities;
using StockCore.Interfaces;
using StockCore.Services;
using System.Net;

namespace StockCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            var account = new Account { 

                Username = registerRequest.username,
                Password = registerRequest.password,
                RoleId = registerRequest.roleid

            };

            await accountService.Register(account);
            return StatusCode((int) HttpStatusCode.Created);
        }
   
        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
                //var account = new Account
                //{

                //    Username = loginRequest.username,
                //    Password = loginRequest.password,

                //};
                var acc = await accountService.Login(loginRequest.username, loginRequest.password);
                if(acc == null)
                {
                    return Unauthorized();
                }


                return Ok(new
                {
                    //token = "sdfsfsfsadfasf" 
                    token = accountService.GenerateToken(acc)
                });
        }
        [HttpGet("[action]")] //enotation
        public async Task<ActionResult> Info(LoginRequest loginRequest)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if(accessToken == null)
            {
                return Unauthorized();
            }

            var account = accountService.GetInfo(accessToken);
            return Ok(new
            {
                username = account.Username,
                role = account.Role.Name
            });
        }
    }
}
