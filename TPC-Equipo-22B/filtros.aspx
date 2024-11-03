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
                <asp:TextBox ID="filtroRapido" runat="server" AutoPostBack="true" OnTextChanged="filtroRapido_TextChanged" class="filter__search-input" placeholder="Search Here"></asp:TextBox>
                <button class="filter__search-button"></button>
            </div>

            <div>
            </div>

            <div class="filter__category">
                <p class="filter__category-text">Product categories</p>

                <asp:CheckBoxList ID="CblCategorias" runat="server" AutoPostBack="true"></asp:CheckBoxList>

                <asp:Button ID="btnFiltrar" CssClass="btn-primary" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />

<%--                <%foreach (dominio.Categoria cat in ListaCategoria)
                    { %>
                <div class="form-check">
                    <asp:CheckBox ID="CheckBox1" runat="server" class="form-check-input" OnCheckedChanged="chkCategorias_ChekedChanged" AutoPostBack="true" />
                    <label class="form-check-label" for="flexCheckDefault">
                        <%: cat.Nombre %>
                    </label>
                    
                </div>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

                <% cat.filtro = CheckBox1.Checked;
                        categoria = cat;
                    } %>--%>

                

            </div>
        </div>
        <div class="product-filter">
            <div class="product-filter__info">
                <h2 class="product-filter__title">Shop</h2>
                <p class="product-filter__results">Showing 1-9 of 10 results</p>
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
                        </div>
                    </div>
                </div>


                <% } %>
            </div>

        </div>
    </div>



</asp:Content>
