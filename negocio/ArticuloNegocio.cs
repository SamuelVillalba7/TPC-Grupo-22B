using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
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
    }
}
