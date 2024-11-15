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
            if (!IsPostBack)  // Evitar la recarga en cada postback
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

                if (articuloNegocio.encontrarArticulo(prodcarrito, int.Parse(id)) == -1)
                {
                    itemCarrito.cantidad = int.Parse(Session["Cantidad"].ToString());
                    prodcarrito.Add(itemCarrito);
                }
                else
                {
                    prodcarrito[articuloNegocio.encontrarArticulo(prodcarrito, int.Parse(id))].cantidad += int.Parse(Session["Cantidad"].ToString());
                }

                Session.Add("carrito", prodcarrito);
                dgv_carrito.DataSource = prodcarrito;
                dgv_carrito.DataBind();
            }
        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    int idProductoAEliminar = int.Parse(btn.CommandArgument);

        //    ItemCarrito productoAEliminar = prodcarrito.FirstOrDefault(p => p.IdProducto == idProductoAEliminar);

        //    prodcarrito.Remove(productoAEliminar);

        //    Session["Carrito"] = prodcarrito;
        //    dgv_carrito.DataSource = prodcarrito;
        //    dgv_carrito.DataBind();
        //}

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            // Convertir el CommandArgument a un ID de producto
            int idProducto = Convert.ToInt32(e.CommandArgument);

            // Recuperar la lista del carrito desde la sesión
            List<ItemCarrito> prodcarrito = Session["carrito"] as List<ItemCarrito>;

            if (prodcarrito != null)
            {
                // Buscar el producto en el carrito
                ItemCarrito productoAEliminar = prodcarrito.FirstOrDefault(p => p.IdProducto == idProducto);

                if (productoAEliminar != null)
                {
                    // Eliminar el producto de la lista del carrito
                    prodcarrito.Remove(productoAEliminar);

                    // Actualizar la sesión
                    Session["carrito"] = prodcarrito;

                    // Volver a enlazar el GridView con los productos restantes en el carrito
                    dgv_carrito.DataSource = prodcarrito;
                    dgv_carrito.DataBind();
                }
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            prodcarrito = Session["carrito"] as List<ItemCarrito>;

            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
            int idProducto = Convert.ToInt32(dgv_carrito.DataKeys[row.RowIndex].Value);  // Asegúrate de tener "DataKeyNames" configurado correctamente

            ItemCarrito producto = prodcarrito.FirstOrDefault(p => p.IdProducto == idProducto);

            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            int nuevaCantidad;

            if (int.TryParse(txtCantidad.Text, out nuevaCantidad) && nuevaCantidad > 0)
            {
                // Actualizar la cantidad en el carrito
                producto.cantidad = nuevaCantidad;

                // Actualizar la sesión con el nuevo carrito
                Session["carrito"] = prodcarrito;
            }
            else
            {
                // Si la cantidad no es válida, restaurar la cantidad anterior
                txtCantidad.Text = producto.cantidad.ToString();
            }

            dgv_carrito.DataSource = prodcarrito;
            dgv_carrito.DataBind();  // Recargar la tabla para reflejar los cambios

        }
    }
}