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
        public int Agregar(Remera remera)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            int idRemeraInsertada = 0;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = @"INSERT INTO Remera (Nombre, Descripcion, Precio, Activo) 
                            VALUES (@nombre, @descripcion, @precio, @activo);
                            SELECT CAST(SCOPE_IDENTITY() AS int);";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                comando.Parameters.AddWithValue("@nombre", remera.Nombre);
                comando.Parameters.AddWithValue("@descripcion", remera.Descripcion);
                comando.Parameters.AddWithValue("@precio", remera.Precio);
                comando.Parameters.AddWithValue("@activo", remera.Activo ? 1 : 0);

                idRemeraInsertada = (int)comando.ExecuteScalar();

                if (remera.UrlImagen != null && remera.UrlImagen.Count > 0)
                {
                    foreach (UrlImagen img in remera.UrlImagen)
                    {
                        if (!string.IsNullOrEmpty(img.DescripcionUrlImagen))
                        {
                            SqlCommand comandoImg = new SqlCommand();
                            comandoImg.Connection = conexion;
                            comandoImg.CommandType = System.Data.CommandType.Text;
                            comandoImg.CommandText = "INSERT INTO UrlImagen (DescripcionUrlImagen, IdRemera) VALUES (@url, @idRemera)";
                            comandoImg.Parameters.AddWithValue("@url", img.DescripcionUrlImagen);
                            comandoImg.Parameters.AddWithValue("@idRemera", idRemeraInsertada);
                            comandoImg.ExecuteNonQuery();
                        }
                    }
                }

                return idRemeraInsertada;
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
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = @"UPDATE Remera SET 
                            Nombre = @nombre, 
                            Descripcion = @descripcion, 
                            Precio = @precio, 
                            Activo = @activo 
                            WHERE Id = @id";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                comando.Parameters.AddWithValue("@nombre", remera.Nombre);
                comando.Parameters.AddWithValue("@descripcion", remera.Descripcion);
                comando.Parameters.AddWithValue("@precio", remera.Precio);
                comando.Parameters.AddWithValue("@activo", remera.Activo ? 1 : 0);
                comando.Parameters.AddWithValue("@id", remera.Id);

                comando.ExecuteNonQuery();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM UrlImagen WHERE IdRemera = @idRemera", conexion);
                cmdDelete.Parameters.AddWithValue("@idRemera", remera.Id);
                cmdDelete.ExecuteNonQuery();

                if (remera.UrlImagen != null && remera.UrlImagen.Count > 0)
                {
                    foreach (UrlImagen img in remera.UrlImagen)
                    {
                        if (!string.IsNullOrEmpty(img.DescripcionUrlImagen))
                        {
                            SqlCommand comandoImg = new SqlCommand();
                            comandoImg.Connection = conexion;
                            comandoImg.CommandType = System.Data.CommandType.Text;
                            comandoImg.CommandText = "INSERT INTO UrlImagen (DescripcionUrlImagen, IdRemera) VALUES (@url, @idRemera)";
                            comandoImg.Parameters.AddWithValue("@url", img.DescripcionUrlImagen);
                            comandoImg.Parameters.AddWithValue("@idRemera", remera.Id);
                            comandoImg.ExecuteNonQuery();
                        }
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
        public void Eliminar(int idRemera)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = @"
                DELETE FROM Stock WHERE IdRemera = @idRemera;
                DELETE FROM UrlImagen WHERE IdRemera = @idRemera;
                DELETE FROM Remera WHERE Id = @idRemera;";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;

                comando.Parameters.AddWithValue("@idRemera", idRemera);
                comando.ExecuteNonQuery();
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
