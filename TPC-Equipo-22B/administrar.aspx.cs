using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using negocio;
using System.Web.UI;

namespace TPC_Equipo_22B
{
    public partial class administrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if( (Session["usuario"]==null || ((dominio.Usuario)Session["usuario"]).Administrador == false))
            {
                Response.Redirect("Default.aspx");
            }


            if (!IsPostBack)
            {
                //CargarCategorias();
                CargarProductos();
            }
        }

        //private void CargarCategorias()
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("select IDCATEGORIA, NOMBRE from CATEGORIAS");
        //        datos.ejecutarLectura();

        //        ddlCategoria.Items.Clear();

        //        while (datos.Lector.Read())
        //        {
        //            // Agregar las categorías al DropDownList
        //            ddlCategoria.Items.Add(new ListItem(datos.Lector["NOMBRE"].ToString(), datos.Lector["IDCATEGORIA"].ToString()));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        private void CargarProductos()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable dtProductos = new DataTable();


            dtProductos.Columns.Add("IDPRODUCTO");
            dtProductos.Columns.Add("NOMBRE");
            dtProductos.Columns.Add("PRECIO");
            dtProductos.Columns.Add("STOCK");
            dtProductos.Columns.Add("IDCATEGORIA");
            dtProductos.Columns.Add("CATEGORIA");
            dtProductos.Columns.Add("ESTADO");

            try
            {
                datos.setearConsulta("SELECT P.IDPRODUCTO, P.NOMBRE, P.PRECIO, P.STOCK, C.IDCATEGORIA, C.NOMBRE AS Categoria, P.ESTADO FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IDCATEGORIA = C.IDCATEGORIA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string estado = datos.Lector["ESTADO"].ToString().ToLower() == "true" ? "1" : "0";
                    dtProductos.Rows.Add(
                        datos.Lector["IDPRODUCTO"].ToString(),
                        datos.Lector["NOMBRE"].ToString(),
                        datos.Lector["PRECIO"].ToString(),
                        datos.Lector["STOCK"].ToString(),
                        datos.Lector["IDCATEGORIA"].ToString(),
                        datos.Lector["Categoria"].ToString(),
                        estado
                    );
                }

                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();
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

        //protected void btnAgregar_Click(object sender, EventArgs e)
        //{
        //    // Validar los campos antes de insertar
        //    if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecio.Text) ||
        //        string.IsNullOrEmpty(txtStock.Text) || string.IsNullOrEmpty(txtUrlImagen.Text) ||
        //        ddlCategoria.SelectedItem == null)
        //    {
        //        // Opcional: mostrar un mensaje de error al usuario si algún campo está vacío
        //        // Ejemplo: lblErrorMessage.Text = "Por favor, complete todos los campos.";
        //        return;
        //    }

        //    // Obtener los valores del formulario
        //    string nombre = txtNombre.Text;
        //    decimal precio;
        //    int stock;
        //    int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
        //    string urlImagen = txtUrlImagen.Text;
        //    int estado = chkEstado.Checked ? 1 : 0;

        //    // Validar que precio y stock tengan valores correctos
        //    if (!decimal.TryParse(txtPrecio.Text, out precio) || !int.TryParse(txtStock.Text, out stock))
        //    {
        //        // Mostrar mensaje de error de validación (opcional)
        //        // Ejemplo: lblErrorMessage.Text = "Precio y Stock deben ser valores numéricos.";
        //        return;
        //    }

        //    // Insertar el producto en la base de datos
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        // Insertar en la tabla PRODUCTOS
        //        datos.setearConsulta("INSERT INTO PRODUCTOS (IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION, ESTADO) " +
        //                             "VALUES (@idCategoria, @nombre, @precio, @stock, @descripcion, @estado)");
        //        datos.setearParametro("@idCategoria", idCategoria);
        //        datos.setearParametro("@nombre", nombre);
        //        datos.setearParametro("@precio", precio);
        //        datos.setearParametro("@stock", stock);
        //        datos.setearParametro("@descripcion", ""); // Si tienes campo de descripción, reemplaza o ajusta
        //        datos.setearParametro("@estado", estado);
        //        datos.ejecutarAccion();

        //        // Obtener el último ID de producto insertado
        //        datos.setearConsulta("SELECT SCOPE_IDENTITY()");
        //        int idProducto = Convert.ToInt32(datos.ejecutarEscalar());

        //        // Insertar en la tabla IMAGENES
        //        datos.setearConsulta("INSERT INTO IMAGENES (IDPRODUCTO, URLIMG) VALUES (@idProducto, @urlImagen)");
        //        datos.setearParametro("@idProducto", idProducto);
        //        datos.setearParametro("@urlImagen", urlImagen);
        //        datos.ejecutarAccion();

        //        // Recargar los productos para reflejar el nuevo producto en la grilla
        //        CargarProductos();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}


        protected void gvProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(gvProductos.DataKeys[e.RowIndex].Value);
                string nombre = (gvProductos.Rows[e.RowIndex].FindControl("txtNombre") as TextBox).Text;
                decimal precio = Convert.ToDecimal((gvProductos.Rows[e.RowIndex].FindControl("txtPrecio") as TextBox).Text);
                int stock = Convert.ToInt32((gvProductos.Rows[e.RowIndex].FindControl("txtStock") as TextBox).Text);

                // Obtener la categoría seleccionada del DropDownList
                DropDownList ddlCategoria = (gvProductos.Rows[e.RowIndex].FindControl("ddlCategoria") as DropDownList);
                int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);

                // Actualizar en la base de datos
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE PRODUCTOS SET NOMBRE = @nombre, PRECIO = @precio, STOCK = @stock, IDCATEGORIA = @idCategoria WHERE IDPRODUCTO = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@precio", precio);
                datos.setearParametro("@stock", stock);
                datos.setearParametro("@idCategoria", idCategoria);
                datos.setearParametro("@id", idProducto);
                datos.ejecutarAccion();

                gvProductos.EditIndex = -1;
                CargarProductos();
            }
            catch (Exception ex)
            {
                // Manejar el error
                Console.WriteLine(ex.Message);
            }
        }

        protected void gvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Establece el índice de la fila en modo de edición
            gvProductos.EditIndex = e.NewEditIndex;

            // Recarga los productos para que la fila seleccionada entre en modo de edición
            CargarProductos();
  
        }

        protected void gvProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProductos.EditIndex = -1; // Cancelar la edición y volver al modo de solo lectura
            CargarProductos(); // Recargar productos para quitar el modo de edición
        }

        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID del producto
            string idProducto = gvProductos.DataKeys[e.RowIndex].Values["IDPRODUCTO"].ToString();

            // Obtener el estado actual de forma segura
            int estadoActual = 0; // Valor predeterminado en caso de error
            if (gvProductos.DataKeys[e.RowIndex].Values["ESTADO"] != null &&
                int.TryParse(gvProductos.DataKeys[e.RowIndex].Values["ESTADO"].ToString(), out estadoActual))
            {
                // Cambiar el estado: si es 1 (activo), lo desactiva a 0; si es 0, lo activa a 1
                int nuevoEstado = estadoActual == 1 ? 0 : 1;

                // Llamar al método que cambia el estado del producto
                CambiarEstadoProducto(idProducto, nuevoEstado);

                // Recargar la lista de productos
                CargarProductos();
            }
            else
            {
                // Manejar el caso en que el valor de ESTADO no sea válido
                Console.WriteLine("Error: El estado actual no es un número válido.");
            }
        }

        private void CambiarEstadoProducto(string idProducto, int nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Configurar la consulta para actualizar el estado del producto
                datos.setearConsulta("UPDATE Productos SET ESTADO = @nuevoEstado WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@nuevoEstado", nuevoEstado);
                datos.setearParametro("@idProducto", idProducto);

                // Ejecutar la acción
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del producto: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
                Console.WriteLine($"ID: {idProducto}, Nuevo Estado: {nuevoEstado}");

            }
        }

        private void EliminarProducto(string idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Productos SET ESTADO = 0 WHERE IDPRODUCTO = @idProducto");
                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            //datos.setearConsulta("UPDATE Productos SET ESTADO = 0 WHERE IDPRODUCTO = @idProducto");
            //// Consulta para actualizar el estado del producto a 0 (eliminado)
            //string query = "UPDATE Productos SET ESTADO = 0 WHERE IDPRODUCTO = @idProducto";

            //// Usar la clase AccesoDatos para ejecutar la consulta
            ////using (SqlConnection conn = new SqlConnection(cadenaConexion))  // cadenaConexion es la cadena de conexión a la base de datos
            //AccesoDatos datos = new AccesoDatos();

            //{
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    cmd.Parameters.AddWithValue("@idProducto", idProducto);

            //    try
            //    {
            //        conn.Open();
            //        cmd.ExecuteNonQuery();  // Ejecutar la consulta de actualización
            //    }
            //    catch (Exception ex)
            //    {
            //        // Manejar cualquier error que ocurra durante la operación
            //        // Puedes mostrar un mensaje de error, registrar en un log, etc.
            //        Response.Write("Error al eliminar el producto: " + ex.Message);
            //    }
            //}
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar si la fila está en modo edición para cargar el DropDownList
                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                {
                    DropDownList ddlCategoria = (DropDownList)e.Row.FindControl("ddlCategoria");

                    AccesoDatos datos = new AccesoDatos();
                    datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE FROM CATEGORIAS");
                    datos.ejecutarLectura();

                    ddlCategoria.DataSource = datos.Lector;
                    ddlCategoria.DataTextField = "NOMBRE";
                    ddlCategoria.DataValueField = "IDCATEGORIA";
                    ddlCategoria.DataBind();

                    string idCategoriaActual = DataBinder.Eval(e.Row.DataItem, "IDCATEGORIA").ToString();
                    ddlCategoria.SelectedValue = idCategoriaActual;

                    datos.cerrarConexion();
                }

                // Verificar y convertir el estado de forma segura
                int estado = 0;
                if (int.TryParse(DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString(), out estado))
                {
                    // Recorrer los controles en la celda y buscar el LinkButton
                    foreach (Control control in e.Row.Cells[5].Controls)
                    {

                        if (control is LinkButton btnEliminar)
                        {
                            btnEliminar.Text = estado == 0 ? "Agregar" : "Eliminar";
                            btnEliminar.OnClientClick = estado == 0
                                ? "return confirm('¿Está seguro de que desea agregar este producto nuevamente a la venta?');"
                                : "return confirm('¿Está seguro de que desea eliminar este producto?');";
                            break;
                        }
                    }
                }
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleState")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    // Obtiene el estado actual del producto y lo invierte (1 a 0, o 0 a 1)
                    datos.setearConsulta("SELECT ESTADO FROM PRODUCTOS WHERE IDPRODUCTO = @idProducto");
                    datos.setearParametro("@idProducto", idProducto);
                    datos.ejecutarLectura();

                    int estadoActual = 0;
                    if (datos.Lector.Read())
                        estadoActual = Convert.ToInt32(datos.Lector["ESTADO"]);

                    int nuevoEstado = estadoActual == 1 ? 0 : 1;

                    // Cambia el estado en la base de datos
                    datos.cerrarConexion();
                    datos.setearConsulta("UPDATE PRODUCTOS SET ESTADO = @nuevoEstado WHERE IDPRODUCTO = @idProducto");
                    datos.setearParametro("@nuevoEstado", nuevoEstado);
                    datos.setearParametro("@idProducto", idProducto);
                    datos.ejecutarAccion();

                    // Recargar los productos después de cambiar el estado
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    // Manejar el error
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("agregarProducto.aspx");
        }

        protected void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("EliminarProducto.aspx");
        }

    }


}
