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
	public partial class Carrito : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
                CargarCarrito();
        }

        private void CargarCarrito()
        {
            List<ItemCarrito> carrito = Session["carrito"] as List<ItemCarrito>;
            if (carrito == null)
                carrito = new List<ItemCarrito>();

            rptCarrito.DataSource = carrito;
            rptCarrito.DataBind();

            float total = 0;
            int i = 0;
            while (i < carrito.Count)
            {
                total += carrito[i].Subtotal;
                i++;
            }

            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }

        protected void btnSeguirComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Catalogo.aspx");
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemCarrito> carrito = Session["carrito"] as List<ItemCarrito>;
                if (carrito == null || carrito.Count == 0)
                {
                    Response.Redirect("Carrito.aspx");
                    return;
                }

                Usuario usuario = Session["usuario"] as Usuario;
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                Response.Redirect("Checkout.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}