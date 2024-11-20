﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public int IdProvincia { get; set; }
        public int IdMetodoPago { get; set; }
        public string Ciudad { get; set; }
        public string CP { get; set; }
        public string Direccion { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal MontoTotal { get; set; }

    }
}