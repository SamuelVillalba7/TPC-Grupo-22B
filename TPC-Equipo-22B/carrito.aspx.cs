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
        List<ItemCarrito> prodcarrito;
        Articulo agregar = new Articulo();
        protected void Page_Load(object sender, EventArgs e)
        {
            prodcarrito = Session["carrito"] as List<ItemCarrito>;

            if (prodcarrito == null)
            {
                prodcarrito = new List<ItemCarrito>();
            }


            ItemCarrito itemCarrito = new ItemCarrito();

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            string id = Session["idp"].ToString();
            itemCarrito.art = articuloNegocio.listarId(id);

            if(articuloNegocio.encontrarArticulo(prodcarrito, int.Parse(id)) == -1)
            {
                itemCarrito.cantidad = 1;
                prodcarrito.Add(itemCarrito);
            }
            else
            {
                prodcarrito[articuloNegocio.encontrarArticulo(prodcarrito, int.Parse(id))].cantidad += 1;
            }

            Session.Add("carrito", prodcarrito);

            dgv_carrito.DataSource = prodcarrito;
            dgv_carrito.DataBind();
        }
    }
}