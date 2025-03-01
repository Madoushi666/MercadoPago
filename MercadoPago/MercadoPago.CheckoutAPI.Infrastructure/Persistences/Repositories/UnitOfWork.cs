using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;

        private IUsuarioRepository _usuario = null;

        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public IUsuarioRepository Usuario => _usuario ?? new UsuarioRepository(_storeDbContext);

        public void Dispose()
        {
            _storeDbContext.Dispose();
        }

        public void SaveChanges()
        {
            _storeDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _storeDbContext.SaveChangesAsync();
        }
    }
}
