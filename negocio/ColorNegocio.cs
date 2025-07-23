using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using acceso_datos;
using System.Configuration;
using System.Data.SqlClient;

namespace negocio
{
    public class ColorNegocio
    {
        public List<Color> listar()
        {
            Color aux;
            List<Color> lista = new List<Color>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString);
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;
            try
            {

                comando.CommandText = "Select Id, Descripcion From COLOR";
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    aux = new Color((int)lector["Id"], (string)lector["Descripcion"]);
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lector.Close();
            }
        }
    }
}
