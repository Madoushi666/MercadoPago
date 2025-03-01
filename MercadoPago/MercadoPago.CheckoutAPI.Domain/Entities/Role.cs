namespace MercadoPago.CheckoutAPI.Domain.Entities;

public partial class Role : BaseEntity
{
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
