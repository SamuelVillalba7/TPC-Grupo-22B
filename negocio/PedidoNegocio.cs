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
        public void RegistrarPedido(int IDUSUARIO, int IDPROVINCIA, int IDMETODO, string CIUDAD, string CODIGOPOSTAL, string DIRECCION, int IDESTADO, DateTime FECHAPEDIDO, decimal MONTOTOTAL)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();
            try
            {


                datos.setearConsulta("insert into PEDIDOS (IDUSUARIO, IDPROVINCIA, IDMETODO, CIUDAD, CODIGOPOSTAL, DIRECCION, IDESTADO, FECHAPEDIDO, MONTOTOTAL ) VALUES (@IdUsuario, @IdProvincia, @IdMetodo, @Ciudad, @CodigoPostal, @Direccion, @IdEstado, @Fecha, @MontoTotal)");
                datos.setearParametro("@IdUsuario", IDUSUARIO);
                datos.setearParametro("@IdProvincia", IDPROVINCIA);
                datos.setearParametro("@IdMetodo", IDMETODO);
                datos.setearParametro("@Ciudad", CIUDAD);
                datos.setearParametro("@CodigoPostal", CODIGOPOSTAL);
                datos.setearParametro("@Direccion", DIRECCION);
                datos.setearParametro("@IdEstado", IDESTADO);
                datos.setearParametro("@Fecha", FECHAPEDIDO);
                datos.setearParametro("@MontoTotal", MONTOTOTAL);

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

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
             
                datos.setearConsulta("select IDPEDIDO, IDUSUARIO, IDPROVINCIA, IDMETODO, CIUDAD, CODIGOPOSTAL, DIRECCION, IDESTADO, FECHAPEDIDO, MONTOTOTAL from PEDIDOS");

              
                datos.ejecutarLectura();

               
                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();

                  
                    pedido.IdPedido = (int)datos.Lector["IDPEDIDO"];
                    pedido.IdUsuario = (int)datos.Lector["IDUSUARIO"];
                    pedido.IdProvincia = (int)datos.Lector["IDPROVINCIA"];
                    pedido.IdMetodoPago = (int)datos.Lector["IDMETODO"];
                    pedido.Ciudad = datos.Lector["CIUDAD"].ToString();
                    pedido.CP = datos.Lector["CODIGOPOSTAL"].ToString();
                    pedido.Direccion = datos.Lector["DIRECCION"].ToString();
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
