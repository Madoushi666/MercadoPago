﻿using MercadoPago.CheckoutAPI.Models.Commons.Response;
using MercadoPago.CheckoutAPI.Models.Customers.Request;

namespace MercadoPago.CheckoutAPI.Interfaces
{
    public interface ICustomersService
    {
        // Clientes
        Task<BaseResponse> SearchCustomers(SearchCustomersRequestFilters filters);
        Task<BaseResponse> GetCustomerById(string customerId);
        Task<BaseResponse> CreateCustomer(CreateCustomerRequest bodyRequest);
        Task<BaseResponse> UpdateCustomer(string customerId, UpdateCustomerRequest bodyRequest);

        // Tarjetas
        Task<BaseResponse> GetCustomerCards(string customerId);
        Task<BaseResponse> GetCustomerCardById(string customerId, string cardId);
        Task<BaseResponse> CreateCustomerCard(string customerId, CreateCustomerCardRequest bodyRequest);
        Task<BaseResponse> UpdateCustomerCard(string customerId, string cardId, UpdateCustomerCardRequest bodyRequest);
        Task<BaseResponse> DeleteCustomerCard(string customerId, string cardId);
    }
}
