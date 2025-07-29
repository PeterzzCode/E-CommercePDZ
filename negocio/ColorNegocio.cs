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
        public List<Color> Listar()
        {
            List<Color> lista = new List<Color>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("SELECT Id, Descripcion FROM Color");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Color color = new Color
                    {
                        Id = (int)datos.Lector["Id"],
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };
                    lista.Add(color);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
