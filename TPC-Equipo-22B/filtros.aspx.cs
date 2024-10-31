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
    public partial class filtros : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listarConSP();
            Session.Add("listaArticulos", ListaArticulo);
        }

        protected void filtroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List <Articulo>) Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(filtroRapido.Text.ToUpper()));
            ListaArticulo = listaFiltrada;
        }

        
    }
}