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
    public partial class GestionarPedidos : System.Web.UI.Page
    {
        List<Pedido> pedidos = new List<Pedido>();


        // Cargar los pedidos desde la base de datos
        private void CargarPedidos()
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            List<Pedido> pedidos = pedidoNegocio.ListarPedidos();
            gvPedidos.DataSource = pedidos;
            gvPedidos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar pedidos (simulado)
                CargarPedidos();
            }
        }


        protected void gvPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Poner el GridView en modo edición
            gvPedidos.EditIndex = e.NewEditIndex;
            CargarPedidos();

        }

        protected void gvPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancelar la edición
            gvPedidos.EditIndex = -1;
            CargarPedidos();
        }

        protected void gvPedidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtener el ID del pedido que se está actualizando
            int idPedido = Convert.ToInt32(gvPedidos.DataKeys[e.RowIndex].Value);

            // Obtener la fila que está en edición
            GridViewRow row = gvPedidos.Rows[e.RowIndex];

            // Obtener el valor actualizado del DropDownList
            DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");
            int nuevoEstado = Convert.ToInt32(ddlEstado.SelectedValue);

            // Actualizar el estado en tu base de datos
            // Aquí llamarías a tu capa de negocio para actualizar el pedido en la base de datos

            // Simulación de actualización en la lista local
            Pedido pedido = pedidos.Find(p => p.IdPedido == idPedido);
            if (pedido != null)
            {
                pedido.IdEstado = nuevoEstado;
            }

            // Terminar edición
            gvPedidos.EditIndex = -1;
            CargarPedidos();
        }
    }

}