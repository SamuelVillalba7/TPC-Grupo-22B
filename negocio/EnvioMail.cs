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

                        // Enviar el correo
                        smtp.Send(mail);
                    }
                }
                Console.WriteLine("Correo enviado con éxito.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
