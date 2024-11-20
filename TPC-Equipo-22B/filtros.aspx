<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="filtros.aspx.cs" Inherits="TPC_Equipo_22B.filtros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/filtros.css" rel="stylesheet" />
    <link href="style/producto.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <p class="section__p">Home > Products</p>
        <h2 class="section__title">Products</h2>
    </div>

    <div class="filter-container">
        <div class="filter">
            <div class="filter__search-container">
                <asp:TextBox ID="filtroRapido" runat="server" AutoPostBack="true" OnTextChanged="filtroRapido_TextChanged" class="filter__search-input" placeholder="Busca aqui"></asp:TextBox>
                <button class="filter__search-button"></button>
            </div>

            <div>
            </div>

            <br />

            <div class="filter__category">
                <p class="filter__category-text">Filtrar productos</p>

                
                <!-- Filtro por Categoría -->
                <label for="ddlCategoria">Categoría:</label> <br />
                <asp:DropDownList CssClass="btn btn-secondary dropdown-toggle" ID="ddlCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FiltrarProductos">
                </asp:DropDownList>

                <br />

                <!-- Filtro por Marca -->
                <label for="ddlMarca">Marca:</label> <br />
                <asp:DropDownList CssClass="btn btn-secondary dropdown-toggle" ID="ddlMarca" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FiltrarProductos">
                </asp:DropDownList>

                
            </div>
        </div>
        <div class="product-filter">
            <div class="product-filter__info">
                <h2 class="product-filter__title">Shop</h2>
                <p class="product-filter__results"></p>
            </div>

            <div class="product-container">

                <% foreach (dominio.Articulo art in ListaArticulo)
                    { %>

                <div class="product-box" onclick="window.location.href='productoDescripcion.aspx?productoId=<%: art.Id %>'">
                    <div class="product">
                        <div class="product__img-container">
                            <img class="product__img" src="<%: art.Imagen %>" alt="" />
                        </div>
                        <div class="product__text">
                            <h2 class="product__name"><%: art.Nombre %></h2>
                            <span class="product__price">$<%: art.PrecioFormateado %></span>
                            <a href="productoDescripcion.aspx?productoId=<%: art.Id %>" class="btn2">Ver detalle</a>
                        </div>
                    </div>
                </div>
                <% } %>
            </div>

        </div>
    </div>

</asp:Content>
