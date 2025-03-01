namespace MercadoPago.CheckoutAPI.Domain.Entities;

public partial class Cliente : BaseEntity
{
    public string IdClienteMP { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }
}
