<%@ Page Title="Agregar Producto" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="agregarProducto.aspx.cs" Inherits="TPC_Equipo_22B.agregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Opcional: Agrega aquí CSS o JS específicos de la página -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3>Agregar Nuevo Producto</h3>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" placeholder="Nombre del producto" />
        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control mb-3" placeholder="Precio" />
        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control mb-3" placeholder="Stock" />
        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control mb-3">
        </asp:DropDownList>
        <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control mb-3" placeholder="URL de Imagen" />
        <asp:CheckBox ID="chkEstado" runat="server" Text="Activo" />
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Producto" CssClass="btn btn-primary btn-block" OnClick="btnAgregar_Click" />
    </div>
</asp:Content>
