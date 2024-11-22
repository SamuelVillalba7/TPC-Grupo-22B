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
                lblError.Visible = true;
                return;
            }


            if(int.Parse(rblEntrega.SelectedValue) == 1)
            {

                if (string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text) ||
                    string.IsNullOrEmpty(txtDireccion.Text) )
                {
                    lblError.Text = "Por favor, complete todos los campos requeridos.";
                    lblError.Visible = true;
                    return;
                }
            }



            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                DatosEnvioNegocio datosEnvioNegocio = new DatosEnvioNegocio();
                List<ItemCarrito> prodcarrito = Session["carrito"] as List<ItemCarrito>;

               
                if (prodcarrito == null || prodcarrito.Count == 0)
                {
                    lblError.Text = "El carrito está vacío.";
                    lblError.Visible = true;
                    return;
                }

                
                montoTotal = CalcularMontoTotal();
                int IdPedido = negocio.RegistrarPedido(
                    usuario.Id,
                    int.Parse(ddlMetodoPago.SelectedValue),
                    1, 
                    DateTime.Now,
                    int.Parse(rblEntrega.SelectedValue),
                    montoTotal
                );

                // Registrar datos de envío si es necesario
                if (int.Parse(rblEntrega.SelectedValue) == 1) 
                {
                    datosEnvioNegocio.agregar(
                        IdPedido,
                        int.Parse(ddlProvincias.SelectedValue),
                        txtCiudad.Text,
                        txtCodigoPostal.Text,
                        txtDireccion.Text
                    );
                }

                
                negocio.GuardarDetallePedido(IdPedido, prodcarrito);
                negocio.ActualizarStock(prodcarrito);

                
                string detallePedido = GenerarDetallePedido(prodcarrito, IdPedido, montoTotal);
                string asunto = "Confirmación de Pedido - Pedido #" + IdPedido;

                
                EnvioMail envioMail = new EnvioMail();
                envioMail.Enviar(txtEmail.Text, asunto, detallePedido);

                
                Session["carrito"] = null; 
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al procesar su compra. Por favor, intente nuevamente.";
                lblError.Visible = true;
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string GenerarDetallePedido(List<ItemCarrito> carrito, int idPedido, decimal montoTotal)
        {
            string detalle = $"<h2>Confirmación de Pedido - Pedido #{idPedido}</h2>";
            detalle += "<p>Gracias por tu compra. Aquí tienes el detalle de tu pedido:</p>";
            detalle += "<table style='width:100%; border-collapse:collapse; border: 1px solid black;'>";
            detalle += "<thead><tr><th style='border: 1px solid black;'>Producto</th><th style='border: 1px solid black;'>Cantidad</th><th style='border: 1px solid black;'>Precio Unitario</th><th style='border: 1px solid black;'>Subtotal</th></tr></thead>";
            detalle += "<tbody>";

            foreach (var item in carrito)
            {
                detalle += $"<tr>" +
                           $"<td style='border: 1px solid black;'>{item.art.Nombre}</td>" +
                           $"<td style='border: 1px solid black;'>{item.cantidad}</td>" +
                           $"<td style='border: 1px solid black;'>${item.art.Precio:N2}</td>" +
                           $"<td style='border: 1px solid black;'>${item.Subtotal:N2}</td>" +
                           $"</tr>";
            }

            detalle += "</tbody></table>";
            detalle += $"<h3>Total: ${montoTotal:N2}</h3>";
            detalle += "<p>Esperamos que disfrutes tu compra. ¡Gracias por elegirnos!</p>";

            return detalle;
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
            List<ItemCarrito> prodcarrito = new List<ItemCarrito>();
            prodcarrito = Session["carrito"] as List<ItemCarrito>;

            foreach (ItemCarrito item in prodcarrito)
            {
                total += item.Subtotal;
            }
            return total;
        }

        private decimal CalcularMontoTotal(DataTable carrito)
        {
            decimal total = 0;

            if (carrito == null || carrito.Rows.Count == 0)
            {
                return total; 
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

        protected void rblEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormDatosEnvio.Visible = rblEntrega.SelectedValue == "1";
        }
        
    }
}