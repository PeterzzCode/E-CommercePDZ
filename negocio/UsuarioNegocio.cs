using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public Usuario Login(string email, string pass)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Id, Email, Pass, Nombre, Apellido, FechaNacimiento, ImagenPerfil, Admin FROM Usuario WHERE Email = '" + email + "' AND Pass = '" + pass + "'";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = (int)lector["Id"];
                    user.Email = lector["Email"].ToString();
                    user.Pass = lector["Pass"].ToString();
                    user.Nombre = lector["Nombre"].ToString();
                    user.Apellido = lector["Apellido"].ToString();
                    user.FechaNacimiento = (DateTime)lector["FechaNacimiento"];
                    user.ImagenPerfil = lector["ImagenPerfil"] is DBNull ? null : lector["ImagenPerfil"].ToString();
                    user.Admin = (Boolean)lector["Admin"];

                    return user;
                }

                return null;
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

        public void Registrar(Usuario nuevo)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "INSERT INTO Usuario (Email, Pass, Nombre, Apellido, FechaNacimiento, ImagenPerfil, Admin) " +
                                      "VALUES ('" + nuevo.Email + "', '" + nuevo.Pass + "', '" + nuevo.Nombre + "', '" + nuevo.Apellido + "', '" + nuevo.FechaNacimiento.ToString("yyyy-MM-dd") + "', '" + nuevo.ImagenPerfil + "', 0);";

                comando.Connection = conexion;
                conexion.Open();

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