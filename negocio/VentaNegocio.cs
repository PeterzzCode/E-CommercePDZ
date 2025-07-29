using dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VentaNegocio
    {
        public void RegistrarVentaConDetalles(Venta venta, List<ItemCarrito> carrito)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString;
            conexion.Open();

            try
            {
                string consultaVenta = "INSERT INTO Venta (IdUsuario, Fecha, Total, Estado, MetodoPago, MetodoEnvio, DireccionEnvio) " +
                       "OUTPUT INSERTED.Id " +
                       "VALUES (" +
                       venta.IdUsuario + ", '" +
                       venta.Fecha.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                       venta.Total.ToString().Replace(",", ".") + ", '" +
                       venta.Estado + "', '" +
                       venta.MetodoPago + "', '" +
                       venta.MetodoEnvio + "', '" +
                       venta.DireccionEnvio + "')";

                SqlCommand comandoVenta = new SqlCommand();
                comandoVenta.Connection = conexion;
                comandoVenta.CommandType = System.Data.CommandType.Text;
                comandoVenta.CommandText = consultaVenta;

                int idVenta = (int)comandoVenta.ExecuteScalar();

                foreach (DetalleVenta detalle in venta.Detalles)
                {
                    string consultaDetalle = "INSERT INTO DetalleVenta (IdVenta, IdProducto, NombreProducto, Cantidad, PrecioUnitario, Subtotal) " +
                                             "VALUES (" +
                                             idVenta + ", " +
                                             detalle.IdProducto + ", '" +
                                             detalle.NombreProducto + "', " +
                                             detalle.Cantidad + ", " +
                                             detalle.PrecioUnitario.ToString().Replace(",", ".") + ", " +
                                             detalle.Subtotal.ToString().Replace(",", ".") + ")";

                    SqlCommand comandoDetalle = new SqlCommand();
                    comandoDetalle.Connection = conexion;
                    comandoDetalle.CommandType = System.Data.CommandType.Text;
                    comandoDetalle.CommandText = consultaDetalle;

                    comandoDetalle.ExecuteNonQuery();
                }

                foreach (ItemCarrito item in carrito)
                {
                    string consultaStock = "UPDATE Stock SET Cantidad = Cantidad - " + item.Cantidad +
                                           " WHERE IdRemera = " + item.IdRemera +
                                           " AND IdColor = " + item.IdColor +
                                           " AND IdTalle = " + item.IdTalle +
                                           " AND Cantidad >= " + item.Cantidad;

                    SqlCommand comandoStock = new SqlCommand();
                    comandoStock.Connection = conexion;
                    comandoStock.CommandType = System.Data.CommandType.Text;
                    comandoStock.CommandText = consultaStock;
                    comandoStock.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        public void ActualizarEstadoVenta(int idVenta, string nuevoEstado)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString))
            {
                SqlCommand comando = new SqlCommand();
                string consulta = "UPDATE Venta SET Estado = @Estado WHERE Id = @IdVenta";
                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                comando.Parameters.AddWithValue("@IdVenta", idVenta);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
        public List<Venta> ListarVentasConCliente()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString))
            {
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector = null;

                string consulta = @"SELECT V.Id, V.IdUsuario, U.Nombre AS NombreCliente, U.Apellido, U.Email, V.Fecha, V.Total, V.Estado
                            FROM Venta V
                            INNER JOIN Usuario U ON V.IdUsuario = U.Id";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = (int)lector["Id"],
                        IdUsuario = (int)lector["IdUsuario"],
                        NombreCliente = lector["NombreCliente"].ToString(),
                        ApellidoCliente = lector["Apellido"].ToString(),
                        EmailCliente = lector["Email"].ToString(),
                        Fecha = (DateTime)lector["Fecha"],
                        Total = (decimal)lector["Total"],
                        Estado = lector["Estado"].ToString()
                    };

                    venta.Detalles = ListarDetallesVenta(venta.Id);

                    lista.Add(venta);
                }

                lector.Close();
            }

            return lista;
        }
        public List<DetalleVenta> ListarDetallesVenta(int idVenta)
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString))
            {
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector = null;

                string consulta = @"SELECT D.Id, D.IdVenta, D.IdProducto, R.Nombre AS NombreProducto, D.Cantidad, D.PrecioUnitario
                            FROM DetalleVenta D
                            INNER JOIN Remera R ON D.IdProducto = R.Id
                            WHERE D.IdVenta = @idVenta";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Parameters.AddWithValue("@idVenta", idVenta);
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Id = (int)lector["Id"],
                        IdVenta = (int)lector["IdVenta"],
                        IdProducto = (int)lector["IdProducto"],
                        NombreProducto = lector["NombreProducto"].ToString(),
                        Cantidad = (int)lector["Cantidad"],
                        PrecioUnitario = (decimal)lector["PrecioUnitario"],
                        Subtotal = (decimal)lector["PrecioUnitario"] * (int)lector["Cantidad"]
                    };

                    detalles.Add(detalle);
                }

                lector.Close();
            }

            return detalles;
        }
        public List<Venta> ListarVentasPorUsuario(int idUsuario)
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString))
            {
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector = null;

                string consulta = @"SELECT V.Id, V.IdUsuario, V.NombreCliente, V.ApellidoCliente, V.EmailCliente,
                            V.Fecha, V.Total, V.Estado
                            FROM Venta V
                            WHERE V.IdUsuario = @idUsuario";

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = (int)lector["Id"],
                        IdUsuario = (int)lector["IdUsuario"],
                        NombreCliente = lector["NombreCliente"].ToString(),
                        ApellidoCliente = lector["ApellidoCliente"].ToString(),
                        EmailCliente = lector["EmailCliente"].ToString(),
                        Fecha = (DateTime)lector["Fecha"],
                        Total = (decimal)lector["Total"],
                        Estado = lector["Estado"].ToString(),
                        Detalles = ListarDetallesVenta((int)lector["Id"])
                    };

                    lista.Add(venta);
                }

                lector.Close();
            }

            return lista;
        }
        public Venta ObtenerVentaPorId(int idVenta)
        {
            Venta venta = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["PDZ_DB"].ConnectionString))
            {
                string consulta = @"SELECT V.Id, V.IdUsuario, U.Nombre, U.Apellido, U.Email, V.Fecha, V.Total, V.Estado
                            FROM Venta V
                            INNER JOIN Usuario U ON V.IdUsuario = U.Id
                            WHERE V.Id = @IdVenta";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdVenta", idVenta);
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    venta = new Venta()
                    {
                        Id = (int)lector["Id"],
                        IdUsuario = (int)lector["IdUsuario"],
                        NombreCliente = lector["Nombre"].ToString(),
                        ApellidoCliente = lector["Apellido"].ToString(),
                        EmailCliente = lector["Email"].ToString(),
                        Fecha = (DateTime)lector["Fecha"],
                        Total = (decimal)lector["Total"],
                        Estado = lector["Estado"].ToString()
                    };
                }
                lector.Close();
            }
            return venta;
        }
    }
}
