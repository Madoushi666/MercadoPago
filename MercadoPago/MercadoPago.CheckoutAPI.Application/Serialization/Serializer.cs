﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MercadoPago.CheckoutAPI.Application.Serialization
{
    public class Serializer : ISerializer
    {
        private readonly ILogger<Serializer> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public Serializer(ILogger<Serializer> logger)
        {
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions() 
            { 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
        }

        public HttpRequestMessage AddJsonBodyToContent<T>(HttpRequestMessage request, T bodyRequest)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(bodyRequest, _jsonSerializerOptions), Encoding.UTF8, "application/json");

            return request;
        }

        public async Task<T> DeserializeJsonAsync<T>(HttpResponseMessage response)
        {
            if (response is null || response.Content is null)
                return default;

            try
            {
                var jsonStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(jsonStream);
            }
            catch (JsonException ex)
            {
                _logger.LogError("Error de deserialización: {Exception}", ex);
                return default;
            }
        }

        public string SetQueryParams<T>(T filters)
        {
            StringBuilder query = new StringBuilder().Append("");

            if (filters is null)
                return query.ToString();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(filters);
                if (value is not null)
                {
                    var name = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;

                    if (query.Length > 0)
                    {
                        query.Append("&");
                    }

                    query.Append($"{name}={Uri.EscapeDataString(value.ToString())}");
                }
            }

            if (query.Length > 0)
            {
                query.Insert(0, "?");
            }

            return query.ToString();
        }
    }
}
