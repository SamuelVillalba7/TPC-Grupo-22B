using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProvinciaNegocio
    {
        public List<Provincia> ListarProvincias()
        {
            List<Provincia> listaProv = new List<Provincia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select IDPROVINCIA, NOMBRE FROM PROVINCIAS");


                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Provincia prov = new Provincia();


                    prov.IdProvincia = (int)datos.Lector["IDPROVINCIA"];
                    prov.NombreProv = (string)datos.Lector["NOMBRE"];

                 


                    listaProv.Add(prov);
                }

                return listaProv;
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
