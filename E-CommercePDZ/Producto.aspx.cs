using dominio;
using negocio;
using System;
using System.Collections.Generic;

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
            {
                return;
            }

            int idRemera = int.Parse(lblId.Text);
            int idColor = int.Parse(ddlColor.SelectedValue);
            int idTalle = int.Parse(ddlTalle.SelectedValue);

            StockNegocio negocioStock = new StockNegocio();
            int stock = negocioStock.ObtenerStock(idRemera, idColor, idTalle);

            if (cantidad > stock)
            {
                return;
            }

            string url = $"Checkout.aspx?idRemera={idRemera}&idColor={idColor}&idTalle={idTalle}&cantidad={cantidad}";
            Response.Redirect(url);
        }
    }
}