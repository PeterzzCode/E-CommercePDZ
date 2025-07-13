using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_CommercePDZ
{
	public partial class Catalogo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                RemeraNegocio negocio = new RemeraNegocio();
                List<Remera> lista = negocio.ListarCatalogo();

                Session["listaRemeras"] = lista;
                rptRemeras.DataSource = lista;
                rptRemeras.DataBind();
            }
        }
	}
}