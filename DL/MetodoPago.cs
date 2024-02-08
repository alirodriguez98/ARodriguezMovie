using System;
using System.Collections.Generic;

namespace DL;

public partial class MetodoPago
{
    public byte IdMetodoPago { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
