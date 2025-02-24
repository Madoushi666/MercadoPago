﻿using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Models.MercadoPago.PaymentMethods.Request;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface IPaymentMethodsApplication
    {
        Task<BaseResponse<T>> SearchPaymentMethods<T>(SearchPaymentMethodsRequestFilters filters);
        Task<BaseResponse<T>> GetPaymentMethods<T>();
    }
}
