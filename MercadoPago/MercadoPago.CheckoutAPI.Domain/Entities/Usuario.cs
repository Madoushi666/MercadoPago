namespace MercadoPago.CheckoutAPI.Domain.Entities;

public partial class Usuario : BaseEntity
{
    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;
}
