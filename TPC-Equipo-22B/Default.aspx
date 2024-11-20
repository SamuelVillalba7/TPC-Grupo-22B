<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo_22B.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid cont">
        <div class="row">
            <div class="col-12 categoria categoria-principal">
                <div class="categoria__caja categoria__caja--principal">
                    <p class="categoria__p categoria__p--principal"><%:ListaCategoria[0].Nombre %></p>
                    <a class="categoria__boton categoria__boton--principal" href="#">ver categorias</a>
                    <img class="categoria__img categoria__img--principal" src="<%:ListaCategoria[0].UrlImagen %>" alt="imagen de <%:ListaCategoria[0].Nombre %>"/>
                </div>
            </div>
        </div>
        <div class="row fila">
            <div class="col-3 categoria categoria-uno">
                <div class="categoria__caja categoria__caja--uno">
                    <p class="categoria__p "><%:ListaCategoria[1].Nombre %></p>
                    <a class="categoria__boton categoria__boton--uno" href="#">browse</a>
                    <img class="categoria__img " src="<%:ListaCategoria[1].UrlImagen %>" alt="imagen de  <%:ListaCategoria[1].Nombre %>"/>

                </div>

               

            </div>
            <div class="col-3 categoria categoria-dos">
                <div class="categoria__caja categoria__caja--dos">
                    <p class="categoria__p "><%:ListaCategoria[2].Nombre %></p>
                    <a class="categoria__boton categoria__boton--dos" href="#">browse</a>
                    <img class="categoria__img " src="<%:ListaCategoria[2].UrlImagen %>" alt="imagen de  <%:ListaCategoria[2].Nombre %>"/>
                </div>



            </div>
            <div class="col-6 categoria categoria-tres">
                <div class="categoria__caja categoria__caja--tres">
                    <p class="categoria__p "><%:ListaCategoria[3].Nombre %></p>
                    <a class="categoria__boton categoria__boton--tres" href="#">browse</a>
                    <img class="categoria__img categoria__img--col-6" src="<%:ListaCategoria[3].UrlImagen %>" alt="imagen de  <%:ListaCategoria[3].Nombre %>"/>
                </div>


            </div>

        </div>

        <div class="row fila">
            <div class="col-6 categoria categoria-tres">
                <div class="categoria__caja categoria__caja--cuatro">
                    <p class="categoria__p categoria__p--cuatro"><%:ListaCategoria[4].Nombre %></p>
                    <a class="categoria__boton categoria__boton--cuatro" href="#">browse</a>
                    <img class="categoria__img categoria__img--col-6" src="<%:ListaCategoria[4].UrlImagen %>" alt="imagen de  <%:ListaCategoria[4].Nombre %>"/>
                </div>
            </div>

            <div class="col-3 categoria categoria-uno">
                <div class="categoria__caja categoria__caja--cinco">
                    <p class="categoria__p "><%:ListaCategoria[5].Nombre %></p>
                    <a class="categoria__boton categoria__boton--cinco" href="#">browse</a>
                    <img class="categoria__img" src="<%:ListaCategoria[5].UrlImagen %>" alt="imagen de  <%:ListaCategoria[5].Nombre %>"/>

                </div>

               

            </div>
            <div class="col-3 categoria categoria-dos">
                <div class="categoria__caja categoria__caja--seis">
                    <p class="categoria__p "><%:ListaCategoria[6].Nombre %></p>
                    <a class="categoria__boton categoria__boton--seis" href="#">browse</a>
                    <img class="categoria__img" src="<%:ListaCategoria[6].UrlImagen %>" alt="imagen de <%:ListaCategoria[6].Nombre %>"/>
                </div>
            </div>

        </div>

    </div>











</asp:Content>
