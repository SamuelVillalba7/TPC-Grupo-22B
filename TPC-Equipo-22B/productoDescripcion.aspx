﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="productoDescripcion.aspx.cs" Inherits="TPC_Equipo_22B.productoDescripcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/producto.css" rel="stylesheet" />
    <link href="style/productoDescripcion.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="detalle-producto">
        <div class="detalle-producto__container-img">
            <img class="detalle-producto__img" src="<%:articulo.Imagen %>" alt="">
        </div>
        <div class="detalle-producto__container-text">
            <h2 class="detalle-producto__name"> <%:articulo.Nombre %></h2>
            <p class="detalle-producto__price">$<%:articulo.PrecioFormateado %></p>
            <p class="detalle-producto__description"><%:articulo.Descripcion %>
            </p>
            <div class="detalle-producto__container-buttons">
                <p>numeros</p>
                <asp:Button ID="Button1" Onclick="Button1_Click" CssClass="detalle-producto__button" runat="server" Text="Add to cart" />
               
            </div>
            <p class="detalle-producto__categoria">Category:<%:articulo.Categoria.Nombre%></p>

        </div>
    </div>

    <div class="title-container">
        <h2 class="title">Related products</h2>









    </div>



</asp:Content>
