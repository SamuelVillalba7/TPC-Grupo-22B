<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="productoDescripcion.aspx.cs" Inherits="TPC_Equipo_22B.productoDescripcion" %>
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
                   <%--<input type="number" class="detalle-producto__cantidad" name="quantity" value="1" title="Qty" size="4" min="1" max="" step="1" placeholder="" inputmode="numeric" autocomplete="off">--%>
                <asp:TextBox ID="txtCantidad" runat="server" type="number" min="1" max="<%:articulo.Stock %>"></asp:TextBox>
                <asp:Button ID="btnAgregarAlCarrito" Onclick="Button1_Click" CssClass="detalle-producto__button" runat="server" Text="Add to cart" />
               
            </div>
            <p class="detalle-producto__categoria">Category:<%:articulo.Categoria.Nombre%></p>

        </div>
    </div>

    <div class="title-container">
        <h2 class="title">Productos relacionados</h2>
    </div>
    <div class="product-container">
 <% foreach (dominio.Articulo art in articulosRelacionados)
         { %>

     <div class="product-box" onclick="window.location.href='productoDescripcion.aspx?productoId=<%: art.Id %>'">
         <div class="product">
             <div class="product__img-container">
                 <img class="product__img" src="<%: art.Imagen %>" alt="" />
             </div>
             <div class="product__text">
                 <h2 class="product__name"><%: art.Nombre %></h2>
                 <span class="product__price">$<%: art.PrecioFormateado %></span>
             </div>
         </div>
     </div>


     <% } %>    
 </div>



</asp:Content>
