using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class DatosEnvio
    {
        public int IdDatosEnvio { get; set; }
        public int IdPedido { get; set; }
        public int IdProvincia { get; set; }
        public string Ciudad { get; set; }
        public string CP { get; set; }
        public string Direccion { get; set; }
    }
}
