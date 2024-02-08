using System;
using System.Collections.Generic;

namespace DL;

public partial class Detalle
{
    public byte IdProducto { get; set; }

    public int IdVenta { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
