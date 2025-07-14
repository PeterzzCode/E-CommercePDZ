using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace E_CommercePDZ
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario user = new Usuario();

            user.Email = txtEmail.Text;
            user.Pass = txtPassword.Text;

            Usuario logueado = negocio.Login(user.Email, user.Pass);

            if (logueado != null)
            {
                Session["usuario"] = logueado;
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}