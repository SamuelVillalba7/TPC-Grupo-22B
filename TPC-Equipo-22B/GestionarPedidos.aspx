<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GestionarPedidos.aspx.cs" Inherits="TPC_Equipo_22B.GestionarPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Título de la página -->
    <div class="row">
        <div class="col-md-12 text-center">
            <h1 class="display-4">Administrar Pedidos</h1>
            <hr />
        </div>
    </div>
    <!-- Tabla de pedidos -->
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                OnRowEditing="gvPedidos_RowEditing"
                OnRowCancelingEdit="gvPedidos_RowCancelingEdit"
                OnRowUpdating="gvPedidos_RowUpdating"
                OnRowDataBound="gvPedidos_RowDataBound"
                DataKeyNames="IdPedido">
                <Columns>
                    <asp:BoundField DataField="IdPedido" HeaderText="Pedido ID" ReadOnly="True" />
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" ReadOnly="True" />
                    <asp:BoundField DataField="MetodoNombre" HeaderText="Método de Pago" ReadOnly="True" />
                    <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" ReadOnly="True" />
                    <asp:BoundField DataField="FechaPedido" HeaderText="Fecha Pedido" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoNombre" runat="server" Text='<%# Eval("EstadoNombre") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEstado" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
