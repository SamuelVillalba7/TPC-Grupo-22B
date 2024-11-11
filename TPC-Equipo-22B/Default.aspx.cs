using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;
namespace TPC_Equipo_22B
{
    public partial class home : System.Web.UI.Page
    {
        public static List<Categoria> ListaCategoria { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ListaCategoria = negocio.listarConSP();

           
        }
    }
}