<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="carrito.aspx.cs" Inherits="TPC_Equipo_22B.carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/filtros.css" rel="stylesheet" />
    <link href="style/carrito.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <p class="section__p">Home > Carrito</p>
        <h2 class="section__title">Carrito</h2>
    </div>
    <div class="table-container">
        <table>
           <tr>
                <th></th>
                <th>Producto</th>
                <th>Precio</th>
                <th>Cantidad</th>
                <th>Subtotal</th>
               <th></th>
                
            </tr>
            <tr>
                <td></td>
                <td>Beats</td>
                <td>$995</td>
                <td>1</td>
                <td>$995</td>
                <td></td>
         
            </tr>
        </table>
    </div>


</asp:Content>
