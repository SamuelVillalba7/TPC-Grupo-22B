using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_Equipo_22B
{
    public partial class GestionarPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        // Cargar los pedidos desde la base de datos
        private void CargarPedidos()
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            List<Pedido> pedidos = pedidoNegocio.ListarPedidosConEstados();

            if (pedidos.Count > 0)
            {
                gvPedidos.Visible = true;
                lblNoPedidos.Visible = false;
                gvPedidos.DataSource = pedidos;
                gvPedidos.DataBind();
            }
            else
            {
                gvPedidos.Visible = false;
                lblNoPedidos.Visible = true;
            }
        }


        protected void gvPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPedidos.EditIndex = e.NewEditIndex;
            CargarPedidos();
        }

        protected void gvPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPedidos.EditIndex = -1;
            CargarPedidos();
        }

        protected void gvPedidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idPedido = Convert.ToInt32(gvPedidos.DataKeys[e.RowIndex].Value);

                GridViewRow row = gvPedidos.Rows[e.RowIndex];
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");
                int nuevoEstado = Convert.ToInt32(ddlEstado.SelectedValue);

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                pedidoNegocio.ActualizarEstadoPedido(idPedido, nuevoEstado);

                gvPedidos.EditIndex = -1;
                CargarPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado del pedido: {ex.Message}");
            }
        }

        protected void gvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                // Obtener el DropDownList en la fila en edición
                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    
                    Pedido pedido = (Pedido)e.Row.DataItem;

                    if (pedido != null)
                    {
                        
                        EstadoNegocio estadoNegocio = new EstadoNegocio();
                        List<Estados> estados = estadoNegocio.ListarEstados(pedido.IdEstado, pedido.Envio);

                        
                        ddlEstado.DataSource = estados;
                        ddlEstado.DataTextField = "Nombre";
                        ddlEstado.DataValueField = "IdEstado";
                        ddlEstado.DataBind();

                        
                        if (ddlEstado.Items.FindByValue(pedido.IdEstado.ToString()) != null)
                        {
                            ddlEstado.SelectedValue = pedido.IdEstado.ToString();
                        }
                    }
                }
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
                Response.Redirect("GestionarPedidos.aspx",false);
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


        //protected void gvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
        //    {
        //        DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEstado");

        //        if (ddlEstado != null)
        //        {
        //            EstadoNegocio estadoNegocio = new EstadoNegocio();
        //            List<Estados> estados = estadoNegocio.ListarEstados();

        //            ddlEstado.DataSource = estados;
        //            ddlEstado.DataTextField = "Nombre";
        //            ddlEstado.DataValueField = "IdEstado";
        //            ddlEstado.DataBind();

        //            Pedido pedido = (Pedido)e.Row.DataItem;
        //            if (pedido != null)
        //            {
        //                if (ddlEstado.Items.FindByValue(pedido.IdEstado.ToString()) != null)
        //                {
        //                    ddlEstado.SelectedValue = pedido.IdEstado.ToString();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
