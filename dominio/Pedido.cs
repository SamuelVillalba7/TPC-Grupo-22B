using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string NombreUsuario { get; set; }
        public int IdUsuario { get; set; }
        public string MetodoNombre { get; set; }
        public int IdMetodoPago { get; set; }
        public string EstadoNombre { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal MontoTotal { get; set; }
        public int Envio { get; set; } // 0: Retiro en tienda, 1: Envío a domicilio

    }
}
