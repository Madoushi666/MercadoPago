using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPago.CheckoutAPI.Application.Interfaces.MercadoPago
{
    public interface IIdentificationTypesApplication
    {
        Task<BaseResponse<object>> GetIdentificationTypesAsync();
    }
}
