using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using acceso_datos;

namespace negocio
{
    public class TalleNegocio
    {
        public List<Talle> listar()
        {
            Talle aux;
            AccesoDatos datos = new AccesoDatos();
            List<Talle> lista = new List<Talle>();
            try
            {

                datos.setearQuery("Select IdTalle, DescripcionTalle From TALLE");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    aux = new Talle((int)datos.Lector["IdTalle"], (string)datos.Lector["DescripcionTalle"]);
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
