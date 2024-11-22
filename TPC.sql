CREATE DATABASE TPC1;
go 
use TPC1;
go 

CREATE TABLE CATEGORIAS(
    IDCATEGORIA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50),
    URLIMAGEN VARCHAR(100),
    VISIBLE bit DEFAULT 0,
	FILTRO bit DEFAULT 0,
    ESTADO BIT DEFAULT 1,
	Orden int not null DEFAULT 0
)
GO

CREATE TABLE MARCAS(
    IDMARCA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50),
    ESTADO BIT DEFAULT 1
)


GO
CREATE TABLE PRODUCTOS (
    IDPRODUCTO INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    IDCATEGORIA INT NULL FOREIGN KEY REFERENCES CATEGORIAS(IDCATEGORIA), 
    IDMARCA INT NULL FOREIGN KEY REFERENCES MARCAS(IDMARCA),
    NOMBRE VARCHAR(50) NOT NULL,
    PRECIO MONEY NOT NULL, 
    STOCK INT NOT NULL, 
    DESCRIPCION VARCHAR(250) NULL,
    ESTADO BIT DEFAULT 1
)

GO
CREATE TABLE IMAGENES(

    IDIMAGEN BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPRODUCTO INT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO), 
    URLIMG VARCHAR(1000) NOT NULL 

) 
GO


CREATE TABLE USUARIOS(
    IDUSUARIO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL,
    APELLIDO VARCHAR(50) NOT NULL,
    EMAIL VARCHAR(150) NOT NULL,
    CONTRASEÑA VARCHAR(50) NOT NULL,
    TELEFONO VARCHAR(15),
    ADMINISTRADOR BIT DEFAULT 0
)


go
INSERT into USUARIOS (NOMBRE,APELLIDO,EMAIL,CONTRASEÑA,TELEFONO,ADMINISTRADOR) VALUES ('SAMUEL','VILLALBA','VILLALBASAMUEL1@GMAIL.COM','1111','1151017737',1)



--SELECT * FROM USUARIOS





GO
CREATE TABLE ESTADOS(

    IDESTADO INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL 

) 
GO
INSERT INTO ESTADOS (NOMBRE) VALUES
('Pendiente'),         -- El pedido ha sido realizado, pero el pago aún no ha sido confirmado.
('Pago confirmado'),   -- El pago ha sido recibido y verificado, listo para ser procesado.
('En procesamiento'),  -- El pedido está siendo preparado (verificación de inventario, empaquetado, etc.).
('Enviado'),           -- El pedido ha sido despachado y está en camino.
('Entregado'),         -- El cliente ha recibido el pedido, proceso de entrega completo.
('Cancelado'),         -- El pedido fue cancelado por el cliente o la tienda.
('Devuelto'),          -- El cliente ha devuelto el pedido, en proceso de devolución o reembolso.
('Reembolsado'),       -- El reembolso ha sido procesado y el dinero devuelto al cliente.
('En espera'),         -- El pedido está pausado temporalmente por algún motivo.
('Fallido'),           -- El pago no fue procesado correctamente, el pedido no se completó.
('Listo para retirar'),          
('Finalizado'),           
('Retirado') ;          

GO
CREATE TABLE PROVINCIAS(

    IDPROVINCIA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL   

) 
GO
INSERT INTO PROVINCIAS (NOMBRE) VALUES
('Buenos Aires'),
('Catamarca'),
('Chaco'),
('Chubut'),
('Córdoba'),
('Corrientes'),
('Entre Ríos'),
('Formosa'),
('Jujuy'),
('La Pampa'),
('La Rioja'),
('Mendoza'),
('Misiones'),
('Neuquén'),
('Río Negro'),
('Salta'),
('San Juan'),
('San Luis'),
('Santa Cruz'),
('Santa Fe'),
('Santiago del Estero'),
('Tierra del Fuego'),
('Tucumán');

GO
CREATE TABLE METODODEPAGO(

    IDMETODO INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL 

) 
GO
INSERT INTO METODODEPAGO (NOMBRE) VALUES
('Transferencia'),
('Tarjeta de Débito'),
('Tarjeta de Crédito');
GO


--select * from DATOSENVIO

CREATE TABLE PEDIDOS(
    IDPEDIDO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDUSUARIO BIGINT NOT NULL FOREIGN KEY REFERENCES USUARIOS(IDUSUARIO),
    IDMETODO INT NOT NULL FOREIGN KEY REFERENCES METODODEPAGO(IDMETODO),
    IDESTADO INT NOT NULL FOREIGN KEY REFERENCES ESTADOS(IDESTADO),
    ENVIO BIT NOT NULL ,
    FECHAPEDIDO DATE DEFAULT GETDATE(),
    MONTOTOTAL MONEY NOT NULL
)

GO
CREATE TABLE DATOSENVIO(
    IDDATOSENVIO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPROVINCIA INT NOT NULL FOREIGN KEY REFERENCES PROVINCIAS(IDPROVINCIA),
    IDPEDIDO BIGINT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(IDPEDIDO),
    CIUDAD VARCHAR(60) NOT NULL , 
    CODIGOPOSTAL VARCHAR(10) NOT NULL,
    DIRECCION VARCHAR(100) NOT NULL

)

GO

CREATE TABLE DETALLEPEDIDOS(
    IDDETALLE BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPEDIDO BIGINT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(IDPEDIDO),
    IDPRODUCTO INT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO),
    CANTIDAD SMALLINT NOT NULL
)

GO
create procedure storedListar as 
SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I
WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P 
INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA

GO 

create procedure storedListarPorCat as 
SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I
WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P 
INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
and C.FILTRO = 1

go

--select top 7 C.IDCATEGORIA, C.NOMBRE from CATEGORIAS as C
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Auriculares','https://localhost:44317/img/productos/auricular.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Celulares','https://localhost:44317/img/productos/celular.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Monitores','https://localhost:44317/img/productos/monitor.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Parlantes','https://localhost:44317/img/productos/parlante.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Consolas','https://localhost:44317/img/productos/consola.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Relojes','https://localhost:44317/img/productos/reloj.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Mouses','https://localhost:44317/img/productos/mouse.png')

--SELECT * from PRODUCTOS MARCAS CATEGORIAS


INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Hyperx',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Samsung',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Redragon',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Apple',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Motorola',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Sony',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('JBL',1)



insert into PRODUCTOS (IDMARCA,IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (1,1, 'Hyperx Cloud Flight', 250000, 500, 'Auris gamer copados')

insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://row.hyperx.com/cdn/shop/products/hyperx_cloud_20flight_1_main.jpg?v=1662435222')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://http2.mlstatic.com/D_NQ_NP_870917-MLU78798955617_082024-O.webp')


insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (2,2, 'Samsung A55 5G', 500000, 200, 'Celular gama alta')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (2, 'https://http2.mlstatic.com/D_NQ_NP_828557-MLA75148190826_032024-O.webp')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (3,1, 'Redragon Zeus', 125000, 120, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (3, 'https://spacegamer.com.ar/img/Public/1058-producto-1-5143.jpg')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (4,2, 'IPhone 15 Pro Max', 1500000, 150, 'Celular gama alta')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (4, 'https://http2.mlstatic.com/D_Q_NP_912227-MLA71782903150_092023-O.webp')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (5,2, 'Motorola Moto G14', 300000, 400, 'Celular gama de entrada')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (5, 'https://tiendadiggit.com.ar/web/image/product.template/246056/image_1024?unique=199acc8')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (6,1, 'Sony WH-XB810', 250000, 120, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (6, 'https://www.sony.com.ar/image/dd18cf93606d238305a733d336c45537?fmt=pjpeg&wid=330&bgcolor=FFFFFF&bgc=FFFFFF')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (7,1, 'JBL Tune 520', 200000, 820, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (7, 'https://ar.oneclickstore.com/wp-content/uploads/2023/10/JBLT520BTBLUAM.jpg')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (1,1, 'Audio-technica Ath-m40x', 350000, 80, 'Auriculares de estudio')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (8, 'https://www.avisistemas.com.ar/795-thickbox_default/audio-technica-ath-m40x-auricular-profesional-estudio.jpg')


=======
CREATE DATABASE TPC1;
go 
use TPC1;
go 

CREATE TABLE CATEGORIAS(
    IDCATEGORIA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50),
    URLIMAGEN VARCHAR(100),
    VISIBLE bit DEFAULT 0,
	FILTRO bit DEFAULT 0,
    ESTADO BIT DEFAULT 1
)
GO

CREATE TABLE MARCAS(
    IDMARCA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50),
    ESTADO BIT DEFAULT 1
)


GO
CREATE TABLE PRODUCTOS (
    IDPRODUCTO INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    IDCATEGORIA INT NULL FOREIGN KEY REFERENCES CATEGORIAS(IDCATEGORIA), 
    IDMARCA INT NULL FOREIGN KEY REFERENCES MARCAS(IDMARCA),
    NOMBRE VARCHAR(50) NOT NULL,
    PRECIO MONEY NOT NULL, 
    STOCK INT NOT NULL, 
    DESCRIPCION VARCHAR(250) NULL,
    ESTADO BIT DEFAULT 1
)

GO
CREATE TABLE IMAGENES(

    IDIMAGEN BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPRODUCTO INT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO), 
    URLIMG VARCHAR(1000) NOT NULL 

) 
GO


CREATE TABLE USUARIOS(
    IDUSUARIO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL,
    APELLIDO VARCHAR(50) NOT NULL,
    EMAIL VARCHAR(150) NOT NULL,
    CONTRASEÑA VARCHAR(50) NOT NULL,
    TELEFONO VARCHAR(15),
    ADMINISTRADOR BIT DEFAULT 0
)


go
INSERT into USUARIOS (NOMBRE,APELLIDO,EMAIL,CONTRASEÑA,TELEFONO,ADMINISTRADOR) VALUES ('SAMUEL','VILLALBA','VILLALBASAMUEL1@GMAIL.COM','1111','1151017737',1)



--SELECT * FROM USUARIOS





GO
CREATE TABLE ESTADOS(

    IDESTADO INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL 

) 
GO
INSERT INTO ESTADOS (NOMBRE) VALUES
('Pendiente'),         -- El pedido ha sido realizado, pero el pago aún no ha sido confirmado.
('Pago confirmado'),   -- El pago ha sido recibido y verificado, listo para ser procesado.
('En procesamiento'),  -- El pedido está siendo preparado (verificación de inventario, empaquetado, etc.).
('Enviado'),           -- El pedido ha sido despachado y está en camino.
('Entregado'),         -- El cliente ha recibido el pedido, proceso de entrega completo.
('Cancelado'),         -- El pedido fue cancelado por el cliente o la tienda.
('Devuelto'),          -- El cliente ha devuelto el pedido, en proceso de devolución o reembolso.
('Reembolsado'),       -- El reembolso ha sido procesado y el dinero devuelto al cliente.
('En espera'),         -- El pedido está pausado temporalmente por algún motivo.
('Fallido'),           -- El pago no fue procesado correctamente, el pedido no se completó.
('Listo para retirar'),          
('Finalizado'),           
('Retirado') ;          

GO
CREATE TABLE PROVINCIAS(

    IDPROVINCIA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL   

) 
GO
INSERT INTO PROVINCIAS (NOMBRE) VALUES
('Buenos Aires'),
('Catamarca'),
('Chaco'),
('Chubut'),
('Córdoba'),
('Corrientes'),
('Entre Ríos'),
('Formosa'),
('Jujuy'),
('La Pampa'),
('La Rioja'),
('Mendoza'),
('Misiones'),
('Neuquén'),
('Río Negro'),
('Salta'),
('San Juan'),
('San Luis'),
('Santa Cruz'),
('Santa Fe'),
('Santiago del Estero'),
('Tierra del Fuego'),
('Tucumán');

GO
CREATE TABLE METODODEPAGO(

    IDMETODO INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50) NOT NULL 

) 
GO
INSERT INTO METODODEPAGO (NOMBRE) VALUES
('Transferencia'),
('Tarjeta de Débito'),
('Tarjeta de Crédito');
GO


--select * from DATOSENVIO

CREATE TABLE PEDIDOS(
    IDPEDIDO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDUSUARIO BIGINT NOT NULL FOREIGN KEY REFERENCES USUARIOS(IDUSUARIO),
    IDMETODO INT NOT NULL FOREIGN KEY REFERENCES METODODEPAGO(IDMETODO),
    IDESTADO INT NOT NULL FOREIGN KEY REFERENCES ESTADOS(IDESTADO),
    ENVIO BIT NOT NULL ,
    FECHAPEDIDO DATE DEFAULT GETDATE(),
    MONTOTOTAL MONEY NOT NULL
)

GO
CREATE TABLE DATOSENVIO(
    IDDATOSENVIO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPROVINCIA INT NOT NULL FOREIGN KEY REFERENCES PROVINCIAS(IDPROVINCIA),
    IDPEDIDO BIGINT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(IDPEDIDO),
    CIUDAD VARCHAR(60) NOT NULL , 
    CODIGOPOSTAL VARCHAR(10) NOT NULL,
    DIRECCION VARCHAR(100) NOT NULL

)

GO

CREATE TABLE DETALLEPEDIDOS(
    IDDETALLE BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPEDIDO BIGINT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(IDPEDIDO),
    IDPRODUCTO INT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO),
    CANTIDAD SMALLINT NOT NULL
)

GO
create procedure storedListar as 
SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I
WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P 
INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA

GO 

create procedure storedListarPorCat as 
SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I
WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P 
INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
and C.FILTRO = 1

go

--select top 7 C.IDCATEGORIA, C.NOMBRE from CATEGORIAS as C
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Auriculares','https://localhost:44317/img/productos/auricular.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Celulares','https://localhost:44317/img/productos/celular.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Monitores','https://localhost:44317/img/productos/monitor.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Parlantes','https://localhost:44317/img/productos/parlante.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Consolas','https://localhost:44317/img/productos/consola.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Relojes','https://localhost:44317/img/productos/reloj.png')
insert into CATEGORIAS (NOMBRE,URLIMAGEN) values ('Mouses','https://localhost:44317/img/productos/mouse.png')

--SELECT * from PRODUCTOS MARCAS CATEGORIAS


INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Hyperx',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Samsung',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Redragon',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Apple',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Motorola',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('Sony',1)
INSERT INTO MARCAS (NOMBRE, ESTADO) values ('JBL',1)



insert into PRODUCTOS (IDMARCA,IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (1,1, 'Hyperx Cloud Flight', 250000, 500, 'Auris gamer copados')

insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://row.hyperx.com/cdn/shop/products/hyperx_cloud_20flight_1_main.jpg?v=1662435222')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://http2.mlstatic.com/D_NQ_NP_870917-MLU78798955617_082024-O.webp')


insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (2,2, 'Samsung A55 5G', 500000, 200, 'Celular gama alta')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (2, 'https://http2.mlstatic.com/D_NQ_NP_828557-MLA75148190826_032024-O.webp')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (3,1, 'Redragon Zeus', 125000, 120, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (3, 'https://spacegamer.com.ar/img/Public/1058-producto-1-5143.jpg')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (4,2, 'IPhone 15 Pro Max', 1500000, 150, 'Celular gama alta')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (4, 'https://http2.mlstatic.com/D_Q_NP_912227-MLA71782903150_092023-O.webp')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (5,2, 'Motorola Moto G14', 300000, 400, 'Celular gama de entrada')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (5, 'https://tiendadiggit.com.ar/web/image/product.template/246056/image_1024?unique=199acc8')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (6,1, 'Sony WH-XB810', 250000, 120, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (6, 'https://www.sony.com.ar/image/dd18cf93606d238305a733d336c45537?fmt=pjpeg&wid=330&bgcolor=FFFFFF&bgc=FFFFFF')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (7,1, 'JBL Tune 520', 200000, 820, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (7, 'https://ar.oneclickstore.com/wp-content/uploads/2023/10/JBLT520BTBLUAM.jpg')

insert into PRODUCTOS (IDMARCA, IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (1,1, 'Audio-technica Ath-m40x', 350000, 80, 'Auriculares de estudio')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (8, 'https://www.avisistemas.com.ar/795-thickbox_default/audio-technica-ath-m40x-auricular-profesional-estudio.jpg')

