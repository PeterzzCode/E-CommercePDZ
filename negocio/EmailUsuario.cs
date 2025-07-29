using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace E_CommercePDZ
{
    public static class EmailUsuario
    {
        public static void EnviarEmail(string destinatario, string asunto, string cuerpo)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("pedroadominguez@hotmail.com");
            mensaje.To.Add(destinatario);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpo;

            SmtpClient servidor = new SmtpClient();
            servidor.Credentials = new NetworkCredential("claushcraft2013@gmail.com", "idpk srck tmaz gzco");
            servidor.EnableSsl = true;
            servidor.Host = "smtp.gmail.com";
            servidor.Port = 587;

            servidor.Send(mensaje);
        }
    }
}