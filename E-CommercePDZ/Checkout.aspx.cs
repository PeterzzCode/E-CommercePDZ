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

                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                Venta nuevaVenta = new Venta();
                nuevaVenta.IdUsuario = usuario.Id;
                nuevaVenta.Fecha = DateTime.Now;
                nuevaVenta.Estado = "Pendiente";
                
                decimal total = 0;
                foreach (ItemCarrito item in carrito)
                {
                    total += (decimal)item.Subtotal;
                }
                nuevaVenta.Total = total;
                
                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (ItemCarrito item in carrito)
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.IdProducto = item.IdRemera;
                    detalle.NombreProducto = item.Nombre;
                    detalle.Cantidad = item.Cantidad;
                    detalle.PrecioUnitario = (decimal)item.Precio;
                    detalle.Subtotal = (decimal)item.Subtotal;
                    detalles.Add(detalle);
                }
                nuevaVenta.Detalles = detalles;
                
                VentaNegocio negocio = new VentaNegocio();
                negocio.RegistrarVentaConDetalles(nuevaVenta, carrito);
                string asunto = "[PDZ] Confirmación de compra";
                string cuerpo = "¡Gracias por tu compra!\n\nTu pedido fue registrado exitosamente el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                    ".\n\nEstado: Pendiente\nTotal: $" + nuevaVenta.Total.ToString("0.00") +
                    "\n\nNos pondremos en contacto pronto. Si tenés dudas escribinos a WhatsApp 1138454432.";
                
                EmailUsuario.EnviarEmail(usuario.Email, asunto, cuerpo);
                Session["carrito"] = null;
                Response.Redirect("CompraExitosa.aspx", false);
            }
        }
    }