using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Detalle
    {
        public ML.Producto Producto { get; set; }
        public ML.Venta Venta { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public List<object> Detalles { get; set; }
    }
}
