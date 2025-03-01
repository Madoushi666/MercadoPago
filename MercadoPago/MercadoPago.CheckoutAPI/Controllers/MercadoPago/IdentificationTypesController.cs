using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercadoPago.CheckoutAPI.Controllers.MercadoPago
{
    [Authorize]
    [Route("api/MercadoPago/[controller]")]
    [ApiController]
    public class IdentificationTypesController : ControllerBase
    {
        private readonly IIdentificationTypesApplication _identificationTypesApplication;
        public IdentificationTypesController(IIdentificationTypesApplication identificationTypesApplication)
        {
            _identificationTypesApplication = identificationTypesApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetIdentificationTypes()
        {
            var response = await _identificationTypesApplication.GetIdentificationTypesAsync();

            return StatusCode(response.StatusCode, response);
        }
    }
}
