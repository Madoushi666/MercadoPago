namespace MercadoPago.CheckoutAPI.Domain.Entities;

public partial class Pago : BaseEntity
{
    public long IdPagoMp { get; set; }

    public string Estado { get; set; } = null!;

    public string IdMetodoPago { get; set; } = null!;

    public decimal ImporteTotal { get; set; }

    public DateTime FechaAprobacion { get; set; }
}
