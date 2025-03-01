using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Users.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercadoPago.CheckoutAPI.Controllers.MercadoPago
{
    [Authorize]
    [Route("api/MercadoPago/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;

        public UsersController(IUsersApplication usersApplication)
        {
            _usersApplication = usersApplication;
        }

        [HttpGet("Me")]
        public async Task<IActionResult> GetMyUser()
        {
            var response = await _usersApplication.GetMyUserAsync();

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "administrator")]
        [HttpPost("Test")]
        public async Task<IActionResult> CreateTestUser([FromBody] CreateTestUserRequest bodyRequest)
        {
            var response = await _usersApplication.CreateTestUserAsync(bodyRequest);

            return StatusCode(response.StatusCode, response);
        }
    }
}
