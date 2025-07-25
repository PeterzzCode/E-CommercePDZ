using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace E_CommercePDZ
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblErrorEmail.Text = "";
            lblErrorPass.Text = "";
            lblErrorNombre.Text = "";
            lblErrorApellido.Text = "";
            lblErrorFecha.Text = "";

            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                lblErrorEmail.Text = "Ingrese un email válido.";
                esValido = false;
            }

            string pass = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(pass) && pass.Length < 6 && !pass.Any(char.IsUpper) && !pass.Any(char.IsLower) && !pass.Any(char.IsDigit))
            {
                lblErrorPass.Text = "La contraseña debe tener al menos 6 caracteres, una mayúscula, una minúscula y un número.";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblErrorNombre.Text = "El nombre es obligatorio.";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblErrorApellido.Text = "El apellido es obligatorio.";
                esValido = false;
            }

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                lblErrorFecha.Text = "Ingrese una fecha válida.";
                esValido = false;
            }

            if (!esValido)
                return;

                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.FechaNacimiento = fechaNacimiento;

                string nombreArchivo = System.IO.Path.GetFileName(fuImagenPerfil.FileName);
                string rutaRelativa = nombreArchivo;
                string rutaFisica = Server.MapPath(rutaRelativa);
                fuImagenPerfil.SaveAs(rutaFisica);
                usuario.ImagenPerfil = rutaRelativa;

                usuario.Admin = false;

                negocio.Registrar(usuario);

                Session["usuario"] = usuario;
                Response.Redirect("Inicio.aspx", false);
        }
    }
}