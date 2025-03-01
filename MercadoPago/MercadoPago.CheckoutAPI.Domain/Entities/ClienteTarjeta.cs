using System;
using System.Collections.Generic;

namespace MercadoPago.CheckoutAPI.Domain.Entities;

public partial class ClienteTarjeta : BaseEntity
{
    public string IdClienteMp { get; set; } = null!;

    public long IdTarjetaMp { get; set; }

    public string Tipo { get; set; } = null!;

    public string MesExpiracion { get; set; } = null!;

    public string AnioExpiracion { get; set; } = null!;

    public string PrimerosSeisDigitos { get; set; } = null!;

    public string UltimosCuatroDigitos { get; set; } = null!;

    public string? Titular { get; set; }
}
