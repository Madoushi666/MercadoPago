using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Payments.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface IPaymentsApplication
    {
        Task<BaseResponse<object>> SearchPaymentsAsync(PaymentsRequestFilters filters);
        Task<BaseResponse<object>> GetPaymentByIdAsync(int paymentId);
        Task<BaseResponse<object>> CreatePaymentAsync(PaymentRequest bodyRequest);
        Task<BaseResponse<object>> UpdatePaymentAsync(int paymentId, PaymentRequest bodyRequest);
    }
}
