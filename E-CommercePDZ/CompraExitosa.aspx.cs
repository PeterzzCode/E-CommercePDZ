using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_CommercePDZ
{
	public partial class CompraExitosa : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }
        protected void btnVolverInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}