<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="productoDescripcion.aspx.cs" Inherits="TPC_Equipo_22B.productoDescripcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/productoDescripcion.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="detalle-producto">
        <div class="detalle-producto__container-img">
            <img class="detalle-producto__img" src="https://coretms.tecnomegastore.ec/assets/images/main/20/MOULOG910005439.webp" alt="">
        </div>
        <div class="detalle-producto__container-text">
            <h2 class="detalle-producto__name">Beats</h2>
            <p class="detalle-producto__price">$995</p>
            <p class="detalle-producto__description">There are many variations passages of Lorem Ipsum available, but the majority have suffered alteration words some 
                form by injected or randomized which don’t even slightly believable. If you are going to use a passage of Lorem 
                Ipsum, you need to be sure there isn’t anything
            </p>
            <div class="detalle-producto__container-buttons">
                <p>numeros</p>
                <button class="detalle-producto__button">Add to cart</button>
            </div>
            <p class="detalle-producto__categoria">Category:Headphone</p>

        </div>
    </div>

    <div class="title-container">
        <h2 class="title">Related products</h2>
    </div>



</asp:Content>
