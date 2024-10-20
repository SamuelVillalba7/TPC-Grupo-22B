<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="filtros.aspx.cs" Inherits="TPC_Equipo_22B.filtros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/filtros.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">
        <p class="section__p">Home > Products</p>
        <h2 class="section__title">Products</h2>
    </div>



    <div class="filter-container">
        <div class="filter">
            <div class="filter__search-container">
                <input type="text" class="filter__search-input" placeholder="Search Here">
                <button class="filter__search-button"></button>
            </div>
            <div class="filter__category">
                <p class="filter__category-text">Product categories</p>
            </div>
        </div>
        <div class="product-filter">
            <div class="product-filter__info">
                <h2 class="product-filter__title">Shop</h2>
                <p class="product-filter__results">Showing 1-9 of 10 results</p>
            </div>
            <div class="product-filter__list">
                <!-- cargar los productos -->
            </div>
        </div>
    </div>



</asp:Content>
