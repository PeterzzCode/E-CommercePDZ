using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using acceso_datos;

namespace negocio
{
    public class ColorNegocio
    {
        public List<Color> listar()
        {
            Color aux;
            AccesoDatos datos = new AccesoDatos();
            List<Color> lista = new List<Color>();
            try
            {

                datos.setearQuery("Select IdColor, DescripcionColor From COLOR");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    aux = new Color((int)datos.Lector["IdColor"], (string)datos.Lector["DescripcionColor"]);
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
                datos.cerrarConexion();
            }
        }
    }
}
