using MercadoPago.CheckoutAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetUserByEmailAsync(string email);
    }
}
