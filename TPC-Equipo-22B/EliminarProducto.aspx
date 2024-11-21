<%@ Page Title="Eliminar Producto" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="TPC_Equipo_22B.EliminarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3 class="text-center">Eliminar Producto</h3>
        <p class="text-center">Seleccione un producto de la lista para eliminarlo permanentemente.</p>
        <hr />

        <!-- GridView para mostrar los productos -->
        <asp:GridView ID="gvProductos" runat="server" CssClass="table table-striped table-bordered text-center"
            AutoGenerateColumns="False" DataKeyNames="IDPRODUCTO" OnRowCommand="gvProductos_RowCommand">
            <Columns>
                <asp:BoundField DataField="IDPRODUCTO" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                            CommandName="Eliminar" CommandArgument='<%# Eval("IDPRODUCTO") %>'
                            OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto de forma permanente?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
