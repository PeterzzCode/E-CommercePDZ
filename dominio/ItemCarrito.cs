using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemCarrito
    {
        public int IdRemera { get; set; }
        public string Nombre { get; set; }
        public string ImagenUrl { get; set; }
        public float Precio { get; set; }
        public int IdColor { get; set; }
        public string DescripcionColor { get; set; }
        public int IdTalle { get; set; }
        public string DescripcionTalle { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal => Precio * Cantidad;
    }
}
