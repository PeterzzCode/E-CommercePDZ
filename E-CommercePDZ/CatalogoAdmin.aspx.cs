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
	public partial class CatalogoAdmin : System.Web.UI.Page
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
                CargarRemeras();
                pnlAgregarEditar.Visible = false;
            }
        }

        public List<Stock> ListaStock
        {
            get
            {
                return Session["ListaStock"] != null ? (List<Stock>)Session["ListaStock"] : new List<Stock>();
            }
            set
            {
                Session["ListaStock"] = value;
            }
        }

        private void CargarRemeras()
        {
            RemeraNegocio negocio = new RemeraNegocio();
            List<Remera> lista = negocio.ListarAdmin();
            rptRemeras.DataSource = lista;
            rptRemeras.DataBind();
        }

        protected void rptRemeras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                CargarFormulario(id);
            }
        }

        private void CargarFormulario(int id)
        {
            RemeraNegocio negocio = new RemeraNegocio();
            Remera remera = negocio.ObtenerRemeraPorId(id);

            if (remera != null)
            {
                hfIdRemera.Value = remera.Id.ToString();
                txtNombre.Text = remera.Nombre;
                txtDescripcion.Text = remera.Descripcion;
                txtPrecio.Text = remera.Precio.ToString();
                chkActivo.Checked = remera.Activo;

                if (remera.UrlImagen != null && remera.UrlImagen.Count > 0)
                    txtUrlImagen.Text = remera.UrlImagen[0].DescripcionUrlImagen;
                else
                    txtUrlImagen.Text = "";

                CargarColorYTalle();

                pnlAgregarEditar.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                RemeraNegocio negocio = new RemeraNegocio();
                StockNegocio stockNegocio = new StockNegocio();
                Remera remera = new Remera();

                if (!string.IsNullOrEmpty(hfIdRemera.Value))
                    remera.Id = int.Parse(hfIdRemera.Value);
                else
                    remera.Id = 0;

                remera.Nombre = txtNombre.Text;
                remera.Descripcion = txtDescripcion.Text;
                remera.Precio = float.Parse(txtPrecio.Text);
                remera.Activo = chkActivo.Checked;

                remera.UrlImagen = new List<UrlImagen>()
        {
            new UrlImagen() { DescripcionUrlImagen = txtUrlImagen.Text }
        };

                int idRemeraGuardada;

                if (remera.Id == 0)
                {
                    idRemeraGuardada = negocio.Agregar(remera);
                    remera.Id = idRemeraGuardada;
                }
                else
                {
                    negocio.Modificar(remera);
                    idRemeraGuardada = remera.Id;
                }

                foreach (Stock s in ListaStock)
                {
                    s.IdRemera = idRemeraGuardada;
                    stockNegocio.GuardarStock(s);
                }

                pnlAgregarEditar.Visible = false;
                CargarRemeras();
                LimpiarFormulario();

                ListaStock = new List<Stock>();
                gvStock.DataSource = null;
                gvStock.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlAgregarEditar.Visible = false;
            LimpiarFormulario();
        }

        protected void btnNuevaRemera_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarColorYTalle();
            pnlAgregarEditar.Visible = true;
        }

        protected void CargarColorYTalle()
        {
            ColorNegocio colorNegocio = new ColorNegocio();
            ddlColor.DataSource = colorNegocio.Listar();
            ddlColor.DataTextField = "Descripcion";
            ddlColor.DataValueField = "Id";
            ddlColor.DataBind();

            TalleNegocio talleNegocio = new TalleNegocio();
            ddlTalle.DataSource = talleNegocio.Listar();
            ddlTalle.DataTextField = "Descripcion";
            ddlTalle.DataValueField = "Id";
            ddlTalle.DataBind();
        }
        protected void btnAgregarStock_Click(object sender, EventArgs e)
        {
            int idColor = int.Parse(ddlColor.SelectedValue);
            int idTalle = int.Parse(ddlTalle.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            List<Stock> stock = ListaStock;

            if (!stock.Any(s => s.IdColor == idColor && s.IdTalle == idTalle))
            {
                Stock nuevo = new Stock
                {
                    IdColor = idColor,
                    IdTalle = idTalle,
                    Cantidad = cantidad,
                    Color = new Color(idColor, ddlColor.SelectedItem.Text),
                    Talle = new Talle(idTalle, ddlTalle.SelectedItem.Text)
                };
                stock.Add(nuevo);
                ListaStock = stock;
                gvStock.DataSource = stock.Select(s => new { Color = s.Color.Descripcion, Talle = s.Talle.Descripcion, s.Cantidad });
                gvStock.DataBind();
            }
        }
        protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                List<Stock> stock = ListaStock;
                stock.RemoveAt(index);
                ListaStock = stock;

                gvStock.DataSource = stock.Select(s => new { Color = s.Color.Descripcion, Talle = s.Talle.Descripcion, s.Cantidad });
                gvStock.DataBind();
            }
        }
        private void LimpiarFormulario()
        {
            hfIdRemera.Value = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtUrlImagen.Text = "";
            chkActivo.Checked = false;
        }
    }
}