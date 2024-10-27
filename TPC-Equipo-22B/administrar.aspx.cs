using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using negocio;

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
            //// Simulación de carga de categorías desde la base de datos
            //ddlCategoria.Items.Add(new ListItem("Tecnología", "1"));
            //ddlCategoria.Items.Add(new ListItem("Accesorios", "2"));
            //ddlCategoria.Items.Add(new ListItem("Consolas", "3"));

            try
            {
                datos.setearConsulta("select IDCATEGORIA, NOMBRE from CATEGORIAS");  // Cambia 'Id' por el nombre correcto de la columna
                datos.ejecutarLectura();

                ddlCategoria.Items.Clear(); // Limpiar cualquier ítem existente en el DropDownList

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
            dtProductos.Columns.Add("CATEGORIA");

            //// Simulación de datos (en tu caso, debes reemplazar con datos de la base de datos)
            //dtProductos.Rows.Add("1", "Auriculares", "1500", "10", "Tecnología");
            //dtProductos.Rows.Add("2", "Teclado", "1200", "5", "Accesorios");

            //gvProductos.DataSource = dtProductos;
            //gvProductos.DataBind();

            try
            {
                datos.setearConsulta("select P.IDPRODUCTO, P.NOMBRE, P.PRECIO, P.STOCK, C.NOMBRE AS Categoria from PRODUCTOS P INNER JOIN CATEGORIAS C ON P.IDCATEGORIA = C.IDCATEGORIA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    dtProductos.Rows.Add(
                    datos.Lector["IDPRODUCTO"].ToString(), // IDPRODUCTO
                    datos.Lector["NOMBRE"].ToString(),     // NOMBRE
                    datos.Lector["PRECIO"].ToString(),     // PRECIO
                    datos.Lector["STOCK"].ToString(),      // STOCK
                    datos.Lector["Categoria"].ToString()   // CATEGORIA
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

            // Lógica de inserción en la base de datos (simulación aquí)
            // SqlCommand cmd = new SqlCommand("INSERT INTO Productos ...");

            // Luego de agregar el producto, recargas la lista de productos
            CargarProductos();
        }

        //protected void gvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    // Lógica para editar un producto
        //    gvProductos.EditIndex = e.NewEditIndex;
        //    CargarProductos(); // Recargar productos para permitir la edición

        //    // Obtener el ID del producto que se está editando
        //    int idProducto = Convert.ToInt32(gvProductos.DataKeys[e.RowIndex].Value);

        //    // Obtener los nuevos valores de las celdas editadas
        //    string nombre = (gvProductos.Rows[e.RowIndex].FindControl("txtNombre") as TextBox).Text;
        //    decimal precio = Convert.ToDecimal((gvProductos.Rows[e.RowIndex].FindControl("txtPrecio") as TextBox).Text);
        //    float stock = Convert.ToSingle((gvProductos.Rows[e.RowIndex].FindControl("txtStock") as TextBox).Text);

        //    // Aquí puedes implementar la lógica para actualizar el producto en la base de datos
        //    // Por ejemplo, llamando a un método en tu clase de acceso a datos (AccesoDatos)

        //    try
        //    {
        //        AccesoDatos datos = new AccesoDatos();
        //        datos.setearConsulta("UPDATE PRODUCTOS SET NOMBRE = @nombre, PRECIO = @precio, STOCK = @stock WHERE IDPRODUCTO = @id");
        //        datos.setearParametro("@nombre", nombre);
        //        datos.setearParametro("@precio", precio);
        //        datos.setearParametro("@stock", stock);
        //        datos.setearParametro("@id", idProducto);
        //        datos.ejecutarAccion();

        //        // Volver al modo normal y recargar productos
        //        gvProductos.EditIndex = -1;
        //        CargarProductos();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error
        //        throw ex; // Aquí puedes manejar el error de manera más amigable
        //    }
        //}

        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lógica para eliminar un producto
            string idProducto = gvProductos.DataKeys[e.RowIndex].Value.ToString();

            // Simulación de eliminación (deberías realizar una consulta DELETE en la base de datos)
            // SqlCommand cmd = new SqlCommand("DELETE FROM Productos WHERE IDPRODUCTO=@id", ...);

            // Luego de eliminar el producto, recargar la lista
            CargarProductos();
        }
    }
}
