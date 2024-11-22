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
            int stockMaximo = negocio.ConsultarStock(int.Parse(Request.QueryString["productoId"])); // Aquí va el valor máximo que has definido en el front-end

            if (int.TryParse(txtCantidad.Text, out cantidadIngresada))
            {
                if (cantidadIngresada > stockMaximo)
                {
                    // Mostrar mensaje de error o corregir el valor a su máximo permitido
                    lblError.Text = "La cantidad ingresada excede el stock disponible.";
                    txtCantidad.Text = stockMaximo.ToString(); // Opcionalmente, ajustar el valor al máximo permitido
                }
                else
                {
                    Session.Add("idp", id);
                    Session.Add("Cantidad", txtCantidad.Text);
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
