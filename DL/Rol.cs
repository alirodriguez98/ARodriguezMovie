using System;
using System.Collections.Generic;

namespace DL;

public partial class Rol
{
    public byte IdRol { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
