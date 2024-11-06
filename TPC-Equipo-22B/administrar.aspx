<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="administrar.aspx.cs" Inherits="TPC_Equipo_22B.administrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Título de la página -->
        <div class="row">
            <div class="col-md-12 text-center">
                <h1 class="display-4">Administrar Productos</h1>
                <hr />
            </div>
        </div>

        <!-- Tabla de productos -->
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvProductos" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="False"
                    OnRowEditing="gvProductos_RowEditing"
                    OnRowUpdating="gvProductos_RowUpdating"
                    OnRowCancelingEdit="gvProductos_RowCancelingEdit"
                    OnRowDeleting="gvProductos_RowDeleting"
                    OnRowDataBound="gvProductos_RowDataBound"
                    DataKeyNames="IDPRODUCTO">
                    <Columns>

                        <asp:BoundField DataField="IDPRODUCTO" HeaderText="ID" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("PRECIO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrecio" runat="server" Text='<%# Bind("PRECIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Eval("STOCK") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStock" runat="server" Text='<%# Bind("STOCK") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Categoría">
                            <ItemTemplate>
                                <asp:Label ID="lblCategoria" runat="server" Text='<%# Eval("CATEGORIA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />

                        <%--<asp:BoundField DataField="IDPRODUCTO" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="PRECIO" HeaderText="Precio" />
                        <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                        <asp:BoundField DataField="CATEGORIA" HeaderText="Categoría" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Formulario de agregación de producto -->
        <div class="row mt-5">
            <div class="col-md-4 offset-md-4">
                <h3>Agregar Producto</h3>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" placeholder="Nombre del producto" />
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control mb-3" placeholder="Precio" />
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control mb-3" placeholder="Stock" />
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control mb-3">
                </asp:DropDownList>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Producto" CssClass="btn btn-primary btn-block" OnClick="btnAgregar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
