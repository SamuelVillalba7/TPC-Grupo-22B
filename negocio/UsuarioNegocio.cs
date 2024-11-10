using dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {
        public void agregarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT into USUARIOS (NOMBRE,APELLIDO,EMAIL,CONTRASEÑA,TELEFONO,ADMINISTRADOR) VALUES (@NOMBRE,@APELLIDO,@EMAIL,@CONTRASEÑA,@TELEFONO,0)");

                datos.setearParametro("@NOMBRE", usuario.Nombre);
                datos.setearParametro("@APELLIDO", usuario.Apellido);
                datos.setearParametro("@EMAIL", usuario.Email);
                datos.setearParametro("@CONTRASEÑA", usuario.Contraseña);
                datos.setearParametro("@TELEFONO", usuario.Telefono);
                datos.setearParametro("@ADMINISTRADOR", usuario.Administrador);
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



        public int buscarId(string email)
        {

            AccesoDatos datos=new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.IDUSUARIO from USUARIOS as U where U.EMAIL = @EMAIL");
                datos.setearParametro("@EMAIL", email);
                datos.ejecutarLectura();
                int id;
               if(datos.Lector.Read())
                {
                    id = (int)((long)datos.Lector["IDUSUARIO"]);
                }
                else
                {
                     id = 0;
                }

                return id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }


        public Usuario buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();
            try
            {
                datos.setearConsulta("SELECT U.IDUSUARIO, U.NOMBRE , U.APELLIDO , U.EMAIL , U.TELEFONO , U.CONTRASEÑA , U.ADMINISTRADOR FROM USUARIOS AS U WHERE U.IDUSUARIO = @ID ");
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)((long)datos.Lector["IDUSUARIO"]);
                    usuario.Nombre = (string)datos.Lector["NOMBRE"];
                    usuario.Apellido = (string)datos.Lector["APELLIDO"];
                    usuario.Email = (string)datos.Lector["EMAIL"];
                    usuario.Telefono = (string)datos.Lector["TELEFONO"];
                    usuario.Contraseña = (string)datos.Lector["CONTRASEÑA"];
                    usuario.Administrador = (bool)datos.Lector["ADMINISTRADOR"];


                }
                else
                {
                    usuario.Id = 0;
                }
                return usuario;



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
