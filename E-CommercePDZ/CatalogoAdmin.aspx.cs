using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (!usuario.Admin)
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

        public List<UrlImagen> ListaImagenes
        {
            get
            {
                return Session["ListaImagenes"] != null ? (List<UrlImagen>)Session["ListaImagenes"] : new List<UrlImagen>();
            }
            set
            {
                Session["ListaImagenes"] = value;
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
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = (int)e.CommandArgument;
                    RemeraNegocio negocio = new RemeraNegocio();

                    negocio.Eliminar(id);

                    CargarRemeras();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

                ListaImagenes = remera.UrlImagen ?? new List<UrlImagen>();
                rptEditarImagenes.DataSource = ListaImagenes;
                rptEditarImagenes.DataBind();

                CargarColorYTalle();

                pnlAgregarEditar.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblErrorNombre.Text = "";
            lblErrorDescripcion.Text = "";
            lblErrorPrecio.Text = "";
            lblErrorImagenes.Text = "";

            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblErrorNombre.Text = "El nombre es obligatorio.";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblErrorDescripcion.Text = "La descripción es obligatoria.";
                esValido = false;
            }

            float precio = 0;
            try
            {
                precio = float.Parse(txtPrecio.Text);

                if (precio <= 0)
                {
                    lblErrorPrecio.Text = "Ingrese un precio válido (mayor a 0).";
                    esValido = false;
                }
            }
            catch
            {
                lblErrorPrecio.Text = "Ingrese un precio numérico válido.";
                esValido = false;
            }

            List<UrlImagen> imagenes = new List<UrlImagen>();
            foreach (RepeaterItem item in rptEditarImagenes.Items)
            {
                TextBox txtUrl = item.FindControl("txtEditarUrlImagen") as TextBox;
                if (txtUrl != null && !string.IsNullOrWhiteSpace(txtUrl.Text))
                {
                    imagenes.Add(new UrlImagen { DescripcionUrlImagen = txtUrl.Text.Trim() });
                }
            }
            if (imagenes.Count == 0)
            {
                lblErrorImagenes.Text = "Debe haber al menos una imagen cargada.";
                esValido = false;
            }

            if (!esValido)
                return;

            try
            {
                RemeraNegocio negocio = new RemeraNegocio();
                StockNegocio stockNegocio = new StockNegocio();
                Remera remera = new Remera();

                if (!string.IsNullOrEmpty(hfIdRemera.Value))
                    remera.Id = int.Parse(hfIdRemera.Value);
                else
                    remera.Id = 0;

                remera.Nombre = txtNombre.Text.Trim();
                remera.Descripcion = txtDescripcion.Text.Trim();
                remera.Precio = precio;
                remera.Activo = chkActivo.Checked;
                remera.UrlImagen = imagenes;

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
                int index = (int)e.CommandArgument;
                List<Stock> stock = ListaStock;
                if (index >= 0)
                {
                    stock.RemoveAt(index);
                    ListaStock = stock;

                    gvStock.DataSource = stock.Select(s => new { Color = s.Color.Descripcion, Talle = s.Talle.Descripcion, s.Cantidad });
                    gvStock.DataBind();
                }
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaUrlImagen.Text))
            {
                if (ListaImagenes == null)
                    ListaImagenes = new List<UrlImagen>();

                ListaImagenes.Add(new UrlImagen { DescripcionUrlImagen = txtNuevaUrlImagen.Text.Trim() });

                rptEditarImagenes.DataSource = ListaImagenes.ToList(); 
                rptEditarImagenes.DataBind();

                txtNuevaUrlImagen.Text = "";
            }
        }

        protected void rptEditarImagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int index = (int)e.CommandArgument;

            if (e.CommandName == "Editar")
            {
                TextBox txtUrl = (TextBox)e.Item.FindControl("txtEditarUrlImagen");
                Button btnEditar = (Button)e.Item.FindControl("btnEditar");
                Button btnGuardar = (Button)e.Item.FindControl("btnGuardar");

                if (txtUrl != null && btnEditar != null && btnGuardar != null)
                {
                    txtUrl.ReadOnly = false;
                    btnEditar.Visible = false;
                    btnGuardar.Visible = true;
                }
            }
            else if (e.CommandName == "Guardar")
            {
                TextBox txtUrl = (TextBox)e.Item.FindControl("txtEditarUrlImagen");
                Button btnEditar = (Button)e.Item.FindControl("btnEditar");
                Button btnGuardar = (Button)e.Item.FindControl("btnGuardar");

                if (txtUrl != null && btnEditar != null && btnGuardar != null)
                {
                    List<UrlImagen> lista = ListaImagenes;

                    if (index >= 0 && index < lista.Count)
                    {
                        lista[index].DescripcionUrlImagen = txtUrl.Text.Trim();
                        ListaImagenes = lista;
                    }

                    txtUrl.ReadOnly = true;
                    btnEditar.Visible = true;
                    btnGuardar.Visible = false;

                    rptEditarImagenes.DataSource = ListaImagenes;
                    rptEditarImagenes.DataBind();
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                if (ListaImagenes != null && ListaImagenes.Count > index)
                {
                    ListaImagenes.RemoveAt(index);
                    rptEditarImagenes.DataSource = ListaImagenes;
                    rptEditarImagenes.DataBind();
                }
            }
        }

        private void LimpiarFormulario()
        {
            hfIdRemera.Value = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            chkActivo.Checked = false;

            ListaStock = new List<Stock>();
            gvStock.DataSource = null;
            gvStock.DataBind();

            ListaImagenes = new List<UrlImagen>();
            rptEditarImagenes.DataSource = null;
            rptEditarImagenes.DataBind();
        }
    }
}