<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="carrito.aspx.cs" Inherits="TPC_Equipo_22B.carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/filtros.css" rel="stylesheet" />
    <link href="style/carrito.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <p class="section__p">Home > Carrito</p>
        <h2 class="section__title">Carrito</h2>
    </div>
    <div class="table-container">

        <!-- Label para mensajes -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <div class="table-container">
            <asp:GridView ID="dgv_carrito" runat="server" class="table-bordered" AutoGenerateColumns="false" EnableViewState="True" DataKeyNames="IdProducto">

                <Columns>
                    <asp:BoundField HeaderText="Articulo" DataField="nombreProducto" />

                    <asp:TemplateField HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad") %>' OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />

                    <asp:TemplateField HeaderText="Eliminar del carrito">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="X" CommandName="EliminarProducto" CommandArgument='<%# Eval("IdProducto") %>' OnCommand="btnEliminar_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <br />
            <a href="filtros.aspx" class="btn btn-primary">Seguir comprando</a>
            <div class="mt-4">
                <asp:Button ID="btnFinalizarCompra" runat="server" CssClass="btn btn-primary btn-block"
                    Text="Proceder a Finalizar Compra" OnClick="btnFinalizarCompra_Click" />
             
            </div>


        </div>
</asp:Content>
