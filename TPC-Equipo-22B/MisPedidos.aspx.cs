using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_Equipo_22B
{
    public partial class MisPedidos : System.Web.UI.Page
    {
        Usuario usuario = new Usuario();
        private void CargarPedidos()
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            List<Pedido> pedidos = pedidoNegocio.ListarPedidosPorUsuario(usuario.Id);
            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario = (Usuario)Session["usuario"];

                if (usuario == null)
                {
                    Response.Redirect("Default.aspx"); // Redirige al inicio si no hay usuario en la sesión
                    return;
                }

                CargarPedidos();
            }
        }


        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancelar")
            {
                // Obtener el ID del pedido a partir del CommandArgument
                int idPedido = Convert.ToInt32(e.CommandArgument);

                CancelarPedido(idPedido);

                // Recargar los pedidos para reflejar los cambios
                CargarPedidos();
                Response.Redirect("MisPedidos.aspx");
            }
        }

        protected void CancelarPedido(int IdPedido)
        {
            //update PEDIDOS set IDESTADO = 6 where IDPEDIDO = @IdPedido

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update PEDIDOS set IDESTADO = 6 where IDPEDIDO = @IdPedido");
                datos.setearParametro("@IdPedido", IdPedido);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }


        }
    }
}