﻿using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Commons;
using System.Text.Json.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Models.MercadoPago.CustomerCards.Request
{
    // Reference: https://www.mercadopago.com.ar/developers/es/reference/cards/_customers_customer_id_cards_id/put

    public class CustomerCardRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("cardholder")]
        public CardHolder? CardHolder { get; set; }

        [JsonPropertyName("expiration_month")]
        public int ExpirationMonth { get; set; }

        [JsonPropertyName("expiration_year")]
        public int ExpirationYear { get; set; }
    }
}
