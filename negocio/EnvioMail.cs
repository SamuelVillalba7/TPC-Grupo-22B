using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;


namespace negocio
{
    public class EnvioMail
    {
        private string smtpServer = "sandbox.smtp.mailtrap.io"; // Servidor SMTP de Mailtrap
        private int smtpPort = 2525; // Puerto SMTP proporcionado por Mailtrap
        private string emailUsername = "6df649ab153d03"; // Usuario de Mailtrap
        private string emailPassword = "f96fe19b808b9f"; // Contraseña de Mailtrap

        public void Enviar(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    // Configuración básica del correo
                    mail.From = new MailAddress("from@example.com", "Phlox Store"); // Dirección de envío ficticia
                    mail.To.Add(destinatario); // Dirección del destinatario
                    mail.Subject = asunto; // Asunto del correo
                    mail.Body = cuerpo; // Contenido del correo
                    mail.IsBodyHtml = true; // Permitir HTML en el correo (si es necesario)

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        // Configuración del cliente SMTP
                        smtp.Credentials = new NetworkCredential(emailUsername, emailPassword); // Credenciales de Mailtrap
                        smtp.EnableSsl = true; // Activar SSL para conexión segura

                        
                        smtp.Send(mail);
                    }
                }
                Console.WriteLine("Correo enviado con éxito");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo: " + ex.Message);
            }
        }

        public void EnviarArticulosDisponibles(string destinatario, List<dominio.Articulo> articulos)
        {
            try
            {
                // Construir el cuerpo del correo con los artículos disponibles
                StringBuilder cuerpo = new StringBuilder();
                cuerpo.Append("<h1>Artículos Disponibles en Phlox Store</h1>");
                cuerpo.Append("<p>Estos son los artículos actualmente disponibles en nuestra tienda:</p>");
                cuerpo.Append("<table style='width:100%; border-collapse:collapse;' border='1'>");
                cuerpo.Append("<thead><tr><th>Producto</th><th>Precio</th><th>Imagen</th></tr></thead>");
                cuerpo.Append("<tbody>");

                foreach (var articulo in articulos)
                {
                    cuerpo.Append($"<tr>");
                    cuerpo.Append($"<td>{articulo.Nombre}</td>");
                    cuerpo.Append($"<td>${articulo.Precio:N2}</td>");
                    cuerpo.Append($"<td><img src='{articulo.Imagen}' alt='{articulo.Nombre}' style='max-height:50px; max-width:50px;'/></td>");
                    cuerpo.Append($"</tr>");
                }

                cuerpo.Append("</tbody></table>");
                cuerpo.Append("<p>Gracias por suscribirte a nuestras novedades.</p>");

                // Enviar el correo
                Enviar(destinatario, "Artículos Disponibles en Phlox Store", cuerpo.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar los artículos disponibles: " + ex.Message);
            }
        }
    }


}
