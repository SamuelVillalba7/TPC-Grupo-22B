<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MisPedidos.aspx.cs" Inherits="TPC_Equipo_22B.MisPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Título de la página -->
    <div class="row">
        <div class="col-md-12 text-center">
            <h1 class="display-4">Mis Pedidos</h1>
            <hr />
        </div>
    </div>
    <!-- Tabla de pedidos -->
    <div class="row">
        <div class="col-md-12">
            <%--<asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" OnRowEditing="gvPedidos_RowEditing" OnRowCancelingEdit="gvPedidos_RowCancelingEdit" OnRowUpdating="gvPedidos_RowUpdating">--%>
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                OnRowCommand="gvPedidos_RowCommand">

                <Columns>

                    <asp:BoundField DataField="IdPedido" HeaderText="Pedido ID" ReadOnly="True" />


                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario ID" ReadOnly="True" />


                    <asp:BoundField DataField="MetodoNombre" HeaderText="Método de Pago ID" ReadOnly="True" />


                    <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" ReadOnly="True" />


                    <asp:BoundField DataField="FechaPedido" HeaderText="Fecha Pedido" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />

                    <asp:BoundField DataField="EstadoNombre" HeaderText="Estado" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                        CommandName="Cancelar" CommandArgument='<%# Eval("IdPedido") %>' 
                                        CssClass="btn btn-danger" OnClientClick="return confirm('¿Está seguro de cancelar este pedido?');" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
