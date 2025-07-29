using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string MetodoEnvio { get; set; }
        public string DireccionEnvio { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }
}
