﻿using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.Payments.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface IPaymentsApplication
    {
        Task<BaseResponse<T>> SearchPayments<T>(SearchPaymentsRequestFilters filters);
        Task<BaseResponse<T>> GetPaymentById<T>(int paymentId);
        Task<BaseResponse<T>> CreatePayment<T>(CreatePaymentRequest bodyRequest);
        Task<BaseResponse<T>> UpdatePayment<T>(int paymentId, UpdatePaymentRequest bodyRequest);
    }
}
