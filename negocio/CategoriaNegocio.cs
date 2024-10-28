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
        public List<Categoria> listarConSP()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select top 7 C.IDCATEGORIA, C.NOMBRE , C.URLIMAGEN from CATEGORIAS as C");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lector["IDCATEGORIA"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];

                    if (!(datos.Lector["URLIMAGEN"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["URLIMAGEN"];


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
    }
}
