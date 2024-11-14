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

        <asp:GridView ID="dgv_carrito" runat="server" class="table-bordered" AutoGenerateColumns="false">
         <%--   DataKeyNames="IdProducto" OnRowEditing="dgvcarrito_RowEditing"
            OnRowUpdating="dgvcarrito_RowUpdating" OnRowCancelingEdit="dgvcarrito_RowCancelingEdit">--%>

            <Columns>
                <asp:BoundField HeaderText="Articulo" DataField="nombreProducto" />

                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("cantidad") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="Cantidad" DataField="cantidad" />--%>
                <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />
                <%-- <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdProducto") %>' />--%>
            </Columns>
        </asp:GridView>

        <br />
        <a href="filtros.aspx" class="btn btn-primary">Seguir comprando</a>


    </div>


</asp:Content>
