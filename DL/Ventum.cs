using System;
using System.Collections.Generic;

namespace DL;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdUsuario { get; set; }

    public byte IdMetodoPago { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
