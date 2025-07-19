using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.Net;

namespace E_CommercePDZ
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string emailCliente = txtEmail.Text;
                string asunto = txtAsunto.Text;
                string comentario = txtComentario.Text;

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("pedroadominguez@hotmail.com");
                mensaje.To.Add(emailCliente);
                mensaje.Subject = "[PDZ Contacto] "+ asunto;
                mensaje.Body = "Buenas Somos PDZ, remitente " + emailCliente + " Nos Acercamos por el asunto que nos menciono: " + comentario + " a la brevedad nos estaremos poniendo en contacto con usted por este medio, este es un mensaje automatico, Cualquier duda respondemos de manera mas eficaz por WhatsApp al 1138454432! Muchas Gracias Por Comunicarse con Nosotros!";

                SmtpClient server = new SmtpClient();
                server.Credentials = new NetworkCredential("claushcraft2013@gmail.com", "idpk srck tmaz gzco");
                server.EnableSsl = true;
                server.Host= "smtp.gmail.com";
                server.Port = 587;

                server.Send(mensaje);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}