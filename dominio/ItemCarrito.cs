using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemCarrito
    {
        public Articulo art;
        public int cantidad { get; set; }

        public string nombreProducto
        {
            get { return art.Nombre; }
        }

        public int IdProducto
        {
            get { return art.Id; }
        }

        public decimal Subtotal
        {
            get { return art.Precio * cantidad; }
        }
    }
}
