namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuario { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
