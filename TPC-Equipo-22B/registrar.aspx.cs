using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;
namespace TPC_Equipo_22B
{
    public partial class registrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblRegistrar.Text = "Existen campos vacios.";
                return;
            }
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();



            usuario.Nombre= txtNombre.Text;
            usuario.Apellido= txtApellido.Text;
            usuario.Email= txtEmail.Text;   
            usuario.Contraseña=txtContraseña.Text;
            usuario.Telefono= txtTelefono.Text;
            usuario.Administrador = false;

            negocio.agregarUsuario(usuario);
        }
    }
}