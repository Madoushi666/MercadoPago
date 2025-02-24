﻿using MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago;
using MercadoPago.CheckoutAPI.Application.Models.CardToken.Request;
using MercadoPago.CheckoutAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercadoPago.CheckoutAPI.Controllers.MercadoPago
{
    [Authorize(Roles = "administrator")]
    [Route("api/MercadoPago/[controller]")]
    [ApiController]
    public class CardTokensController : ControllerBase
    {
        private readonly ICardTokensApplication _cardTokensApplication;

        public CardTokensController(ICardTokensApplication cardTokensApplication)
        {
            _cardTokensApplication = cardTokensApplication;
        }

        // USAR ESTE ENDPOINT SOLAMENTE PARA PRUEBAS, POR SEGURIDAD EL TOKEN DEBE GENERARSE EN EL FRONTEND APUNTANDO DIRECTAMENTE HACIA LA API DE MERCADO PAGO
        [HttpPost]
        public async Task<IActionResult> CreateCardToken([FromBody] CreateCardTokenRequest bodyRequest)
        {
            var response = await _cardTokensApplication.CreateCardToken(bodyRequest);

            return response.ReturnStatusCode(this);
        }
    }
}
