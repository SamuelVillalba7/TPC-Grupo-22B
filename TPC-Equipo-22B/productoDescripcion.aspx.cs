using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_22B
{
    public partial class productoDescripcion : System.Web.UI.Page
    {
        public static Articulo articulo;
        public static List<Articulo> articulosRelacionados;
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["productoId"];
            if (id != null)
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                articulo = articuloNegocio.listarId(id);
                articulosRelacionados = articuloNegocio.listarArticulosRelacionados(articulo.Id, articulo.Categoria.Id);
            }
            else
            {
                Response.Redirect("filtros.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cantidadIngresada;
            ArticuloNegocio negocio = new ArticuloNegocio();
            int stockDisponible = negocio.ConsultarStock(int.Parse(id)); // Consulta el stock actual del producto

            if (int.TryParse(txtCantidad.Text, out cantidadIngresada))
            {
                if (cantidadIngresada > stockDisponible)
                {
                    // Mostrar mensaje de error
                    lblError.Text = "La cantidad ingresada excede el stock disponible.";
                }
                else if (cantidadIngresada <= 0)
                {
                    lblError.Text = "La cantidad debe ser mayor a 0.";
                }
                else
                {
                    // Si la cantidad es válida, agregar a la sesión y redirigir al carrito
                    Session.Add("idp", id);
                    Session.Add("Cantidad", cantidadIngresada.ToString());
                    Response.Redirect("carrito.aspx", false);
                }
            }
            else
            {
                lblError.Text = "Por favor, ingresa un valor válido.";
            }
        }
    }
}
