using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.PaymentMethods.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface IPaymentMethodsApplication
    {
        Task<BaseResponse<object>> SearchPaymentMethodsAsync(PaymentMethodsRequestFilters filters);
        Task<BaseResponse<object>> GetPaymentMethodsAsync();
        Task<BaseResponse<object>> GetInstallmentsAsync(PaymentMethodsRequestFilters filters);
    }
}
