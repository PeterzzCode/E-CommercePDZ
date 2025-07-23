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
    public partial class Producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idRemera = int.Parse(Request.QueryString["id"]);
                CargarDatosProducto(idRemera);
            }
        }

        private void CargarDatosProducto(int idRemera)
        {
            RemeraNegocio negocioRemera = new RemeraNegocio();
            StockNegocio negocioStock = new StockNegocio();

            Remera remera = negocioRemera.ObtenerRemeraPorId(idRemera);

            if (remera == null)
            {
                Response.Redirect("Catalogo.aspx");
                return;
            }

            lblId.Text = remera.Id.ToString();
            lblNombre.Text = remera.Nombre;
            lblDescripcion.Text = remera.Descripcion;
            lblPrecio.Text = remera.Precio.ToString("0.00");

            rptImagenes.DataSource = remera.UrlImagen;
            rptImagenes.DataBind();

            List<Color> colores = negocioStock.ObtenerColoresPorRemera(idRemera);
            ddlColor.DataSource = colores;
            ddlColor.DataTextField = "Descripcion";
            ddlColor.DataValueField = "Id";
            ddlColor.DataBind();

            List<Talle> talles = negocioStock.ObtenerTallesPorRemera(idRemera);
            ddlTalle.DataSource = talles;
            ddlTalle.DataTextField = "Descripcion";
            ddlTalle.DataValueField = "Id";
            ddlTalle.DataBind();

            ActualizarStock();
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStock();
        }

        protected void ddlTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStock();
        }

        private void ActualizarStock()
        {
            int idRemera = int.Parse(lblId.Text);
            int idColor = int.Parse(ddlColor.SelectedValue);
            int idTalle = int.Parse(ddlTalle.SelectedValue);

            StockNegocio negocioStock = new StockNegocio();
            int stock = negocioStock.ObtenerStock(idRemera, idColor, idTalle);

            lblStock.Text = stock.ToString();

            if (stock > 0)
            {
                txtCantidad.Text = "1";
            }
            else
            {
                txtCantidad.Text = "0";
            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtCantidad.Text);
            if (cantidad <= 0)
                return;

            int idRemera = int.Parse(lblId.Text);
            int idColor = int.Parse(ddlColor.SelectedValue);
            int idTalle = int.Parse(ddlTalle.SelectedValue);

            StockNegocio negocioStock = new StockNegocio();
            int stock = negocioStock.ObtenerStock(idRemera, idColor, idTalle);
            if (cantidad > stock)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertStock",
                "alert('No hay suficiente stock disponible para esta cantidad.');", true);
                return;
            }

            RemeraNegocio negocioRemera = new RemeraNegocio();
            Remera remera = negocioRemera.ObtenerRemeraPorId(idRemera);
            string imagenUrl = remera.UrlImagen != null && remera.UrlImagen.Count > 0 ? remera.UrlImagen[0].DescripcionUrlImagen : "";

            ItemCarrito item = new ItemCarrito();

            item.IdRemera = remera.Id;
            item.Nombre = remera.Nombre;
            item.ImagenUrl = imagenUrl;
            item.Precio = remera.Precio;
            item.IdColor = idColor;
            item.DescripcionColor = ddlColor.SelectedItem.Text;
            item.IdTalle = idTalle;
            item.DescripcionTalle = ddlTalle.SelectedItem.Text;
            item.Cantidad = cantidad;


            List<ItemCarrito> carrito = Session["carrito"] as List<ItemCarrito>;
            if (carrito == null)
                carrito = new List<ItemCarrito>();

            carrito.Add(item);
            Session["carrito"] = carrito;

            Response.Redirect("Carrito.aspx");
        }
    }
}