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
    public partial class AdministrarMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null || !((dominio.Usuario)Session["usuario"]).Administrador)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable dtMarcas = new DataTable();

            try
            {
                datos.setearConsulta("SELECT IDMARCA, NOMBRE, ESTADO FROM MARCAS");
                datos.ejecutarLectura();

                dtMarcas.Load(datos.Lector);
                gvMarcas.DataSource = dtMarcas;
                gvMarcas.DataBind();
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

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreMarca.Text))
            {
                // Validar que el nombre no esté vacío
                return;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (NOMBRE, ESTADO) VALUES (@nombre, @estado)");
                datos.setearParametro("@nombre", txtNombreMarca.Text);
                datos.setearParametro("@estado", chkEstadoMarca.Checked);
                datos.ejecutarAccion();

                CargarMarcas(); // Recargar la tabla de marcas
                txtNombreMarca.Text = string.Empty; // Limpiar campos
                chkEstadoMarca.Checked = true;
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

        protected void gvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMarcas.EditIndex = e.NewEditIndex;
            CargarMarcas();
        }

        protected void gvMarcas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMarcas.EditIndex = -1;
            CargarMarcas();
        }

        protected void gvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idMarca = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)gvMarcas.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            bool estado = ((CheckBox)gvMarcas.Rows[e.RowIndex].FindControl("chkEstadoEdit")).Checked;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET NOMBRE = @nombre, ESTADO = @estado WHERE IDMARCA = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@estado", estado);
                datos.setearParametro("@id", idMarca);
                datos.ejecutarAccion();

                gvMarcas.EditIndex = -1;
                CargarMarcas();
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

        protected void gvMarcas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idMarca = Convert.ToInt32(gvMarcas.DataKeys[e.RowIndex].Value);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE IDMARCA = @id");
                datos.setearParametro("@id", idMarca);
                datos.ejecutarAccion();

                CargarMarcas();
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
    }
}