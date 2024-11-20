using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class DatosEnvioNegocio
    {
        public DatosEnvioNegocio() { }
        public void agregar(int IdPedido, int IdProvincia,string Ciudad , string CP ,string Direccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("insert into DATOSENVIO (IDPROVINCIA,IDPEDIDO, CIUDAD, CODIGOPOSTAL, DIRECCION ) VALUES ( @IdProvincia,@IdPedido,@Ciudad, @CodigoPostal, @Direccion)");
                datos.setearParametro("@IdPedido", IdPedido);
                datos.setearParametro("@IdProvincia", IdProvincia);
                datos.setearParametro("@Ciudad", Ciudad);
                datos.setearParametro("@CodigoPostal",CP);
                datos.setearParametro("@Direccion", Direccion);
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

        public DatosEnvio buscarPorIDPedido(int id)
        {
           
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select IDPROVINCIA,IDPEDIDO, CIUDAD, CODIGOPOSTAL, DIRECCION from DATOSENVIO WHERE IDPEDIDO=@idPedido");
                datos.setearParametro("@IdPedido", id);
                datos.ejecutarLectura();

                DatosEnvio envio = new DatosEnvio();
                while (datos.Lector.Read())                {
                    envio.IdDatosEnvio = (int)datos.Lector["IDDATOSENVIO"];
                    envio.IdPedido = (int)datos.Lector["IDPEDIDO"];
                    envio.IdProvincia = (int)datos.Lector["IDPROVINCIA"];
                    envio.Ciudad = datos.Lector["CIUDAD"].ToString();
                    envio.CP = datos.Lector["CODIGOPOSTAL"].ToString();
                    envio.Direccion = datos.Lector["DIRECCION"].ToString();


                }

                return envio;
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
