CREATE DATABASE TPC1;
go 
use TPC1;
go 

CREATE TABLE CATEGORIAS(
    IDCATEGORIA INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NOMBRE VARCHAR(50)
)
GO
CREATE TABLE PRODUCTOS (
    IDPRODUCTO INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    IDCATEGORIA INT NULL FOREIGN KEY REFERENCES CATEGORIAS(IDCATEGORIA), 
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
    URLIMG VARCHAR(1000) NOT NULL ,  

) 
GO

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
GO

CREATE TABLE PEDIDOS(
    IDPEDIDO BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDUSUARIO BIGINT NOT NULL FOREIGN KEY REFERENCES USUARIOS(IDUSUARIO),
    FECHAPEDIDO DATE DEFAULT GETDATE(),
    MONTOTOTAL MONEY NOT NULL
)
GO

CREATE TABLE DETALLEPEDIDOS(
    IDDETALLE BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IDPEDIDO BIGINT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(IDPEDIDO),
    IDPRODUCTO INT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO),
    CANTIDAD SMALLINT NOT NULL,
)

create procedure storedListar as 
SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I
WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P 
INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA

insert into CATEGORIAS (NOMBRE) values ('Celulares')
insert into CATEGORIAS (NOMBRE) values ('Auriculares')

insert into PRODUCTOS (IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (2, 'Hyperx Cloud Flight', 250000, 500, 'Auris gamer copados')

insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://row.hyperx.com/cdn/shop/products/hyperx_cloud_20flight_1_main.jpg?v=1662435222')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (1, 'https://http2.mlstatic.com/D_NQ_NP_870917-MLU78798955617_082024-O.webp')


insert into PRODUCTOS (IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (1, 'Samsung A55 5G', 500000, 200, 'Celular gama alta')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (2, 'https://http2.mlstatic.com/D_NQ_NP_828557-MLA75148190826_032024-O.webp')


insert into PRODUCTOS (IDCATEGORIA, NOMBRE, PRECIO, STOCK, DESCRIPCION) values (2, 'Redragon Zeus', 125000, 120, 'Auriculares inalambricos')
insert into IMAGENES (IDPRODUCTO, URLIMG) values (3, 'https://spacegamer.com.ar/img/Public/1058-producto-1-5143.jpg')