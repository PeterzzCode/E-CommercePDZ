using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_CommercePDZ
{
    public partial class VentasAdmin1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
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

                HiddenField hfIdVenta = (HiddenField)e.Item.FindControl("hfIdVenta");

                Button btnPendiente = (Button)e.Item.FindControl("btnPendiente");
                Button btnPagoRecibido = (Button)e.Item.FindControl("btnPagoRecibido");
                Button btnEnviado = (Button)e.Item.FindControl("btnEnviado");
                Button btnCerrado = (Button)e.Item.FindControl("btnCerrado");
                Button btnCancelar = (Button)e.Item.FindControl("btnCancelar");

                string estadoActual = venta.Estado;

                btnPendiente.Enabled = false;
                btnPagoRecibido.Enabled = false;
                btnEnviado.Enabled = false;
                btnCerrado.Enabled = false;

                switch (estadoActual)
                {
                    case "Pendiente":
                        btnPendiente.CssClass += " active btn-primary";
                        btnPagoRecibido.Enabled = true;
                        break;
                    case "Pago recibido":
                        btnPagoRecibido.CssClass += " active btn-primary";
                        btnEnviado.Enabled = true;
                        break;
                    case "Enviado":
                        btnEnviado.CssClass += " active btn-primary";
                        btnCerrado.Enabled = true;
                        break;
                    case "Cerrado":
                        btnCerrado.CssClass += " active btn-primary";
                        break;
                    case "Cancelado":
                        btnPendiente.Enabled = false;
                        btnPagoRecibido.Enabled = false;
                        btnEnviado.Enabled = false;
                        btnCerrado.Enabled = false;
                        btnCancelar.Enabled = false;
                        break;
                }
                Repeater rptDetalles = (Repeater)e.Item.FindControl("rptDetalles");
                VentaNegocio negocio = new VentaNegocio();
                List<DetalleVenta> detalles = negocio.ListarDetallesVenta(venta.Id);
                rptDetalles.DataSource = detalles;
                rptDetalles.DataBind();
            }
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            HiddenField hfIdVenta = (HiddenField)item.FindControl("hfIdVenta");

            int idVenta = int.Parse(hfIdVenta.Value);
            string nuevoEstado = btn.CommandArgument;

            VentaNegocio negocio = new VentaNegocio();
            Venta ventaActualizada = negocio.ObtenerVentaPorId(idVenta);

            negocio.ActualizarEstadoVenta(idVenta, nuevoEstado);

            string emailCliente = ventaActualizada.EmailCliente;
            string nombreCliente = ventaActualizada.NombreCliente;

            string asunto = "[PDZ] Actualización de tu pedido";
            string cuerpo = $"Hola " + nombreCliente 
                + ",\n\nEl estado de tu pedido (Nº" + idVenta + ") ha sido actualizado a:" + nuevoEstado 
                + ".\n\nGracias por elegir PDZ.";
            EmailUsuario.EnviarEmail(emailCliente, asunto, cuerpo);

            CargarVentas();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            HiddenField hfIdVenta = (HiddenField)item.FindControl("hfIdVenta");

            int idVenta = int.Parse(hfIdVenta.Value);
            string nuevoEstado = "Cancelado";

            VentaNegocio negocio = new VentaNegocio();
            Venta ventaActualizada = negocio.ObtenerVentaPorId(idVenta);

            if (ventaActualizada.Estado != "Cancelado")
            {
                negocio.ActualizarEstadoVenta(idVenta, nuevoEstado);

                string emailCliente = ventaActualizada.EmailCliente;
                string nombreCliente = ventaActualizada.NombreCliente;

                string asunto = "[PDZ] Pedido cancelado";
                string cuerpo = $"Hola {nombreCliente},\n\nTu pedido (Nº {idVenta}) ha sido cancelado.\n\nSi tienes dudas, contactanos.";

                EmailUsuario.EnviarEmail(emailCliente, asunto, cuerpo);
            }

            CargarVentas();
        }

    }
}