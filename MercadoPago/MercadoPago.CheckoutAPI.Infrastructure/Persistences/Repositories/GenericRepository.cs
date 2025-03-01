using MercadoPago.CheckoutAPI.Domain.Entities;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            _entity = _storeDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity.AsNoTracking().ToListAsync();

            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            return getById;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            entity.FechaCreacion = DateTime.UtcNow;

            await _storeDbContext.AddAsync(entity);

            var recordsAffected = await _storeDbContext.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            entity.FechaActualizacion = DateTime.UtcNow;

            _storeDbContext.Update(entity);
            _storeDbContext.Entry(entity).Property(obj => obj.FechaCreacion).IsModified = false;

            var recordsAffected = await _storeDbContext.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Esto hace borrado físico, mas adelante agrego los campos para hacer borrado logico.
            _storeDbContext.Remove(id);

            var recordsAffected = await _storeDbContext.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public IQueryable<T> GetEntityQueryable(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
