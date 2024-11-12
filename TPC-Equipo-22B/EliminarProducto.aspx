<%@ Page Title="Eliminar Producto" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="TPC_Equipo_22B.EliminarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3>Eliminar Producto</h3>
        
        <!-- DropDownList para seleccionar el producto a eliminar -->
        <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control mb-3"></asp:DropDownList>
        
        <!-- Botón para eliminar el producto seleccionado -->
       <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Producto" CssClass="btn btn-danger btn-block" 
            OnClick="btnEliminar_Click" 
            OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto de forma permanente?');" />
    </div>
</asp:Content>
