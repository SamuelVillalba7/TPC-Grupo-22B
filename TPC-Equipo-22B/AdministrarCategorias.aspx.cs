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
                datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE, ESTADO, URLIMAGEN,VISIBLE, ORDEN FROM Categorias");
                datos.ejecutarLectura();

                dtCategorias.Load(datos.Lector);
                gvCategorias.DataSource = dtCategorias;
                gvCategorias.DataBind();


                DataView dvCategoriasVisible = new DataView(dtCategorias);
                gvCategoriasVisible.DataSource = dvCategoriasVisible;
                gvCategoriasVisible.DataBind();




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


        ///////datagrifview visible
        ///
        protected void btnEditar_Click(object sender, EventArgs e)
        {



            // Habilitar los CheckBox en la columna "Visible"
            foreach (GridViewRow row in gvCategoriasVisible.Rows)
            {
                CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");
                if (chkVisible != null)
                {
                    chkVisible.Enabled = true;
                }
            }

            // Cambiar el estado de los botones
            btnEditar.Enabled = false;
            btnActualizar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        protected void actualizarVisible(int idCategoria, bool nuevoValorVisible)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Actualizar cada fila en la base de datos
                datos.setearConsulta("UPDATE CATEGORIAS SET VISIBLE = @visible WHERE IDCATEGORIA = @id");
                datos.setearParametro("@visible", nuevoValorVisible);
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            try
            {
                int categoriasVisibles = 0;

                // Contar cuántas categorías tienen VISIBLE = true
                foreach (GridViewRow row in gvCategoriasVisible.Rows)
                {
                    CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");
                    if (chkVisible != null && chkVisible.Checked)
                    {
                        categoriasVisibles++;
                    }
                }

                // Validar que haya exactamente 7 categorías visibles
                if (categoriasVisibles != 7)
                {
                    lblMensaje.Text = "Debe haber exactamente 7 categorías visibles.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                foreach (GridViewRow row in gvCategoriasVisible.Rows)
                {
                    int idCategoria = Convert.ToInt32(gvCategoriasVisible.DataKeys[row.RowIndex].Value);
                    CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");

                    if (chkVisible != null)
                    {
                        bool nuevoValorVisible = chkVisible.Checked;

                        actualizarVisible(idCategoria, nuevoValorVisible);

                    }
                }

                // Deshabilitar los CheckBox después de actualizar
                foreach (GridViewRow row in gvCategoriasVisible.Rows)
                {
                    CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");
                    if (chkVisible != null)
                    {
                        chkVisible.Enabled = false;
                    }
                }

                // Cambiar el estado de los botones
                btnEditar.Enabled = true;
                btnActualizar.Enabled = false;
                btnCancelar.Enabled = false;

                // Recargar los datos para reflejar cambios
                CargarCategorias();
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Deshabilitar los CheckBox
            foreach (GridViewRow row in gvCategoriasVisible.Rows)
            {
                CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");
                if (chkVisible != null)
                {
                    chkVisible.Enabled = false;
                }
            }

            // Cambiar el estado de los botones
            btnEditar.Enabled = true;
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = false;
            lblMensaje.Text = "";
            // Recargar los datos originales
            CargarCategorias();
        }




    }
}
