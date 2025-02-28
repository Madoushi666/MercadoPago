﻿using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Commons.Request;
using System.Text.Json.Serialization;

namespace MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Payments.Request
{
    // Reference: https://www.mercadopago.com.ar/developers/es/reference/payments/_payments_search/get

    public class PaymentsRequestFilters : RequestFilters
    {
        [JsonPropertyName("sort")]
        public string? Sort { get; set; }

        [JsonPropertyName("criteria")]
        public string? Criteria { get; set; }

        [JsonPropertyName("external_reference")]
        public string? ExternalReference { get; set; }

        [JsonPropertyName("range")]
        public string? Range { get; set; }

        [JsonPropertyName("begin_date")]
        public string? BeginDate { get; set; }

        [JsonPropertyName("end_date")]
        public string? EndDate { get; set; }

        [JsonPropertyName("store_id")]
        public string? StoreId { get; set; }

        [JsonPropertyName("pos_id")]
        public string? PosId { get; set; }

        [JsonPropertyName("collector.id")]
        public string? CollectorId { get; set; }

        [JsonPropertyName("payer.id")]
        public string? PayerId { get; set; }
    }
}
