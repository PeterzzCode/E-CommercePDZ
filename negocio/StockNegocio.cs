using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class StockNegocio
    {
        public List<Color> ObtenerColoresPorRemera(int idRemera)
        {
            List<Color> colores = new List<Color>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = "SELECT DISTINCT C.Id, C.Descripcion " +
                                  "FROM Stock S JOIN Color C ON S.IdColor = C.Id " +
                                  "WHERE S.IdRemera = " + idRemera;

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;

                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Color color = new Color();
                    color.Id = (int)lector["Id"];
                    color.Descripcion = lector["Descripcion"].ToString();

                    colores.Add(color);
                }

                return colores;
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



        public List<Talle> ObtenerTallesPorRemera(int idRemera)
        {
            List<Talle> talles = new List<Talle>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = "SELECT DISTINCT T.Id, T.Descripcion " +
                                  "FROM Stock S JOIN Talle T ON S.IdTalle = T.Id " +
                                  "WHERE S.IdRemera = " + idRemera;

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;

                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Talle talle = new Talle();
                    talle.Id = (int)lector["Id"];
                    talle.Descripcion = lector["Descripcion"].ToString();

                    talles.Add(talle);
                }

                return talles;
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



        public int ObtenerStock(int idRemera, int idColor, int idTalle)
        {
            int stock = 0;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector = null;

            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
                conexion.Open();

                string consulta = "SELECT Cantidad FROM Stock " +
                                  "WHERE IdRemera = " + idRemera +
                                  " AND IdColor = " + idColor +
                                  " AND IdTalle = " + idTalle;

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;

                lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    stock = (int)lector["Cantidad"];
                }

                return stock;
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


    }
}
