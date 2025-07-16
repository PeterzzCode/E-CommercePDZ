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
                mensaje.Body = "Email del cliente: " + emailCliente + "Mensaje: " + comentario;

                SmtpClient server = new SmtpClient();
                server.Credentials = new NetworkCredential("programationiii@gmail.com", "programacion3");
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