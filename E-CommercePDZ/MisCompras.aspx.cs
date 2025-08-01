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
    public partial class MisCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarMisCompras(usuario.Id);
            }
        }

        private void CargarMisCompras(int idUsuario)
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> compras = negocio.ListarVentasPorUsuario(idUsuario);

            if (compras != null && compras.Count > 0)
            {
                rptMisCompras.DataSource = compras;
                rptMisCompras.DataBind();

                pnlSinCompras.Visible = false;
            }
            else
            {
                rptMisCompras.DataSource = null;
                rptMisCompras.DataBind();

                pnlSinCompras.Visible = true;
            }
        }

        protected void rptMisCompras_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
                Venta venta = (Venta)e.Item.DataItem;
                Repeater rptDetalles = (Repeater)e.Item.FindControl("rptDetalles");
                rptDetalles.DataSource = venta.Detalles;
                rptDetalles.DataBind();
        }
    }
}