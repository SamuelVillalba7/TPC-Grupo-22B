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
    public partial class carrito : System.Web.UI.Page
    {
        List<Articulo> prodcarrito = new List<Articulo>();
        Articulo agregar = new Articulo();
        protected void Page_Load(object sender, EventArgs e)
        {
            prodcarrito = Session["carrito"] as List<Articulo>;

            if (prodcarrito == null)
            {
                prodcarrito = new List<Articulo>();
            }
   


            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            string id = Session["idp"].ToString();
            agregar = articuloNegocio.listarId(id);
            prodcarrito.Add(agregar);
            Session.Add("carrito", prodcarrito);

            dgv_carrito.DataSource = prodcarrito;
            dgv_carrito.DataBind();
        }
    }
}