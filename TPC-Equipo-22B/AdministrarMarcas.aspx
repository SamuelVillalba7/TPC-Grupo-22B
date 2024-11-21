<%@ Page Title="Administrar Marcas" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdministrarMarcas.aspx.cs" Inherits="TPC_Equipo_22B.AdministrarMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3>Administrar Marcas</h3>

        <!-- Tabla de marcas existentes -->
        <asp:GridView ID="gvMarcas" runat="server" CssClass="table table-striped table-bordered"
                      AutoGenerateColumns="False" DataKeyNames="IDMARCA" OnRowEditing="gvMarcas_RowEditing"
                      OnRowCancelingEdit="gvMarcas_RowCancelingEdit" OnRowUpdating="gvMarcas_RowUpdating"
                      OnRowDeleting="gvMarcas_RowDeleting">
            <Columns>
                <asp:BoundField DataField="IDMARCA" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ESTADO")) %>' Enabled="False" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkEstadoEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("ESTADO")) %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" EditText="Editar" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" />
            </Columns>
        </asp:GridView>

        <!-- Formulario para agregar una nueva marca -->
        <div class="mt-4 p-4 bg-light rounded">
            <h4>Agregar Nueva Marca</h4>
            <div class="form-group">
                <label for="txtNombreMarca">Nombre de la Marca:</label>
                <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control mb-3" placeholder="Nombre de la marca" />
            </div>
            <div class="form-group form-check">
                <asp:CheckBox ID="chkEstadoMarca" runat="server" CssClass="form-check-input" Checked="True" />
                <label class="form-check-label" for="chkEstadoMarca">Activo</label>
            </div>
            <asp:Button ID="btnAgregarMarca" runat="server" Text="Agregar Marca" CssClass="btn btn-primary btn-block" OnClick="btnAgregarMarca_Click" />
        </div>
    </div>
</asp:Content>
