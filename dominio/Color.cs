using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Color
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Color() { }

        public Color(int id, string desc)
        {
            Id = id;
            Descripcion = desc;
        }
    }
}
