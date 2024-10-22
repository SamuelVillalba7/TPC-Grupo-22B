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
    IDPRODUCTO BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
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
    IDPRODUCTO BIGINT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO), 
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
    IDPRODUCTO BIGINT NOT NULL FOREIGN KEY REFERENCES PRODUCTOS(IDPRODUCTO),
    CANTIDAD SMALLINT NOT NULL,
    PRECIOUNITARIO MONEY NOT NULL
)

