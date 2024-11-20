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
   
        public int RegistrarPedido(int IDUSUARIO, int IDMETODO, int IDESTADO, DateTime FECHAPEDIDO,int ENVIO, decimal MONTOTOTAL)
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
             
                datos.setearConsulta("select IDPEDIDO, IDUSUARIO, IDMETODO, IDESTADO, FECHAPEDIDO, MONTOTOTAL from PEDIDOS");

              
                datos.ejecutarLectura();

               
                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();

                  
                    pedido.IdPedido = (int)datos.Lector["IDPEDIDO"];
                    pedido.IdUsuario = (int)datos.Lector["IDUSUARIO"];
                    pedido.IdMetodoPago = (int)datos.Lector["IDMETODO"];
                    pedido.IdEstado = (int)datos.Lector["IDESTADO"];
                    pedido.FechaPedido = (DateTime)datos.Lector["FECHAPEDIDO"];
                    pedido.MontoTotal = (decimal)datos.Lector["MONTOTOTAL"];
                    
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
