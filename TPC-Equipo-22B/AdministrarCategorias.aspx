<%@ Page Title="Administrar Categorías" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarCategorias.aspx.cs" Inherits="TPC_Equipo_22B.AdministrarCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            margin-top: 50px;
        }

        .table {
            margin-top: 20px;
        }

        .form-section {
            padding: 20px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

            .form-section h4 {
                margin-bottom: 20px;
            }

            .form-section .form-group {
                margin-bottom: 15px;
            }

        .btn {
            font-size: 16px;
            font-weight: bold;
        }

        .btn-block {
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">




        <h3 class="text-center">Categorías visibles en el home</h3>

        <asp:GridView ID="gvCategoriasVisible" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" DataKeyNames="IDCATEGORIA" EnableViewState="True">
            <Columns>
                <asp:BoundField DataField="IDCATEGORIA" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" ReadOnly="True" />
                <asp:TemplateField HeaderText="Visible">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkVisible" runat="server" Checked='<%# Convert.ToBoolean(Eval("VISIBLE")) %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="mensaje"></asp:Label><br />

        <!-- Botones globales -->
        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" CssClass="btn btn-success" Enabled="false" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-danger" Enabled="false" />




        <h3 class="text-center">Administrar Categorías</h3>

        <!-- Tabla de categorías existentes -->
        <asp:GridView ID="gvCategorias" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" DataKeyNames="IDCATEGORIA" OnRowEditing="gvCategorias_RowEditing"
            OnRowCancelingEdit="gvCategorias_RowCancelingEdit" OnRowUpdating="gvCategorias_RowUpdating"
            OnRowDeleting="gvCategorias_RowDeleting"
            OnRowCommand="gvCategorias_RowCommand">
            <Columns>
                <asp:BoundField DataField="IDCATEGORIA" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ESTADO")) %>' Enabled="False" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkEstadoEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("ESTADO")) %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="URLIMAGEN" HeaderText="URL Imagen" />
                <asp:CommandField ShowEditButton="True" EditText="Editar" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" />

                <asp:TemplateField HeaderText="Orden">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%# Eval("ORDEN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:Button ID="btnSubir" runat="server" Text="Subir" CommandName="Subir" CommandArgument='<%# Eval("IDCATEGORIA") %>' CssClass="btn btn-primary btn-sm" />
                        <asp:Button ID="btnBajar" runat="server" Text="Bajar" CommandName="Bajar" CommandArgument='<%# Eval("IDCATEGORIA") %>' CssClass="btn btn-secondary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

        

        <!-- Formulario para agregar una nueva categoría -->
        <div class="form-section mt-5">
            <h4>Agregar Nueva Categoría</h4>
            <div class="form-group">
                <label for="txtNombreCategoria">Nombre de la Categoría:</label>
                <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control" placeholder="Nombre de la categoría" />
            </div>
            <div class="form-group">
                <label for="txtURLCategoria">URL de la Imagen:</label>
                <asp:TextBox ID="txtURLCategoria" runat="server" CssClass="form-control" placeholder="URL de la imagen de la categoría" />
            </div>
            <div class="form-group form-check">
                <asp:CheckBox ID="chkEstadoCategoria" runat="server" CssClass="form-check-input" Checked="True" />
                <label class="form-check-label" for="chkEstadoCategoria">Activo</label>
            </div>

            <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoría" CssClass="btn btn-primary btn-block" OnClick="btnAgregarCategoria_Click" />
        </div>
    </div>
</asp:Content>
