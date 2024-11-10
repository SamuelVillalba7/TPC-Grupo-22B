<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="administrar.aspx.cs" Inherits="TPC_Equipo_22B.administrar" UnobtrusiveValidationMode="None" %>

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
                                <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("PRECIO") %>'></asp:Label>
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


                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" DeleteText="Eliminar" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnToggleState" runat="server" Text='<%# Eval("ESTADO").ToString() == "1" ? "Eliminar" : "Agregar" %>'
                                    CommandName="ToggleState" CommandArgument='<%# Eval("IDPRODUCTO") %>'
                                    CssClass="btn btn-secondary"
                                    OnClientClick="return confirm('¿Está seguro de que desea cambiar el estado de este producto?');" />
                            </ItemTemplate>
                        </asp:TemplateField>


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
