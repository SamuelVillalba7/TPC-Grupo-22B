using negocio;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (Session["usuario"] == null || ((dominio.Usuario)Session["usuario"]).Administrador == false)
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
                // Obtener los productos para llenar la grilla
                datos.setearConsulta("SELECT IDPRODUCTO, NOMBRE FROM PRODUCTOS");
                datos.ejecutarLectura();

                // Convertir los datos a DataTable
                DataTable dtProductos = new DataTable();
                dtProductos.Load(datos.Lector);

                // Asignar el DataSource y enlazar los datos
                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtener el ID del producto seleccionado
                int idProducto = Convert.ToInt32(e.CommandArgument);

                // Eliminar el producto
                EliminarProductoFisicamente(idProducto);

                // Recargar la lista de productos después de eliminar
                CargarProductos();
            }
        }

        private void EliminarProductoFisicamente(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Eliminar el producto de la base de datos
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();

                // Opcional: Eliminar imágenes asociadas al producto
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar producto: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}