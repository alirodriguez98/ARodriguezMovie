using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public byte IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public byte[] Imagen { get; set; } = null!;

    public byte IdCategoria { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
