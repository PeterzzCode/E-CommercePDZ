using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using acceso_datos;

namespace negocio
{
    public class RemeraNegocio
    {
        public List<Remera> listarConSP()
        {
            List<Remera> lista = new List<Remera>();
            Remera aux;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearSP("spListar");
                datos.ejecutarLector();
                while (datos.Lector.Read())
                {
                    aux = new Remera();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["DescripcionRemera"];
                    aux.Talle = new Talle();
                    aux.Talle.Id = (int)datos.Lector["IdTalle"];
                    aux.Talle.Descripcion = (string)datos.Lector["DescripcionTalle"];
                    aux.Color = new Color();
                    aux.Color.Id = (int)datos.Lector["IdColor"];
                    aux.Color.Descripcion = (string)datos.Lector["DescripcionColor"];
                    aux.Precio = (float)datos.Lector["Precio"];
                    aux.UrlImagen = (string)datos.Lector["UrlImagen"];

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
                datos = null;
            }
        }

    }
}
