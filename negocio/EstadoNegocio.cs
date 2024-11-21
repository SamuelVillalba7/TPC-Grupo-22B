using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EstadoNegocio
    {
        public List<Estados> listar()
        {
            List<Estados> lista = new List<Estados>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select IDESTADO, NOMBRE from ESTADOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estados aux = new Estados();

                    if (!(datos.Lector["IDESTADO"] is DBNull))
                        aux.IdEstado = (int)datos.Lector["IDESTADO"];

                    if (!(datos.Lector["NOMBRE"] is DBNull))
                        aux.Nombre = (string)datos.Lector["NOMBRE"];


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

        public List<string> ListarEstados()
        {
            return new List<string> { "Pendiente", "Enviado", "Entregado", "Cancelado" };
        }
    }


}
