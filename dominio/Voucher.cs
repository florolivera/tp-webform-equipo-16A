using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Voucher
    {
        public string CodigoVoucher { get; set; }
        public int? IdCliente { get; set; }
        public int? IdArticulo { get; set; }
        public DateTime? FechaCanje { get; set; }

        public bool Usado
        {
            get
            {
                return IdCliente.HasValue || IdArticulo.HasValue || FechaCanje.HasValue;
            }
        }

        public override string ToString()
        {
            return CodigoVoucher;
        }
    }
}
