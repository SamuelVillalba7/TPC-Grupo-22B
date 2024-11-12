using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_22B
{
    public partial class EliminarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["usuario"] == null || ((dominio.Usuario)Session["usuario"]).Administrador == false))
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDPRODUCTO, NOMBRE FROM PRODUCTOS");
                datos.ejecutarLectura();

                ddlProductos.Items.Clear();
                ddlProductos.Items.Add(new ListItem("Seleccione un producto", ""));

                while (datos.Lector.Read())
                {
                    ddlProductos.Items.Add(new ListItem(datos.Lector["NOMBRE"].ToString(), datos.Lector["IDPRODUCTO"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (ddlProductos.SelectedValue == "")
            {
                // Opcional: Mostrar un mensaje de error indicando que debe seleccionar un producto.
                return;
            }

            int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);
            EliminarProductoFisicamente(idProducto);

            // Recargar la lista de productos después de eliminar
            CargarProductos();
        }

        private void EliminarProductoFisicamente(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();

                // Opcional: También eliminar imágenes asociadas al producto si es necesario
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el producto: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}