using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Stock
    {
        public int Id { get; set; }
        public Remera Remera { get; set; }
        public Color Color { get; set; }
        public Talle Talle { get; set; }
        public int IdRemera { get; set; }
        public int IdColor { get; set; }
        public int IdTalle { get; set; }
        public int Cantidad { get; set; }
    }
}
