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
        public partial class Checkout : System.Web.UI.Page
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
                {
                    CargarResumen();
                }
            }

            private void CargarResumen()
            {
                List<ItemCarrito> carrito = (List<ItemCarrito>)Session["carrito"];
                if (carrito == null || carrito.Count == 0)
                {
                    lblMensaje.Text = "Tu carrito está vacío.";
                    btnConfirmarCompra.Visible = false;
                    return;
                }

                rptResumenCarrito.DataSource = carrito;
                rptResumenCarrito.DataBind();

                float total = 0;
                foreach (ItemCarrito item in carrito)
                {
                    total += item.Subtotal;
                }

                lblTotal.Text = total.ToString("0.00");
            }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            List<ItemCarrito> carrito = (List<ItemCarrito>)Session["carrito"];
            Usuario usuario = (Usuario)Session["usuario"];

            if (rblEnvio.SelectedValue == "envio" && string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblMensaje.Text = "Por favor, ingresá tu dirección para el envío.";
                return;
            }

            Venta nuevaVenta = new Venta();
            nuevaVenta.IdUsuario = usuario.Id;
            nuevaVenta.Fecha = DateTime.Now;
            nuevaVenta.Estado = "Pendiente";
            nuevaVenta.MetodoPago = ddlMetodoPago.SelectedValue;
            nuevaVenta.MetodoEnvio = rblEnvio.SelectedValue;
            nuevaVenta.DireccionEnvio = (rblEnvio.SelectedValue == "envio") ? txtDireccion.Text.Trim() : "Retiro en tienda";

            decimal total = 0;
            List<DetalleVenta> detalles = new List<DetalleVenta>();
            foreach (ItemCarrito item in carrito)
            {
                total += (decimal)item.Subtotal;

                DetalleVenta detalle = new DetalleVenta();
                detalle.IdProducto = item.IdRemera;
                detalle.NombreProducto = item.Nombre;
                detalle.Cantidad = item.Cantidad;
                detalle.PrecioUnitario = (decimal)item.Precio;
                detalle.Subtotal = (decimal)item.Subtotal;
                detalles.Add(detalle);
            }
            nuevaVenta.Total = total;
            nuevaVenta.Detalles = detalles;

            VentaNegocio negocio = new VentaNegocio();
            negocio.RegistrarVentaConDetalles(nuevaVenta, carrito);

            string cuerpo = $"¡Gracias por tu compra!\n\n" +
                $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                $"Estado: Pendiente\nTotal: ${nuevaVenta.Total:0.00}\n\n" +
                $"Método de pago: {nuevaVenta.MetodoPago}\n" +
                $"Envío: {(nuevaVenta.MetodoEnvio == "envio" ? "A domicilio" : "Retiro en tienda")}\n" +
                $"Dirección: {nuevaVenta.DireccionEnvio}\n\n" +
                "Nos pondremos en contacto pronto. Si tenés dudas escribinos a WhatsApp 1138454432.";

            EmailUsuario.EnviarEmail(usuario.Email, "[PDZ] Confirmación de compra", cuerpo);
            Session["carrito"] = null;
            Response.Redirect("CompraExitosa.aspx", false);
        }
        protected void rblEnvio_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (rblEnvio.SelectedValue == "envio")
                {
                    pnlDireccion.Visible = true;
                    pnlRetiro.Visible = false;
                }
                else if (rblEnvio.SelectedValue == "retiro")
                {
                    pnlDireccion.Visible = false;
                    pnlRetiro.Visible = true;
                }
            }
        }

}