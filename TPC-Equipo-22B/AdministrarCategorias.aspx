<%@ Page Title="Administrar Categorías" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarCategorias.aspx.cs" Inherits="TPC_Equipo_22B.AdministrarCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3>Administrar Categorías</h3>

        <!-- Tabla de categorías existentes -->
        <asp:GridView ID="gvCategorias" runat="server" CssClass="table table-striped table-bordered"
                      AutoGenerateColumns="False" DataKeyNames="IDCATEGORIA" OnRowDeleting="gvCategorias_RowDeleting">
            <Columns>
                <asp:BoundField DataField="IDCATEGORIA" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" />
            </Columns>
        </asp:GridView>

        <!-- Formulario para agregar una nueva categoría -->
        <h4 class="mt-4">Agregar Nueva Categoría</h4>
        <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control mb-3" placeholder="Nombre de la categoría" />
        <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoría" CssClass="btn btn-primary btn-block" OnClick="btnAgregarCategoria_Click" />
    </div>
</asp:Content>
