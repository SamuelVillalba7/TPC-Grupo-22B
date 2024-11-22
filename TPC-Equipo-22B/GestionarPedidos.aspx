<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GestionarPedidos.aspx.cs" Inherits="TPC_Equipo_22B.GestionarPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilo del encabezado de la tabla */
        .grid-header {
            background-color: #343a40; /* Fondo oscuro */
            color: white; /* Texto blanco */
            text-align: center;
            font-weight: bold;
            padding: 10px;
        }

        /* Filas normales */
        .grid-row {
            text-align: center;
            background-color: white;
        }

            /* Filas al pasar el mouse */
            .grid-row:hover {
                background-color: #f8f9fa; /* Color claro */
            }

        /* Botones de acción */
        .btn-action {
            font-size: 0.875rem;
            padding: 5px 15px;
        }

        /* Título de la página */
        .titulo-pagina {
            color: #343a40;
            margin-bottom: 20px;
        }

        /* Mensaje si no hay pedidos */
        .no-pedidos {
            color: #6c757d;
            font-size: 1.25rem;
            margin-top: 20px;
        }

        /* Encabezados estáticos sin cambiar color */
        .table thead th {
            background-color: #343a40 !important;
            color: white !important;
            cursor: default;
        }

        /* Etiquetas de estado */
        .badge-info {
            background-color: #17a2b8;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Título de la página -->
    <div class="row">
        <div class="col-md-12 text-center">
            <h1 class="display-4 titulo-pagina">Administrar Pedidos</h1>
            <p class="lead">Gestiona el estado de los pedidos realizados en el sistema.</p>
            <hr />
        </div>
    </div>

    <!-- Tabla de pedidos -->
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                HeaderStyle-CssClass="grid-header"
                RowStyle-CssClass="grid-row"
                OnRowEditing="gvPedidos_RowEditing"
                OnRowCancelingEdit="gvPedidos_RowCancelingEdit"
                OnRowUpdating="gvPedidos_RowUpdating"
                OnRowDataBound="gvPedidos_RowDataBound"
                DataKeyNames="IdPedido"
                OnRowCommand="gvPedidos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdPedido" HeaderText="Pedido ID" ReadOnly="True" />
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" ReadOnly="True" />
                    <asp:BoundField DataField="MetodoNombre" HeaderText="Método de Pago" ReadOnly="True" />
                    <asp:BoundField DataField="MontoTotal" HeaderText="Monto Total" DataFormatString="{0:C}" ReadOnly="True" />
                    <asp:BoundField DataField="FechaPedido" HeaderText="Fecha Pedido" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoNombre" runat="server" CssClass="badge badge-info"
                                Text='<%# Eval("EstadoNombre") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                CommandName="Cancelar" CommandArgument='<%# Eval("IdPedido") %>'
                                CssClass="btn btn-danger" OnClientClick="return confirm('¿Está seguro de cancelar este pedido?');" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <!-- Mensaje si no hay pedidos -->
            <asp:Label ID="lblNoPedidos" runat="server" CssClass="no-pedidos" Text="No hay pedidos disponibles." Visible="False"></asp:Label>
        </div>
    </div>
</asp:Content>
