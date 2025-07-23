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
                string consultaVenta = "INSERT INTO Venta (IdUsuario, NombreCliente, Fecha, Total, Estado) " +
                                       "OUTPUT INSERTED.Id " +
                                       "VALUES (" +
                                       venta.IdUsuario + ", '" +
                                       venta.NombreCliente + "', '" +
                                       venta.Fecha.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                       venta.Total.ToString().Replace(",", ".") + ", '" +
                                       venta.Estado + "')";

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
                string consulta = @"SELECT V.Id, V.IdUsuario, U.Nombre AS NombreCliente, V.Fecha, V.Total, V.Estado
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
                        Fecha = (DateTime)lector["Fecha"],
                        Total = (decimal)lector["Total"],
                        Estado = lector["Estado"].ToString()
                    };

                    lista.Add(venta);
                }

                lector.Close();
            }

            return lista;
        }
    }
}
