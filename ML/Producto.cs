﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public byte IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
        public ML.Categoria Categoria { get; set; }
        public List<object> Productos { get; set; }
    }
}
