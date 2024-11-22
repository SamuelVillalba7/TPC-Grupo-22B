using negocio;
using System;
using System.Data;
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
                datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE, ESTADO, URLIMAGEN, ORDEN FROM Categorias");
                datos.ejecutarLectura();

                dtCategorias.Load(datos.Lector);
                gvCategorias.DataSource = dtCategorias;
                gvCategorias.DataBind();
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

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreCategoria.Text) || string.IsNullOrEmpty(txtURLCategoria.Text))
            {
                return; // Validar que los campos no estén vacíos
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (NOMBRE, ESTADO, URLIMAGEN) VALUES (@nombre, @estado, @url)");
                datos.setearParametro("@nombre", txtNombreCategoria.Text);
                datos.setearParametro("@estado", chkEstadoCategoria.Checked);
                datos.setearParametro("@url", txtURLCategoria.Text);
                datos.ejecutarAccion();

                CargarCategorias();
                txtNombreCategoria.Text = string.Empty;
                txtURLCategoria.Text = string.Empty;
                chkEstadoCategoria.Checked = true; // Por defecto activo
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

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;
            CargarCategorias();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);
            string nombre = ((TextBox)gvCategorias.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            CheckBox chkEstadoEdit = (CheckBox)gvCategorias.Rows[e.RowIndex].FindControl("chkEstadoEdit");
            string urlImagen = ((TextBox)gvCategorias.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET NOMBRE = @nombre, ESTADO = @estado, URLIMAGEN = @url WHERE IDCATEGORIA = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@estado", chkEstadoEdit.Checked);
                datos.setearParametro("@url", urlImagen);
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarAccion();

                gvCategorias.EditIndex = -1;
                CargarCategorias();
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

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE IDCATEGORIA = @id");
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarAccion();

                CargarCategorias();
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

        //protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int idCategoria = Convert.ToInt32(e.CommandArgument);

        //    CategoriaNegocio negocio = new CategoriaNegocio();
        //    if (e.CommandName == "Subir")
        //    {
        //        negocio.CambiarOrden(idCategoria, true); // True indica que se debe subir.
        //    }
        //    else if (e.CommandName == "Bajar")
        //    {
        //        negocio.CambiarOrden(idCategoria, false); // False indica que se debe bajar.
        //    }

        //    // Recargar categorías después del cambio.
        //    CargarCategorias();
        //}

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Subir" || e.CommandName == "Bajar")
            {
                int categoriaId = Convert.ToInt32(e.CommandArgument);
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (e.CommandName == "Subir")
                {
                    negocio.CambiarOrden(categoriaId, "Subir");
                }
                else if (e.CommandName == "Bajar")
                {
                    negocio.CambiarOrden(categoriaId, "Bajar");
                }

                // Recargar el GridView después de actualizar el orden
                CargarCategorias();
            }
        }


    }
}
