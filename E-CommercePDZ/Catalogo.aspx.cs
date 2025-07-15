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
    public partial class Catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RemeraNegocio negocioRemera = new RemeraNegocio();
                List<Remera> listaRemeras = negocioRemera.ListarCatalogo();
                Session["listaRemeras"] = listaRemeras;

                if (Session["filtroBusqueda"] != null)
                {
                    string filtro = Session["filtroBusqueda"].ToString();

                    if (filtro != "")
                    {
                        List<Remera> listaFiltrada = new List<Remera>();
                        int j = 0;
                        while (j < listaRemeras.Count)
                        {
                            if (listaRemeras[j].Nombre.ToLower().Contains(filtro.ToLower()))
                            {
                                listaFiltrada.Add(listaRemeras[j]);
                            }
                            j++;
                        }
                        listaRemeras = listaFiltrada;
                    }

                    Session["filtroBusqueda"] = null;
                }

                rptRemeras.DataSource = listaRemeras;
                rptRemeras.DataBind();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Remera> lista = (List<Remera>)Session["listaRemeras"];

            string precioMinTexto = txtPrecioMin.Text;
            string precioMaxTexto = txtPrecioMax.Text;

            float precioMin = 0;
            float precioMax = 9999999;

            if (precioMinTexto != "")
                precioMin = float.Parse(precioMinTexto);

            if (precioMaxTexto != "")
                precioMax = float.Parse(precioMaxTexto);

            List<Remera> listaFiltrada = new List<Remera>();

            int i = 0;
            while (i < lista.Count)
            {
                Remera r = lista[i];

                if (r.Precio >= precioMin && r.Precio <= precioMax)
                {
                    listaFiltrada.Add(r);
                }

                i++;
            }

            rptRemeras.DataSource = listaFiltrada;
            rptRemeras.DataBind();
        }
    }
}