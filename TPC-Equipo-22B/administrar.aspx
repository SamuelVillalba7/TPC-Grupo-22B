<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="administrar.aspx.cs" Inherits="TPC_Equipo_22B.administrar" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Título de la página -->
        <div class="row mb-5">
            <div class="col-md-12 text-center">
                <h1 class="display-4">Panel de Administración</h1>
                <hr />
            </div>
        </div>


        <!-- Tabla de productos -->
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvProductos" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="IDPRODUCTO,ESTADO"
                    OnRowEditing="gvProductos_RowEditing"
                    OnRowUpdating="gvProductos_RowUpdating"
                    OnRowCancelingEdit="gvProductos_RowCancelingEdit"
                    OnRowDeleting="gvProductos_RowDeleting"
                    OnRowDataBound="gvProductos_RowDataBound"
                    OnRowCommand="gvProductos_RowCommand">
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
                                <asp:Label ID="lblPrecio" runat="server"
                                    Text='<%# "$ " + Convert.ToDecimal(Eval("PRECIO")).ToString("N2").Replace(",", "#").Replace(".", ",").Replace("#", ".") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrecio" runat="server" Text='<%# Bind("PRECIO") %>' CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Precio es obligatorio" ForeColor="Red" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Ingrese un número válido" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Eval("STOCK") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStock" runat="server" Text='<%# Bind("STOCK") %>' CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvStock" runat="server" ControlToValidate="txtStock" ErrorMessage="Stock es obligatorio" ForeColor="Red" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revStock" runat="server" ControlToValidate="txtStock" ErrorMessage="Ingrese un número entero" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$" />
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

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO").ToString() == "1" ? "Activo" : "Inactivo" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" DeleteText="Eliminar/Agregar" />

                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Botón para agregar un nuevo producto -->
        <div class="row mb-4 text-center">
            <div class="col-md-3">
                <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Nuevo Producto" CssClass="btn btn-primary btn-lg btn-block"
                    OnClick="btnAgregarProducto_Click" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnEliminarProducto" runat="server" Text="Eliminar Producto" CssClass="btn btn-secondary btn-lg btn-block"
                    OnClick="btnEliminarProducto_Click" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnAdministrarCategorias" runat="server" Text="Administrar Categorías" CssClass="btn btn-info btn-lg btn-block"
                    PostBackUrl="~/AdministrarCategorias.aspx" />
            </div>

            <div class="col-md-3">
                <asp:Button ID="btnAdministrarMarcas" runat="server" Text="Administrar Marcas" CssClass="btn btn-success btn-lg btn-block"
                    OnClick="btnAdministrarMarcas_Click" />
            </div>
        </div>
    </div>
</asp:Content>
