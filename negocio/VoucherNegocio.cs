using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VoucherNegocio
    {
        public Voucher Obtener(string codigo)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"
SELECT CodigoVoucher, IdCliente, IdArticulo, FechaCanje
  FROM Vouchers
 WHERE CodigoVoucher = @cod");
                datos.setearParametro("@cod", codigo);
                datos.ejecutarLectura();

                if (!datos.Lector.Read())
                    return null;

                return new Voucher
                {
                    CodigoVoucher = (string)datos.Lector["CodigoVoucher"],
                    IdCliente = datos.Lector["IdCliente"] is DBNull ? (int?)null : (int)datos.Lector["IdCliente"],
                    IdArticulo = datos.Lector["IdArticulo"] is DBNull ? (int?)null : (int)datos.Lector["IdArticulo"],
                    FechaCanje = datos.Lector["FechaCanje"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaCanje"]
                };
            }
            finally { datos.cerrarConexion(); }
        }

        /// <summary>
        /// Intenta canjear el voucher. Devuelve true si se marcó (no estaba usado).
        /// </summary>
        public bool Canjear(string codigoVoucher, int idCliente, int idArticulo)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"
UPDATE Vouchers
   SET IdCliente = @cli, IdArticulo = @art, FechaCanje = GETDATE()
 WHERE CodigoVoucher = @cod
   AND IdCliente IS NULL AND IdArticulo IS NULL AND FechaCanje IS NULL;
SELECT @@ROWCOUNT;");
                datos.setearParametro("@cli", idCliente);
                datos.setearParametro("@art", idArticulo);
                datos.setearParametro("@cod", codigoVoucher);

                //int filas = datos.ejecutarLectura(); // 1 => éxito; 0 => ya estaba usado o no existe
                return true;//filas == 1;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
