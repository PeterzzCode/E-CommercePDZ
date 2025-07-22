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
        public List<Remera> ListarAdmin()
        {
            List<Remera> lista = new List<Remera>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = " SELECT R.Id AS Id, R.Nombre, R.Descripcion, R.Precio, R.Activo, U.Id AS IdUrlImagen, U.DescripcionUrlImagen, U.IdRemera FROM Remera R JOIN UrlImagen U ON R.Id = U.IdRemera ORDER BY R.Id";

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
        public Remera ObtenerRemeraPorId(int idRemera)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = @"
                SELECT 
                R.Id, R.Nombre, R.Descripcion, R.Precio, R.Activo,
                U.Id AS IdUrlImagen, U.DescripcionUrlImagen, U.IdRemera
                FROM Remera R
                JOIN UrlImagen U ON R.Id = U.IdRemera
                WHERE R.Id = " + idRemera;

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;

                lector = comando.ExecuteReader();

                Remera remera = null;

                while (lector.Read())
                {
                    if (remera == null)
                    {
                        remera = new Remera();
                        remera.Id = (int)lector["Id"];
                        remera.Nombre = lector["Nombre"].ToString();
                        remera.Descripcion = lector["Descripcion"].ToString();
                        remera.Precio = float.Parse(lector["Precio"].ToString());
                        remera.Activo = bool.Parse(lector["Activo"].ToString());
                        remera.UrlImagen = new List<UrlImagen>();
                    }

                    UrlImagen imagen = new UrlImagen();
                    imagen.Id = (int)lector["IdUrlImagen"];
                    imagen.IdRemera = (int)lector["IdRemera"];
                    imagen.DescripcionUrlImagen = lector["DescripcionUrlImagen"].ToString();

                    remera.UrlImagen.Add(imagen);
                }

                return remera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (lector != null) lector.Close();
                conexion.Close();
            }
        }
        public void Agregar(Remera remera)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString);

            try
            {
                string consulta = "INSERT INTO Remera (Nombre, Descripcion, Precio, Activo) " +
                               "VALUES ('" + remera.Nombre + "', '" + remera.Descripcion + "', " + remera.Precio.ToString().Replace(',', '.') + ", " + (remera.Activo ? 1 : 0) + ")";

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                if (remera.UrlImagen != null && remera.UrlImagen.Count > 0)
                {
                    string descripcionImg = remera.UrlImagen[0].DescripcionUrlImagen;

                    if (descripcionImg != null && descripcionImg.Length > 0)
                    {
                        string consultaImg = "INSERT INTO UrlImagen (DescripcionUrlImagen, IdRemera) " +
                                          "VALUES ('" + descripcionImg + "', (SELECT MAX(Id) FROM Remera))";

                        SqlCommand cmdImg = new SqlCommand(consultaImg, conexion);
                        conexion.Open();
                        cmdImg.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                    conexion.Close();
            }
        }

        public void Modificar(Remera remera)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString);

            try
            {
                string consulta = "UPDATE Remera SET " +
                               "Nombre = '" + remera.Nombre + "', " +
                               "Descripcion = '" + remera.Descripcion + "', " +
                               "Precio = " + remera.Precio.ToString().Replace(',', '.') + ", " +
                               "Activo = " + (remera.Activo ? 1 : 0) + " " +
                               "WHERE Id = " + remera.Id;

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                if (remera.UrlImagen != null && remera.UrlImagen.Count > 0)
                {
                    string descripcionImg = remera.UrlImagen[0].DescripcionUrlImagen;

                    if (descripcionImg != null && descripcionImg.Length > 0)
                    {
                        string consultaImg = "UPDATE UrlImagen SET " +
                                          "DescripcionUrlImagen = '" + descripcionImg + "' " +
                                          "WHERE IdRemera = " + remera.Id;

                        SqlCommand cmdImg = new SqlCommand(consultaImg, conexion);
                        conexion.Open();
                        cmdImg.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                    conexion.Close();
            }
        }
    }
}
