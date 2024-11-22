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
               
                datos.setearConsulta("SELECT IDPRODUCTO, NOMBRE FROM PRODUCTOS");
                datos.ejecutarLectura();

                DataTable dtProductos = new DataTable();
                dtProductos.Load(datos.Lector);

               
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
                
                int idProducto = Convert.ToInt32(e.CommandArgument);
                EliminarProductoFisicamente(idProducto);
                CargarProductos();
            }
        }

        private void EliminarProductoFisicamente(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                datos.setearConsulta("DELETE FROM PRODUCTOS WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
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