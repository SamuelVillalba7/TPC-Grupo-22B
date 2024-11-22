using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            if (!IsPostBack)
            {
                lblMensaje.Visible = false; // Ocultar el mensaje al cargar la página
                prodcarrito = Session["carrito"] as List<ItemCarrito> ?? new List<ItemCarrito>();

                
                if (Session["idp"] != null && Session["Cantidad"] != null)
                {
                    string id = Session["idp"].ToString();
                    string cantidadSesion = Session["Cantidad"].ToString();

                    if (int.TryParse(cantidadSesion, out int cantidadSolicitada))
                    {
                        ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                        Articulo articulo = articuloNegocio.listarId(id);

                        if (articuloNegocio.ConsultarStock(int.Parse(id)) >= cantidadSolicitada)
                        {
                            
                            int indice = articuloNegocio.encontrarArticulo(prodcarrito, int.Parse(id));
                            if (indice == -1)
                            {
                                prodcarrito.Add(new ItemCarrito
                                {
                                    art = articulo,
                                    cantidad = cantidadSolicitada
                                });
                            }
                            else
                            {
                                prodcarrito[indice].cantidad += cantidadSolicitada;
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "No hay suficiente stock disponible para añadir al carrito.";
                            lblMensaje.Visible = true;
                        }
                    }
                    
                    Session["idp"] = null;
                    Session["Cantidad"] = null;
                }

                
                Session["carrito"] = prodcarrito;
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
            
            int idProducto = Convert.ToInt32(e.CommandArgument);

            
            List<ItemCarrito> prodcarrito = Session["carrito"] as List<ItemCarrito>;

            if (prodcarrito != null)
            {
                
                ItemCarrito productoAEliminar = prodcarrito.FirstOrDefault(p => p.IdProducto == idProducto);

                if (productoAEliminar != null)
                {
                    
                    prodcarrito.Remove(productoAEliminar);

                    // Actualizar la sesiion
                    Session["carrito"] = prodcarrito;
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
                
                producto.cantidad = nuevaCantidad;
                Session["carrito"] = prodcarrito;
            }
            else
            {
         
                txtCantidad.Text = producto.cantidad.ToString();
            }

            dgv_carrito.DataSource = prodcarrito;
            dgv_carrito.DataBind();  

        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            
            if(Session["usuario"] == null)
            {
                lblMensaje.Text = "Debe loguearse antes de finalizar la compra.";
                lblMensaje.Visible = true;
                return;
            }


            var carrito = Session["carrito"] as List<ItemCarrito>;
            if (carrito == null || carrito.Count == 0)
            {
                lblMensaje.Text = "El carrito está vacío. Agrega productos antes de finalizar la compra.";
                lblMensaje.Visible = true;
                return;
            }

            
            Console.WriteLine("Contenido del carrito:");
            foreach (var item in carrito)
            {
                Console.WriteLine($"Producto: {item.art.Nombre}, Cantidad: {item.cantidad}, Subtotal: {item.art.Precio * item.cantidad}");
            }

          
            Response.Redirect("FinalizarCompra.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}