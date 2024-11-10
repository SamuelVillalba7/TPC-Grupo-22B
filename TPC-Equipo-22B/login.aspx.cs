using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;


namespace TPC_Equipo_22B
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            int id = negocio.buscarId(txtEmail.Text);
            Usuario usuario = negocio.buscarPorId(id);
            if (usuario.Id != 0)
            {   
                if(usuario.Contraseña == txtContraseña.Text)
                {
                    //mandar al home y guardar al  usuario en la session
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx");
                }
                else
                {

                    txtEmail.Text = "CONTRASENIA INCORRECTA!!!";
                }
                
            }
        }
    }
}