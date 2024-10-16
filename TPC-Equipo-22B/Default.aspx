<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo_22B.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style/home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="categorias-container">
            
            <div class="categoria categoria--headphone">

                <div class="categoria__caja">
                    <h2 class="categoria__h2 categoria__h2--headphone">Beats Solo</h2>
                    <span class="categoria__span categoria__span--headphone">Wireless</span>
                    <p class="categoria__p categoria__p--headphone">HEADPHONE</p>
                </div>

                <div class="categoria__caja-bottom">
                        
                    <a class="categoria__boton categoria__boton--headphone" href="#">Shop By Category</a>     
                    <div class="categoria__descripcion">
                            <div class="categoria__descripcion-titulo">
                                <p>Description</p>
                            </div>
                            <div class="categoria__descripcion-texto">
                                <p>there are many vatiations passages <br>
                                 of Lorem Ipsum available, but the <br>
                                 majority have suffered alteration</p>
                            </div>
                    </div>
                     
                </div>    
            
                <img class="categoria__img categoria__img--headphone" src="./img/categorias/headphone.png" alt="imagen de auriculares"/>

            </div>


            <div class="categoria categoria--earphone">

                    <div class="categoria__caja categoria__caja--earphone">
                        <h2 class="categoria__h2 categoria__h2--earphone">Enjoy</h2>
                        <span class="categoria__span categoria__span--earphone">With</span>
                        <p class="categoria__p categoria__p--earphone">EARPHONE</p>
                        <a class="categoria__boton categoria__boton--earphone" href="#">browse</a>
    
                    </div>
  
                <img class="categoria__img categoria__img--earphone" src="./img/categorias/earphone.png" alt="imagen de auriculares"/>

            </div>

            <div class="categoria categoria--gadgets">

                <div class="categoria__caja categoria__caja--gadgets">
                    <h2 class="categoria__h2 categoria__h2--gadgets">New</h2>
                    <span class="categoria__span categoria__span--gadgets">Wear</span>
                    <p class="categoria__p categoria__p--gadgets">GADGETS</p>
                    <a class="categoria__boton categoria__boton--gadgets" href="#">browse</a>

                </div>

            <img class="categoria__img categoria__img--gadgets" src="./img/categorias/gadgets.png" alt="imagen de auriculares"/>

            </div>

            <div class="categoria categoria--laptop">

            <div class="categoria__caja">
                <h2 class="categoria__h2 categoria__h2">Trend</h2>
                <span class="categoria__span categoria__span">Devices</span>
                <p class="categoria__p categoria__p--laptop">LAPTOP</p>
                <a class="categoria__boton categoria__boton--laptop" href="#">browse</a>

            </div>

            <img class="categoria__img categoria__img--laptop" src="./img/categorias/laptop.png" alt="imagen de auriculares"/>

            </div>

            <div class="categoria categoria--console">

                <div class="categoria__caja">
                    <h2 class="categoria__h2 categoria__h2--console">Best</h2>
                    <span class="categoria__span categoria__span--console">Gaming</span>
                    <p class="categoria__p categoria__p--console">CONSOLE</p>
                    <a class="categoria__boton categoria__boton--console"  href="#">browse</a>

                </div>

                 <img class="categoria__img categoria__img--console" src="./img/categorias/console.png" alt="imagen de auriculares"/>

            </div>

            <div class="categoria categoria--oculus">

                <div class="categoria__caja">
                    <h2 class="categoria__h2 categoria__h2">Play</h2>
                    <span class="categoria__span categoria__span">Game</span>
                    <p class="categoria__p categoria__p">OCULUS</p>
                    <a class="categoria__boton categoria__boton--oculus"  href="#">browse</a>

                </div>

                 <img class="categoria__img categoria__img--oculus" src="./img/categorias/oculus.png" alt="imagen de auriculares"/>

            </div>        
            
            <div class="categoria categoria--speaker">

                <div class="categoria__caja">
                    <h2 class="categoria__h2 categoria__h2">New</h2>
                    <span class="categoria__span categoria__span">Amazon</span>
                    <p class="categoria__p categoria__p">SPEAKER</p>
                    <a class="categoria__boton categoria__boton--speaker"  href="#">browse</a>

                </div>

                 <img class="categoria__img categoria__img--speaker" src="./img/categorias/speaker.png" alt="imagen de auriculares"/>

            </div> 
        
        </div>


    <div class="servicios-container">

            <div class="servicio">
                <img class="servicio__img" src="./img/servicios/envio.svg" alt="">
                <div class="servicio__texto">
                    <h3 class="servicio__h3">Free Shipping</h3>
                    <p class="servicio__p">Free Shipping On All Order</p>
                </div>
            </div>

            <div class="servicio">
                <img class="servicio__img" src="./img/servicios/garantia.svg" alt="">
                <div class="servicio__texto">
                    <h3 class="servicio__h3">Money Guarantee</h3>
                    <p class="servicio__p" >30 Day Money Back</p>
                </div>
            </div>

            <div class="servicio">
                <img class="servicio__img" src="./img/servicios/soporte.svg" alt="">
                <div class="servicio__texto">
                    <h3 class="servicio__h3">Online Support 24/7</h3>
                    <p class="servicio__p">Technical Support 24/7</p>
                </div>
            </div>

            <div class="servicio">
                <img class="servicio__img" src="./img/servicios/pago.svg" alt="">
                <div class="servicio__texto">
                    <h3 class="servicio__h3">Secure Payment</h3>
                    <p class="servicio__p">All Cards Accepted</p>
                </div>
            </div>



        </div>




    <div class="oferta-container">
            <div class="oferta">
                <div class="oferta__caja oferta__caja--primera">
                    <div class="oferta__texto">
                        <h2 class="oferta__h2" >20% OFF</h2>
                        <span class="oferta__span">FINE <br> SMILE</span>
                        <p class="oferta__p">15 Nov To 7 Dec</p>  
                    </div>    
                </div>
                <div class="oferta__caja oferta__caja--segunda">
                        <h2 class="oferta__h2 oferta__h2--segundo">Beats Solo Air</h2>
                        <span class="oferta__span oferta__span--segundo">Summer Sale</span>
                        <p class="oferta__p oferta__p--segundo">Company that’s grown from 270 to 480 <br> employees in the last 12 months.</p>
                        <a class="categoria__boton categoria__boton--laptop" href="#">Shop</a>        
                </div>
                <img class="oferta__img" src="./img/ofertas/auriculares.png" alt="imagen">
            </div>
        </div>


                 <div class="title-container">
                    <h2 class="title">Best Seller Products</h2>
                    <span class="title__text">speakerThere are many variations passages</span>
                </div>



         <div class="oferta-container">
            <div class="oferta oferta--last">
                <div class="oferta__caja oferta__caja--primera">
                    <div class="oferta__texto">
                        <h2 class="oferta__h2" >20% OFF</h2>
                        <span class="oferta__span">HAPPY <br> HOURSE</span>
                        <p class="oferta__p">15 Nov To 7 Dec</p>  
                    </div>    
                </div>
                <div class="oferta__caja oferta__caja--segunda">
                        <h2 class="oferta__h2 oferta__h2--segundo">Beats Solo Air</h2>
                        <span class="oferta__span oferta__span--segundo">Summer Sale</span>
                        <p class="oferta__p oferta__p--segundo">Company that’s grown from 270 to 480 <br> employees in the last 12 months.</p>
                        <a class="categoria__boton categoria__boton--oculus" href="#">Shop</a>        
                </div>
                <img class="oferta__img oferta__img--last" src="./img/ofertas/reloj.png" alt="imagen"/>
            </div>
        </div>




    
</asp:Content>
