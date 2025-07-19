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
	public partial class VentasAdmin1 : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (usuario.Admin != true)
            {
                Response.Redirect("Error.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> ventas = negocio.ListarVentasConCliente();
            rptVentas.DataSource = ventas;
            rptVentas.DataBind();
        }

        protected void rptVentas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Venta venta = (Venta)e.Item.DataItem;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlEstados");
                ddl.SelectedValue = venta.Estado;
            }
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddl.NamingContainer;
            HiddenField hfIdVenta = (HiddenField)item.FindControl("hfIdVenta");

            int idVenta = int.Parse(hfIdVenta.Value);
            string nuevoEstado = ddl.SelectedValue;

            VentaNegocio negocio = new VentaNegocio();
            negocio.ActualizarEstadoVenta(idVenta, nuevoEstado);

            CargarVentas();
        }

    }
}