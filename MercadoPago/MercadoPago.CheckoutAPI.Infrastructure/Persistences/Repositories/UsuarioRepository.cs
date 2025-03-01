using MercadoPago.CheckoutAPI.Domain.Entities;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public UsuarioRepository(StoreDbContext storeDbContext) : base(storeDbContext) 
        {
            _storeDbContext = storeDbContext;
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            var user = await _storeDbContext.Usuarios.Include(usuario => usuario.IdRolNavigation).FirstOrDefaultAsync(x => x.Email.Equals(email));

            return user;
        }
    }
}
