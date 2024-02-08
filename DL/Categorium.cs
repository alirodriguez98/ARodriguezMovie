using System;
using System.Collections.Generic;

namespace DL;

public partial class Categorium
{
    public byte IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
