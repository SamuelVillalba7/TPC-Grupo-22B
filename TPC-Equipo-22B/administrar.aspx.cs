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
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
            }
        }

        private void CargarCategorias()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select IDCATEGORIA, NOMBRE from CATEGORIAS");
                datos.ejecutarLectura();

                ddlCategoria.Items.Clear();

                while (datos.Lector.Read())
                {
                    // Agregar las categorías al DropDownList
                    ddlCategoria.Items.Add(new ListItem(datos.Lector["NOMBRE"].ToString(), datos.Lector["IDCATEGORIA"].ToString()));
                }
            }
            catch (Exception ex)
            {
                // Manejar el error
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void CargarProductos()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable dtProductos = new DataTable();

            dtProductos.Columns.Add("IDPRODUCTO");
            dtProductos.Columns.Add("NOMBRE");
            dtProductos.Columns.Add("PRECIO");
            dtProductos.Columns.Add("STOCK");
            dtProductos.Columns.Add("IDCATEGORIA");  // ID de la categoría
            dtProductos.Columns.Add("CATEGORIA");    // Nombre de la categoría

            try
            {
                datos.setearConsulta("SELECT P.IDPRODUCTO, P.NOMBRE, P.PRECIO, P.STOCK, C.IDCATEGORIA, C.NOMBRE AS Categoria FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IDCATEGORIA = C.IDCATEGORIA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    dtProductos.Rows.Add(
                        datos.Lector["IDPRODUCTO"].ToString(),
                        datos.Lector["NOMBRE"].ToString(),
                        datos.Lector["PRECIO"].ToString(),
                        datos.Lector["STOCK"].ToString(),
                        datos.Lector["IDCATEGORIA"].ToString(),   // IDCATEGORIA
                        datos.Lector["Categoria"].ToString()      // CATEGORIA (Nombre)
                    );
                }

                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar el error
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Aquí debes agregar la lógica para insertar el nuevo producto en la base de datos
            string nombre = txtNombre.Text;
            decimal precio = Convert.ToDecimal(txtPrecio.Text);
            int stock = Convert.ToInt32(txtStock.Text);
            string categoria = ddlCategoria.SelectedItem.Text;
            CargarProductos();
        }

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
            // Obtener el ID del producto que se va a "eliminar"
            string idProducto = gvProductos.DataKeys[e.RowIndex].Value.ToString();

            // Llamar a un método que cambie el estado del producto a 0
            EliminarProducto(idProducto);

            // Luego de "eliminar" el producto, recargar la lista de productos
            CargarProductos();
        }

        private void EliminarProducto(string idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Productos SET ESTADO = 0 WHERE IDPRODUCTO = @idProducto");
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                // Encontrar el DropDownList en la fila en modo edición
                DropDownList ddlCategoria = (DropDownList)e.Row.FindControl("ddlCategoria");

                // Cargar categorías en el DropDownList desde la base de datos
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE FROM CATEGORIAS");
                datos.ejecutarLectura();

                ddlCategoria.DataSource = datos.Lector;
                ddlCategoria.DataTextField = "NOMBRE";
                ddlCategoria.DataValueField = "IDCATEGORIA";
                ddlCategoria.DataBind();

                // Establecer la categoría actual del producto como seleccionada
                string idCategoriaActual = DataBinder.Eval(e.Row.DataItem, "IDCATEGORIA").ToString();
                ddlCategoria.SelectedValue = idCategoriaActual;

                datos.cerrarConexion();
            }
        }


    }

}
