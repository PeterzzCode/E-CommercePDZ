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
    public class TalleNegocio
    {
        public List<Talle> Listar()
        {
            List<Talle> lista = new List<Talle>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("SELECT Id, Descripcion FROM Talle");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Talle talle = new Talle
                    {
                        Id = (int)datos.Lector["Id"],
                        Descripcion = datos.Lector["Descripcion"].ToString()
                    };
                    lista.Add(talle);
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
