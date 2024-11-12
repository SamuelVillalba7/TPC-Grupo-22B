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
    public partial class AdministrarCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["usuario"] == null || ((dominio.Usuario)Session["usuario"]).Administrador == false))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable dtCategorias = new DataTable();

            try
            {
                datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE FROM CATEGORIAS");
                datos.ejecutarLectura();

                dtCategorias.Load(datos.Lector);
                gvCategorias.DataSource = dtCategorias;
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreCategoria.Text))
            {
                // Mensaje de error si el nombre está vacío
                return;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (NOMBRE) VALUES (@nombre)");
                datos.setearParametro("@nombre", txtNombreCategoria.Text);
                datos.ejecutarAccion();

                // Recargar la lista de categorías
                CargarCategorias();
                txtNombreCategoria.Text = string.Empty; // Limpiar el campo de texto
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE IDCATEGORIA = @id");
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarAccion();

                // Recargar la lista de categorías
                CargarCategorias();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}