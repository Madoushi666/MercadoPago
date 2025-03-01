namespace MercadoPago.CheckoutAPI.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
