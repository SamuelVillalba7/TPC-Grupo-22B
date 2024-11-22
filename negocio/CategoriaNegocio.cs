using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace negocio
{
    public class CategoriaNegocio
    {
        //public List<Categoria> listarConSP()
        //{
        //    List<Categoria> lista = new List<Categoria>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {

        //        //datos.setearConsulta("select top 7 C.IDCATEGORIA, C.NOMBRE , C.URLIMAGEN from CATEGORIAS as C");
        //        datos.setearConsulta("select top 7 C.IDCATEGORIA, C.NOMBRE , C.URLIMAGEN, C.FILTRO from CATEGORIAS as C");
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Categoria aux = new Categoria();

        //            aux.Id = (int)datos.Lector["IDCATEGORIA"];

        //            if (!(datos.Lector["NOMBRE"] is DBNull))
        //                aux.Nombre = (string)datos.Lector["NOMBRE"];

        //            if (!(datos.Lector["URLIMAGEN"] is DBNull))
        //                aux.UrlImagen = (string)datos.Lector["URLIMAGEN"];

        //            aux.filtro = (bool)datos.Lector["FILTRO"];
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

        public List<Categoria> listarConSP()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Limitar la consulta a las primeras 7 categorías ordenadas por el campo Orden
                datos.setearConsulta("SELECT TOP 7 IDCATEGORIA, NOMBRE, UrlImagen, Orden FROM Categorias ORDER BY Orden ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        Id = (int)datos.Lector["IDCATEGORIA"],
                        Nombre = (string)datos.Lector["Nombre"],
                        UrlImagen = (string)datos.Lector["UrlImagen"],
                        Orden = (int)datos.Lector["Orden"]
                    };
                    lista.Add(categoria);
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

        public void CambiarOrden(int categoriaId, string direccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (direccion == "Subir")
                {
                    datos.setearConsulta(@"UPDATE Categorias
                                    SET Orden = Orden - 1
                                    WHERE IDCATEGORIA = @CategoriaId
                                    AND Orden > 1"); // Evitar que baje a un orden menor a 1
                }
                else if (direccion == "Bajar")
                {
                    datos.setearConsulta(@"UPDATE Categorias
                                    SET Orden = Orden + 1
                                    WHERE IDCATEGORIA = @CategoriaId");
                }
                datos.setearParametro("@CategoriaId", categoriaId);
                datos.ejecutarAccion();
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
