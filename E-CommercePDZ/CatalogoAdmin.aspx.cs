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

                pnlAgregarEditar.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                RemeraNegocio negocio = new RemeraNegocio();
                Remera remera = new Remera();

                if (hfIdRemera.Value != null && hfIdRemera.Value != "")
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

                if (remera.Id == 0)
                {
                    negocio.Agregar(remera);
                }
                else
                {
                    negocio.Modificar(remera);
                }

                pnlAgregarEditar.Visible = false;
                CargarRemeras();
                LimpiarFormulario();
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