using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class MetodoPago
    {
        public byte IdMetodoPago { get; set; }
        public string Tipo { get; set; }
        public List<object> MetodosPago { get; set; }
    }
}
