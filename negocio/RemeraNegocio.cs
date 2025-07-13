using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using acceso_datos;
using System.Data.SqlClient;
using System.Configuration;

namespace negocio
{
    public class RemeraNegocio
    {
        public List<Remera> ListarCatalogo()
        {
            List<Remera> lista = new List<Remera>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = " SELECT R.Id AS Id, R.Nombre, R.Descripcion, R.Precio, R.Activo, U.Id AS IdUrlImagen, U.DescripcionUrlImagen, U.IdRemera FROM Remera R JOIN UrlImagen U ON R.Id = U.IdRemera WHERE R.Activo = 1 ORDER BY R.Id";

                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                int idAnterior = -1;
                Remera aux = null;

                while (lector.Read())
                {
                    int idActual = (int)lector["Id"];

                    if (idActual != idAnterior)
                    {
                        aux = new Remera();
                        aux.Id = idActual;
                        aux.Nombre = lector["Nombre"].ToString();
                        aux.Descripcion = lector["Descripcion"].ToString();
                        aux.Precio = float.Parse(lector["Precio"].ToString());
                        aux.Activo = bool.Parse(lector["Activo"].ToString());
                        aux.UrlImagen = new List<UrlImagen>();

                        lista.Add(aux);
                        idAnterior = idActual;
                    }

                    UrlImagen imagen = new UrlImagen();
                    imagen.Id = (int)lector["IdUrlImagen"];
                    imagen.IdRemera = (int)lector["IdRemera"];
                    imagen.DescripcionUrlImagen = lector["DescripcionUrlImagen"] is DBNull ? null : lector["DescripcionUrlImagen"].ToString();

                    aux.UrlImagen.Add(imagen);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
