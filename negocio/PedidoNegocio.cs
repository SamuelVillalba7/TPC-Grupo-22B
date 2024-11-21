using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PedidoNegocio
    {

        public int RegistrarPedido(int IDUSUARIO, int IDMETODO, int IDESTADO, DateTime FECHAPEDIDO, int ENVIO, decimal MONTOTOTAL)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {


                datos.setearConsulta("insert into PEDIDOS (IDUSUARIO, IDMETODO, IDESTADO, FECHAPEDIDO,ENVIO, MONTOTOTAL ) VALUES (@IdUsuario, @IdMetodo, @IdEstado, @Fecha,@Envio, @MontoTotal) SELECT CAST(SCOPE_IDENTITY() AS INT) AS UltimoIDPedido;");
                datos.setearParametro("@IdUsuario", IDUSUARIO);
                datos.setearParametro("@IdMetodo", IDMETODO);
                datos.setearParametro("@IdEstado", IDESTADO);
                datos.setearParametro("@Envio", ENVIO);
                datos.setearParametro("@Fecha", FECHAPEDIDO);
                datos.setearParametro("@MontoTotal", MONTOTOTAL);

                int ultimoIDPedido = Convert.ToInt32(datos.ejecutarEscalar());

                return ultimoIDPedido;


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

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select P.IDPEDIDO, P.IDUSUARIO, U.NOMBRE AS UNOMBRE, P.IDMETODO, MP.NOMBRE AS MPNOMBRE, P.IDESTADO, E.NOMBRE AS ENOMBRE,  P.ENVIO, P.FECHAPEDIDO, P.MONTOTOTAL from PEDIDOS P INNER JOIN USUARIOS U ON P.IDUSUARIO = U.IDUSUARIO INNER JOIN METODODEPAGO MP ON P.IDMETODO = MP.IDMETODO INNER JOIN ESTADOS E ON P.IDESTADO = E.IDESTADO");


                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();


                    pedido.IdPedido = (int)(long)(datos.Lector["IDPEDIDO"]);
                    pedido.IdUsuario = (int)(long)(datos.Lector["IDUSUARIO"]);
                    pedido.IdMetodoPago = (int)datos.Lector["IDMETODO"];
                    pedido.IdEstado = (int)datos.Lector["IDESTADO"];
                    pedido.FechaPedido = (DateTime)datos.Lector["FECHAPEDIDO"];
                    pedido.MontoTotal = (decimal)datos.Lector["MONTOTOTAL"];
                    pedido.NombreUsuario = (string)datos.Lector["UNOMBRE"];
                    pedido.MetodoNombre = (string)datos.Lector["MPNOMBRE"];
                    pedido.EstadoNombre = (string)datos.Lector["ENOMBRE"];

                    listaPedidos.Add(pedido);
                }

                return listaPedidos;
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
