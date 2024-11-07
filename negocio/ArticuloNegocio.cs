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
