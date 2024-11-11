<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registrar.aspx.cs" Inherits="TPC_Equipo_22B.registrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-container">
        <div class="login-container">
            <div class="login">
                <h2 class="login__title">Sing in</h2>
                <p class="login__p">introduce los datos de tu cuenta</p>
                <div class="login__form">
                    <label for="txtNombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" CssClass="login__input" runat="server" ></asp:TextBox>
                    
                    <label for="txtApellido">Apellido</label>
                    <asp:TextBox ID="txtApellido" CssClass="login__input" runat="server"></asp:TextBox>
                    
                    <label for="txtTelefono">Telefono</label>
                    <asp:TextBox ID="txtTelefono" CssClass="login__input" runat="server"></asp:TextBox>
                    
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="login__input" runat="server"></asp:TextBox>
                    
                    <label for="txtContraseña">Contraseña</label>
                    <asp:TextBox ID="txtContraseña" CssClass="login__input" type="password" runat="server"></asp:TextBox>

                    <asp:Label ID="lblRegistrar" CssClass="lbl" runat="server" Text=""></asp:Label>
                
                    <asp:Button ID="btnRegistrarse" OnClick="btnRegistrarse_Click" CssClass="login__button" runat="server" Text="Registrar" />
                    
                </div>
                
            </div>
        </div>
    </div>



</asp:Content>
