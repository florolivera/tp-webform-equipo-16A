using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public Marca() { }
        public Marca(int id, string descripcion)
        {
            this.Id = id;
            this.Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
