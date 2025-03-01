using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Customers.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface ICustomersApplication
    {
        Task<BaseResponse<object>> SearchCustomersAsync(CustomersRequestFilters filters);
        Task<BaseResponse<object>> GetCustomerByIdAsync(string customerId);
        Task<BaseResponse<object>> CreateCustomerAsync(CustomerRequest bodyRequest);
        Task<BaseResponse<object>> UpdateCustomerAsync(string customerId, CustomerRequest bodyRequest);
    }
}
