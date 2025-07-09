using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Talle
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public Talle() { }

        public Talle(int id, string desc)
        {
            Id = id;
            Descripcion = desc;
        }
    }
}
