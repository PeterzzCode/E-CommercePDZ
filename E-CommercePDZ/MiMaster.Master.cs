using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_CommercePDZ
{
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarContadorCarrito();
            }

            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];
                pnlLogueado.Visible = true;
                pnlNoLogueado.Visible = false;
                pnlAdminNavbar.Visible = user.Admin;

                lblUsuario.Text = "Hola, " + user.Nombre;

                if (!string.IsNullOrEmpty(user.ImagenPerfil))
                    imgPerfil.ImageUrl = user.ImagenPerfil;
                else
                    imgPerfil.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTKxYIQWAfUXUfl8B8Qb5bOszmQN04aUipVCQ&s";
            }
            else
            {
                pnlLogueado.Visible = false;
                pnlNoLogueado.Visible = true;
            }
        }

        private void ActualizarContadorCarrito()
        {
            int cantidad = 0;

            if (Session["carrito"] != null)
            {
                List<ItemCarrito> carrito = (List<ItemCarrito>)Session["carrito"];
                foreach (ItemCarrito item in carrito)
                {
                    cantidad += item.Cantidad;
                }
            }

            carritoCount.InnerText = cantidad.ToString();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Inicio.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["filtroBusqueda"] = txtBuscar.Text;
            Response.Redirect("Catalogo.aspx");
        }

    }
}