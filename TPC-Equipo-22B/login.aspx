<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TPC_Equipo_22B.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-container">
        <div class="login">
            <h2 class="login__title">Login</h2>
            <p class="login__p">introduce los datos de tu cuenta</p>
            <div class="login__form">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="login__input" runat="server"></asp:TextBox>
                
                <label for="">Contraseña</label>
                <asp:TextBox ID="txtContraseña" CssClass="login__input" type="password" runat="server"></asp:TextBox>
        

                <asp:Button ID="btnLogin" CssClass="login__button" OnClick="btnLogin_Click" runat="server" Text="Button" />
                
            </div>
            
        </div>
    </div>


</asp:Content>
