using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int posVec { get; set; }
        public List<string> Imagenes;
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        //public Marca Marca { get; set; }

        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }

        public decimal Precio;
        public string PrecioFormateado => Precio.ToString("F2");
        public bool Estado { get; set; }

        public float Stock {  get; set; } 

        public Articulo()
        {
            posVec = 0;
            //Imagenes = new List<string>();
        }
    }
}
