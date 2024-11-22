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
        public List<Estados> ListarEstados()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Estados> estados = new List<Estados>();

            try
            {
                datos.setearConsulta("SELECT IDESTADO, NOMBRE FROM ESTADOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estados estado = new Estados
                    {
                        IdEstado = (int)datos.Lector["IDESTADO"],
                        Nombre = datos.Lector["NOMBRE"].ToString()
                    };

                    estados.Add(estado);
                }

                return estados;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Estados> ListarEstados(int IdEstadoActual, int IdEnvio)
        {
            //Retiro en tienda -> 0
            //Envío a domicilio -> 1

            List<Estados> estados = new List<Estados>();

            switch (IdEstadoActual)
            {
                // Pendiente -> Fallido/Pago confirmado
                case 1:
                    estados.Add(new Estados { IdEstado = 10, Nombre = "Fallido" });
                    estados.Add(new Estados { IdEstado = 2, Nombre = "Pago confirmado" });
                    break;
                // Pago confirmado -> En procesamiento
                case 2:
                    estados.Add(new Estados { IdEstado = 3, Nombre = "En procesamiento" });
                    break;
                // En procesamiento -> Enviado/Listo para retirar
                case 3:
                    if (IdEnvio == 1) // Envío a domicilio
                    {
                        estados.Add(new Estados { IdEstado = 4, Nombre = "Enviado" });
                    }
                    else // Retiro en tienda
                    {
                        estados.Add(new Estados { IdEstado = 11, Nombre = "Listo para retirar" });
                    }
                    break;
                // Enviado -> Entregado
                case 4:
                    estados.Add(new Estados { IdEstado = 5, Nombre = "Entregado" });
                    break;
                // Entregado -> En espera/Finalizado
                case 5:
                    estados.Add(new Estados { IdEstado = 9, Nombre = "En espera" });
                    estados.Add(new Estados { IdEstado = 12, Nombre = "Finalizado" });
                    break;
                // Cancelado -> Reembolsado
                case 6:
                    estados.Add(new Estados { IdEstado = 8, Nombre = "Reembolsado" });
                    break;
                // Devuelto -> En procesamiento/Reembolsado
                case 7:
                    estados.Add(new Estados { IdEstado = 3, Nombre = "En procesamiento" });
                    estados.Add(new Estados { IdEstado = 8, Nombre = "Reembolsado" });
                    break;
                // En espera -> Devuelto
                case 9:
                    estados.Add(new Estados { IdEstado = 7, Nombre = "Devuelto" });
                    break;
                //case 10:
                //    estados.Add(new Estados { IdEstado = 10, Nombre = "Fallido" });
                //    break;
                // Fallido o estado desconocido
                case 11:
                    estados.Add(new Estados { IdEstado = 13, Nombre = "Retirado" });
                    break;
                case 13:
                    estados.Add(new Estados { IdEstado = 9, Nombre = "En espera" });
                    estados.Add(new Estados { IdEstado = 12, Nombre = "Finalizado" }); ;
                    break;
                default:
                    break;
            }

            return estados;
        }



        public void ActualizarEstadoPedido(int idPedido, int nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET IDESTADO = @IdEstado WHERE IDPEDIDO = @IdPedido");
                datos.setearParametro("@IdEstado", nuevoEstado);
                datos.setearParametro("@IdPedido", idPedido);
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

        //public List<string> ListarEstados()
        //{
        //    return new List<string> { "Pendiente", "Enviado", "Entregado", "Cancelado" };
        //}


    }


}
