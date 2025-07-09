using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Remera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Talle Talle { get; set; }
        public Color Color { get; set; }
        public float Precio { get; set; }
        public string UrlImagen { get; set; }
    }
}
