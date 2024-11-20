<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FinalizarCompra.aspx.cs" Inherits="TPC_Equipo_22B.FinalizarCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .step-title {
            font-size: 1.5rem;
            font-weight: bold;
            margin-bottom: 15px;
        }

        .form-section {
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            margin-bottom: 20px;
            background-color: #f9f9f9;
        }

        .form-label {
            font-weight: bold;
        }

        .total-container {
            font-size: 1.5rem;
            font-weight: bold;
            color: #28a745;
        }

        .btn-finalizar {
            background-color: #007bff;
            color: white;
            font-size: 1.2rem;
            font-weight: bold;
        }

            .btn-finalizar:hover {
                background-color: #0056b3;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Finalizar Compra</h2>
        <hr />

        <!-- Revisión del Carrito -->
        <div class="form-section">
            <div class="step-title">1. Revisión del Carrito</div>
            <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
                <Columns>
                    <asp:BoundField DataField="Articulo" HeaderText="Producto" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Total" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
            <div class="total-container text-right">
                <asp:Label ID="lblTotal" runat="server" Text="Total: $0.00"></asp:Label>
            </div>
        </div>

        <!-- Selección de Método de Pago -->
        <div class="form-section">
            <div class="step-title">2. Método de Pago</div>
            <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un método" Value="" />
                <asp:ListItem Text="Transferencia Bancaria" Value="1" />
                <asp:ListItem Text="Tarjeta de Débito" Value="2" />
                <asp:ListItem Text="Tarjeta de Crédito" Value="3" />
            </asp:DropDownList>
        </div>

        <!-- Datos del Cliente -->
        <div class="form-section">
            <div class="step-title">3. Datos del Cliente</div>
            <div class="form-group">
                <label class="form-label">Nombre Completo</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese su nombre completo"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingrese su dirección"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese su correo electrónico"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese su número de teléfono"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Ciudad</label>
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ingrese su ciudad"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="form-label">Codigo Postal</label>
                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Ingrese su codigo postal"></asp:TextBox>
            </div>
             <div class="form-group">
                <label class="form-label" for="ddlProvincias">Provincias</label> <br>
                <asp:DropDownList ID="ddlProvincias" runat="server"  >
                </asp:DropDownList>

             </div>

        </div>

        <!-- Opciones de Entrega -->
        <div class="form-section">
            <div class="step-title">4. Opciones de Entrega</div>
            <asp:RadioButtonList ID="rblEntrega" runat="server" CssClass="form-check">
                <asp:ListItem Text="Retiro en tienda" Value="Retiro" />
                <asp:ListItem Text="Envío a domicilio" Value="Envio" />
            </asp:RadioButtonList>
            <asp:TextBox ID="txtDireccionEnvio" runat="server" CssClass="form-control mt-2" placeholder="Dirección de envío" Visible="false"></asp:TextBox>
        </div>

        <!-- Confirmación de Compra -->
        <div class="text-center mt-4">
            <asp:Button ID="btnConfirmarCompra" runat="server" CssClass="btn btn-finalizar btn-lg" Text="Confirmar Compra" OnClick="btnConfirmarCompra_Click" />
            <br />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
