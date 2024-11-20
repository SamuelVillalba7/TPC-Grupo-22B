using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using negocio;
using System.Data.SqlClient;
using dominio;

namespace TPC_Equipo_22B
{
    public partial class FinalizarCompra : System.Web.UI.Page
    {

        public List<Provincia> provincias;
        public Usuario usuario;
        public Decimal montoTotal;
        protected void Page_Load(object sender, EventArgs e)
        {

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            
            usuario = (dominio.Usuario)Session["usuario"];

            txtEmail.Text = usuario.Email;
            txtNombre.Text= usuario.Nombre+" "+ usuario.Apellido;
            txtTelefono.Text= usuario.Telefono;
          


            if (!IsPostBack)
            {
                CargarCarrito();
                ProvinciaNegocio negocio = new ProvinciaNegocio();
                provincias = negocio.ListarProvincias();
                ddlProvincias.DataSource = provincias;
                ddlProvincias.DataTextField = "NombreProv";
                ddlProvincias.DataValueField = "IdProvincia";
                ddlProvincias.DataBind();


                


            }
        }

        private void CargarCarrito()
        {
            var carrito = Session["carrito"] as List<ItemCarrito>;

            if (carrito == null || carrito.Count == 0)
            {
                Response.Redirect("carrito.aspx");
                return;
            }

            DataTable carritoTable = new DataTable();
            carritoTable.Columns.Add("Articulo", typeof(string));
            carritoTable.Columns.Add("Cantidad", typeof(int));
            carritoTable.Columns.Add("Precio", typeof(decimal));
            carritoTable.Columns.Add("Subtotal", typeof(decimal));

            foreach (var item in carrito)
            {
                carritoTable.Rows.Add(
                    item.art.Nombre,
                    item.cantidad,
                    item.art.Precio,
                    item.art.Precio * item.cantidad
                );
            }

            gvCarrito.DataSource = carritoTable;
            gvCarrito.DataBind();

            montoTotal = carrito.Sum(item => item.art.Precio * item.cantidad);
            lblTotal.Text = $"Total: ${montoTotal:N2}";
        }


        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMetodoPago.SelectedValue) || string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblError.Text = "Por favor, complete todos los campos requeridos.";
                return;
            }

            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                //Falta pasar el ID del usuario que está logueado, que pida la ciudad y el CP en caso de querar entrega a domicilio, hacer un condiconal para ver que estado toma de acuerdo al metodo de pago seleccionado, pasar el monto
                negocio.RegistrarPedido(usuario.Id,int.Parse(ddlProvincias.SelectedValue), int.Parse(ddlMetodoPago.SelectedValue), txtCiudad.Text , txtCodigoPostal.Text , txtDireccion.Text, 1, DateTime.Now ,montoTotal );
                ActualizarStock();

                Session["carrito"] = null; // Limpiar el carrito después de confirmar la compra

                Response.Redirect("Confirmacion.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al procesar su compra. Por favor, intente nuevamente.";
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private int CrearPedido()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Pedidos (IDUsuario, FechaPedido, MontoTotal, MetodoPago, Envio, Estado) " +
                                     "VALUES (@IDUsuario, @FechaPedido, @MontoTotal, @MetodoPago, @Envio, @Estado)");
                datos.setearParametro("@IDUsuario", Session["UsuarioID"] ?? DBNull.Value);
                datos.setearParametro("@FechaPedido", DateTime.Now);
                datos.setearParametro("@MontoTotal", CalcularMontoTotal());
                datos.setearParametro("@MetodoPago", ddlMetodoPago.SelectedValue);
                datos.setearParametro("@Envio", rblEntrega.SelectedValue == "Envio" ? "Envío" : "Retiro");
                datos.setearParametro("@Estado", "Pedido en Preparación");

                datos.ejecutarAccion();

                // Obtener el ID del pedido recién creado
                datos.setearConsulta("SELECT SCOPE_IDENTITY()");
                return Convert.ToInt32(datos.ejecutarEscalar());
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private decimal CalcularMontoTotal()
        {
            decimal total = 0;

            // Verificar si el carrito no es null
            var carrito = Session["carrito"] as DataTable;
            if (carrito == null || carrito.Rows.Count == 0)
            {
                return total; // Si no hay productos, el total es 0
            }

            foreach (DataRow row in carrito.Rows)
            {
                total += Convert.ToDecimal(row["Cantidad"]) * Convert.ToDecimal(row["Precio"]);
            }
            return total;
        }

        private decimal CalcularMontoTotal(DataTable carrito)
        {
            decimal total = 0;

            if (carrito == null || carrito.Rows.Count == 0)
            {
                return total; // Si no hay productos, el total es 0
            }

            foreach (DataRow row in carrito.Rows)
            {
                total += Convert.ToDecimal(row["Cantidad"]) * Convert.ToDecimal(row["Precio"]);
            }
            return total;
        }

        private void GuardarDetallePedido(int idPedido)
        {
            var carrito = Session["carrito"] as DataTable;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (DataRow row in carrito.Rows)
                {
                    datos.setearConsulta("INSERT INTO DetallePedidos (IDPedido, IDProducto, Cantidad, Precio) " +
                                         "VALUES (@IDPedido, @IDProducto, @Cantidad, @Precio)");
                    datos.setearParametro("@IDPedido", idPedido);
                    datos.setearParametro("@IDProducto", row["IDProducto"]);
                    datos.setearParametro("@Cantidad", row["Cantidad"]);
                    datos.setearParametro("@Precio", row["Precio"]);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void ActualizarStock()
        {
            var carrito = Session["carrito"] as DataTable;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (DataRow row in carrito.Rows)
                {
                    datos.setearConsulta("UPDATE Productos SET Stock = Stock - @Cantidad WHERE IDProducto = @IDProducto");
                    datos.setearParametro("@Cantidad", row["Cantidad"]);
                    datos.setearParametro("@IDProducto", row["IDProducto"]);
                    datos.ejecutarAccion();
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}