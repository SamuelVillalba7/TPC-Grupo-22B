using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {

        public Articulo listarId(string id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA WHERE P.IDPRODUCTO=@ID");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                Articulo aux = new Articulo();
                while (datos.Lector.Read())
                {

                    aux.Id = (int)datos.Lector["ID"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];
                    if (!(datos.Lector["DESCRIPCION"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    if (!(datos.Lector["PRECIO"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["PRECIO"];

                    if (!(datos.Lector["URLIMG"] is DBNull))
                        aux.Imagen = (string)datos.Lector["URLIMG"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IDCATEGORIA"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IDCATEGORIA"];
                    if (!(datos.Lector["CATEGORIA"] is DBNull))
                        aux.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];


                    aux.Imagenes = cargarVecImagenes(aux.Id);


                }

                    return aux;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
      
           
        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["ID"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];
                    if (!(datos.Lector["DESCRIPCION"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    if (!(datos.Lector["PRECIO"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["PRECIO"];

                    if (!(datos.Lector["URLIMG"] is DBNull))
                        aux.Imagen = (string)datos.Lector["URLIMG"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IDCATEGORIA"] is DBNull))
                        aux.Categoria.Id= (int)datos.Lector["IDCATEGORIA"];
                    if (!(datos.Lector["CATEGORIA"] is DBNull))
                        aux.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];

                    aux.Imagenes = cargarVecImagenes(aux.Id);
                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> listarConSP(int idMarca = -1, int idCategoria = -1)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Caso: solo categoría
                if (idMarca == -1 && idCategoria != -1)
                {
                    datos.setearConsulta(@"
                SELECT 
                    P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, 
                    C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, 
                    (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, 
                    P.IDCATEGORIA, P.IDMARCA 
                FROM PRODUCTOS P
                INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
                INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA
                WHERE C.IDCATEGORIA = @Categoria");
                    datos.setearParametro("@Categoria", idCategoria);
                }
                // Caso: solo marca
                else if (idMarca != -1 && idCategoria == -1)
                {
                    datos.setearConsulta(@"
                SELECT 
                    P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, 
                    C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, 
                    (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, 
                    P.IDCATEGORIA, P.IDMARCA 
                FROM PRODUCTOS P
                INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
                INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA
                WHERE M.IDMARCA = @Marca");
                    datos.setearParametro("@Marca", idMarca);
                }
                // Caso: marca y categoría
                else if (idMarca != -1 && idCategoria != -1)
                {
                    datos.setearConsulta(@"
                SELECT 
                    P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, 
                    C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, 
                    (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, 
                    P.IDCATEGORIA, P.IDMARCA 
                FROM PRODUCTOS P
                INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
                INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA
                WHERE C.IDCATEGORIA = @Categoria AND M.IDMARCA = @Marca");
                    datos.setearParametro("@Categoria", idCategoria);
                    datos.setearParametro("@Marca", idMarca);
                }
                // Caso: sin filtros (todos los productos)
                else
                {
                    datos.setearConsulta(@"
                SELECT 
                    P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, 
                    C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, 
                    (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, 
                    P.IDCATEGORIA, P.IDMARCA 
                FROM PRODUCTOS P
                INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA
                INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA");
                }

                datos.ejecutarLectura();

                // Procesar los datos
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["ID"],
                        Nombre = datos.Lector["NOMBRE"] is DBNull ? null : (string)datos.Lector["NOMBRE"],
                        Descripcion = datos.Lector["DESCRIPCION"] is DBNull ? null : (string)datos.Lector["DESCRIPCION"],
                        Precio = datos.Lector["PRECIO"] is DBNull ? 0 : (decimal)datos.Lector["PRECIO"],
                        Imagen = datos.Lector["URLIMG"] is DBNull ? null : (string)datos.Lector["URLIMG"],
                        Categoria = new Categoria
                        {
                            Id = datos.Lector["IDCATEGORIA"] is DBNull ? 0 : (int)datos.Lector["IDCATEGORIA"],
                            Nombre = datos.Lector["CATEGORIA"] is DBNull ? null : (string)datos.Lector["CATEGORIA"]
                        },
                        Marca = new Marca
                        {
                            Codigo = datos.Lector["IDMARCA"] is DBNull ? 0 : (int)datos.Lector["IDMARCA"],
                            Nombre = datos.Lector["MARCA"] is DBNull ? null : (string)datos.Lector["MARCA"]
                        }
                    };

                    aux.Imagenes = cargarVecImagenes(aux.Id);
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        //public List<Articulo> listarConSP(int idMarca, int idCategoria)
        //{
        //    List<Articulo> lista = new List<Articulo>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        if(idMarca == -1)
        //        {
        //            datos.setearConsulta("SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA, P.IDMARCA FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA where C.IDCATEGORIA = @Categoria");
        //            datos.setearParametro("Categoria", idCategoria);
        //            datos.ejecutarLectura();
        //        }
        //        else
        //        {
        //            if (idCategoria == -1)
        //            {
        //                datos.setearConsulta("SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA, P.IDMARCA FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA where M.IDMARCA = @Marca");
        //                datos.setearParametro("Marca", idMarca);
        //                datos.ejecutarLectura();
        //            }
        //            else
        //            {
        //                datos.setearConsulta("SELECT P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, M.NOMBRE AS MARCA, (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA, P.IDMARCA FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA INNER JOIN MARCAS M ON M.IDMARCA = P.IDMARCA where C.IDCATEGORIA = @Categoria and M.IDMARCA = @Marca");
        //                datos.setearParametro("Marca", idMarca);
        //                datos.setearParametro("Categoria", idCategoria);
        //                datos.ejecutarLectura();
        //            }
        //        }

        //        while (datos.Lector.Read())
        //        {
        //            Articulo aux = new Articulo();

        //            aux.Id = (int)datos.Lector["ID"];

        //            if (!(datos.Lector["NOMBRE"] is DBNull))
        //                aux.Nombre = (string)datos.Lector["NOMBRE"];
        //            if (!(datos.Lector["DESCRIPCION"] is DBNull))
        //                aux.Descripcion = (string)datos.Lector["DESCRIPCION"];
        //            if (!(datos.Lector["PRECIO"] is DBNull))
        //                aux.Precio = (decimal)datos.Lector["PRECIO"];

        //            if (!(datos.Lector["URLIMG"] is DBNull))
        //                aux.Imagen = (string)datos.Lector["URLIMG"];
        //            aux.Categoria = new Categoria();
        //            aux.Marca = new Marca();
        //            if (!(datos.Lector["IDCATEGORIA"] is DBNull))
        //                aux.Categoria.Id = (int)datos.Lector["IDCATEGORIA"];
        //            if (!(datos.Lector["CATEGORIA"] is DBNull))
        //                aux.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
        //            if (!(datos.Lector["IDMARCA"] is DBNull))
        //                aux.Marca.Codigo = (int)datos.Lector["IDMARCA"];
        //            if (!(datos.Lector["MARCA"] is DBNull))
        //                aux.Marca.Nombre = (string)datos.Lector["MARCA"];

        //            aux.Imagenes = cargarVecImagenes(aux.Id);
        //            lista.Add(aux);

        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public int ConsultarStock(int IdProducto)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select IDPRODUCTO, STOCK from PRODUCTOS WHERE IDPRODUCTO = @IDPRO");
                datos.setearParametro("IDPRO", IdProducto);
                datos.ejecutarLectura();

                int stock = 0;

                while (datos.Lector.Read())
                {
                    

                    stock = (int)datos.Lector["STOCK"];

                    

                }

                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    

        public List<Articulo> listarArticulosRelacionados(int Id, int Categoria)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT DISTINCT TOP 4 P.IDPRODUCTO as ID, P.NOMBRE, P.DESCRIPCION, P.PRECIO, C.NOMBRE AS CATEGORIA, (SELECT TOP 1 URLIMG FROM IMAGENES I WHERE I.IDPRODUCTO = P.IDPRODUCTO) AS URLIMG, P.IDCATEGORIA FROM PRODUCTOS P INNER JOIN CATEGORIAS C ON C.IDCATEGORIA = P.IDCATEGORIA WHERE C.IDCATEGORIA = @Categoria and P.IDPRODUCTO <> @Id and P.STOCK > 0");
                datos.setearParametro("Id",Id);
                datos.setearParametro("Categoria",Categoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["ID"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];
                    if (!(datos.Lector["DESCRIPCION"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    if (!(datos.Lector["PRECIO"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["PRECIO"];

                    if (!(datos.Lector["URLIMG"] is DBNull))
                        aux.Imagen = (string)datos.Lector["URLIMG"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IDCATEGORIA"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IDCATEGORIA"];
                    if (!(datos.Lector["CATEGORIA"] is DBNull))
                        aux.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];

                    aux.Imagenes = cargarVecImagenes(aux.Id);
                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<string> cargarVecImagenes(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<string> lista = new List<string>();


            try
            {
                datos.setearConsulta("select IDIMAGEN, IDPRODUCTO, URLIMG from IMAGENES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (!((datos.Lector["URLIMG"] is DBNull)) && (Id == (int)datos.Lector["IDPRODUCTO"]))
                        lista.Add((string)datos.Lector["URLIMG"]);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }






        }

        public List<Articulo> listaFiltrandoCategoria()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearProcedimiento("storedListarPorCat");
                datos.ejecutarLectura();
      

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["ID"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];
                    if (!(datos.Lector["DESCRIPCION"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    if (!(datos.Lector["PRECIO"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["PRECIO"];

                    if (!(datos.Lector["URLIMG"] is DBNull))
                        aux.Imagen = (string)datos.Lector["URLIMG"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IDCATEGORIA"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IDCATEGORIA"];
                    if (!(datos.Lector["CATEGORIA"] is DBNull))
                        aux.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];

                    aux.Imagenes = cargarVecImagenes(aux.Id);
                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void limpiarFiltro()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update CATEGORIAS set FILTRO = 0");
                datos.ejecutarLectura();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //mover y crear la clase de negocio ItemCarritoNegocio
        public int encontrarArticulo(List<ItemCarrito> lista, int id)
        {
            int contador = 0;

            foreach(ItemCarrito item in lista)
            {
                if(item.art.Id == id)
                {
                    return contador;
                }

                contador++;
            }

            return -1;
        }
    }
}
