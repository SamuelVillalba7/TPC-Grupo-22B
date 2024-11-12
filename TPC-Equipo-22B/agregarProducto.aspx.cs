using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_22B
{
    public partial class agregarProducto : System.Web.UI.Page
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

            try
            {
                datos.setearConsulta("SELECT IDCATEGORIA, NOMBRE FROM CATEGORIAS");
                datos.ejecutarLectura();

                ddlCategoria.Items.Clear();

                while (datos.Lector.Read())
                {
                    ddlCategoria.Items.Add(new ListItem(datos.Lector["NOMBRE"].ToString(), datos.Lector["IDCATEGORIA"].ToString()));
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecio.Text) ||
                string.IsNullOrEmpty(txtStock.Text) || string.IsNullOrEmpty(txtUrlImagen.Text) ||
                ddlCategoria.SelectedItem == null)
            {
                return;
            }

            string nombre = txtNombre.Text;
            decimal precio;
            int stock;
            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            string urlImagen = txtUrlImagen.Text;
            int estado = chkEstado.Checked ? 1 : 0;

            if (!decimal.TryParse(txtPrecio.Text, out precio) || !int.TryParse(txtStock.Text, out stock))
            {
                return;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO PRODUCTOS (IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION, ESTADO) " +
                                     "VALUES (@idCategoria, @nombre, @precio, @stock, @descripcion, @estado)");
                datos.setearParametro("@idCategoria", idCategoria);
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@precio", precio);
                datos.setearParametro("@stock", stock);
                datos.setearParametro("@descripcion", "");
                datos.setearParametro("@estado", estado);
                datos.ejecutarAccion();

                datos.setearConsulta("SELECT SCOPE_IDENTITY()");
                int idProducto = Convert.ToInt32(datos.ejecutarEscalar());

                datos.setearConsulta("INSERT INTO IMAGENES (IDPRODUCTO, URLIMG) VALUES (@idProducto, @urlImagen)");
                datos.setearParametro("@idProducto", idProducto);
                datos.setearParametro("@urlImagen", urlImagen);
                datos.ejecutarAccion();

                Response.Redirect("administrar.aspx");
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