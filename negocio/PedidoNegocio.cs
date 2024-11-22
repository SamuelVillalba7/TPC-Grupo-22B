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

        public List<Pedido> ListarPedidosPorUsuario(int idUsuario)
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta SQL para filtrar pedidos por usuario
                datos.setearConsulta(@"
            SELECT 
                P.IDPEDIDO, 
                P.IDUSUARIO, 
                U.NOMBRE AS UNOMBRE, 
                P.IDMETODO, 
                MP.NOMBRE AS MPNOMBRE, 
                P.IDESTADO, 
                E.NOMBRE AS ENOMBRE, 
                P.FECHAPEDIDO, 
                P.MONTOTOTAL
            FROM 
                PEDIDOS P
            INNER JOIN USUARIOS U ON P.IDUSUARIO = U.IDUSUARIO
            INNER JOIN METODODEPAGO MP ON P.IDMETODO = MP.IDMETODO
            INNER JOIN ESTADOS E ON P.IDESTADO = E.IDESTADO
            WHERE P.IDUSUARIO = @IdUsuario");

                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        IdPedido = (int)(long)(datos.Lector["IDPEDIDO"]),
                        IdUsuario = (int)(long)(datos.Lector["IDUSUARIO"]),
                        NombreUsuario = datos.Lector["UNOMBRE"].ToString(),
                        IdMetodoPago = (int)datos.Lector["IDMETODO"],
                        MetodoNombre = datos.Lector["MPNOMBRE"].ToString(),
                        IdEstado = (int)datos.Lector["IDESTADO"],
                        EstadoNombre = datos.Lector["ENOMBRE"].ToString(),
                        FechaPedido = (DateTime)datos.Lector["FECHAPEDIDO"],
                        MontoTotal = (decimal)datos.Lector["MONTOTOTAL"]
                    };

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


        public void GuardarDetallePedido(int idPedido, List<ItemCarrito> prodcarrito)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (ItemCarrito item in prodcarrito)
                {
                    guardarDetallePedido(item, idPedido);
                }
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

        public void guardarDetallePedido(ItemCarrito item, int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO DETALLEPEDIDOS (IDPEDIDO, IDPRODUCTO, CANTIDAD, PRECIOUNITARIO) " +
                                        "VALUES (@IDPedido, @IDProducto, @Cantidad, @Precio)");
                datos.setearParametro("@IDPedido", idPedido);
                datos.setearParametro("@IDProducto", item.IdProducto);
                datos.setearParametro("@Cantidad", item.cantidad);
                datos.setearParametro("@Precio", item.art.Precio);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }


        }


        public void ActualizarStock(List<ItemCarrito> prodcarrito)
        {
            try
            {
                foreach (ItemCarrito item in prodcarrito)
                {
                    actualizarStock(item.IdProducto, item.cantidad);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void actualizarStock(int id, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Productos SET Stock = Stock - @Cantidad WHERE IDProducto = @IDProducto");
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@IDProducto", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void ActualizarEstadoPedido(int idPedido, int idEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Pedidos SET IdEstado = @IdEstado WHERE IdPedido = @IdPedido");
                datos.setearParametro("@IdEstado", idEstado);
                datos.setearParametro("@IdPedido", idPedido);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pedido> ListarPedidosConEstados()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta SQL que incluye los estados
                datos.setearConsulta(@"
            SELECT 
                P.IDPEDIDO, 
                P.IDUSUARIO, 
                U.NOMBRE AS UNOMBRE, 
                P.IDMETODO, 
                MP.NOMBRE AS MPNOMBRE, 
                P.IDESTADO, 
                E.NOMBRE AS ENOMBRE, 
                P.FECHAPEDIDO, 
                P.MONTOTOTAL
            FROM 
                PEDIDOS P
            INNER JOIN USUARIOS U ON P.IDUSUARIO = U.IDUSUARIO
            INNER JOIN METODODEPAGO MP ON P.IDMETODO = MP.IDMETODO
            INNER JOIN ESTADOS E ON P.IDESTADO = E.IDESTADO");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        IdPedido = (int)(long)(datos.Lector["IDPEDIDO"]),
                        IdUsuario = (int)(long)(datos.Lector["IDUSUARIO"]),
                        NombreUsuario = datos.Lector["UNOMBRE"].ToString(),
                        IdMetodoPago = (int)datos.Lector["IDMETODO"],
                        MetodoNombre = datos.Lector["MPNOMBRE"].ToString(),
                        IdEstado = (int)datos.Lector["IDESTADO"],
                        EstadoNombre = datos.Lector["ENOMBRE"].ToString(),
                        FechaPedido = (DateTime)datos.Lector["FECHAPEDIDO"],
                        MontoTotal = (decimal)datos.Lector["MONTOTOTAL"]
                    };

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
