using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_22B
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSuscribirse_Click(object sender, EventArgs e)
        {
            string email = txtEmailSuscripcion.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblSuscripcion.Text = "Por favor, ingresa un correo válido.";
                lblSuscripcion.CssClass = "text-danger";
                lblSuscripcion.Visible = true;
                return;
            }

            try
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                List<Articulo> articulosDisponibles = articuloNegocio.listarConSP(); // Método que devuelve la lista de artículos disponibles

                EnvioMail envioMail = new EnvioMail();
                envioMail.EnviarArticulosDisponibles(email, articulosDisponibles);

                lblSuscripcion.Text = "Te hemos enviado un correo con los artículos disponibles.";
                lblSuscripcion.CssClass = "text-success";
                lblSuscripcion.Visible = true;
            }
            catch (Exception ex)
            {
                lblSuscripcion.Text = "Hubo un error al enviar el correo. Intenta más tarde.";
                lblSuscripcion.CssClass = "text-danger";
                lblSuscripcion.Visible = true;

                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}