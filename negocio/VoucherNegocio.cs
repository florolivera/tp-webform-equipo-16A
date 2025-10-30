using dominio;
using System;

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
WHERE CodigoVoucher = @cod;");
                datos.setearParametro("@cod", codigo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    var v = new Voucher();
                    v.CodigoVoucher = (string)datos.Lector["CodigoVoucher"];
                    v.IdCliente = datos.Lector["IdCliente"] is DBNull ? (int?)null : Convert.ToInt32(datos.Lector["IdCliente"]);
                    v.IdArticulo = datos.Lector["IdArticulo"] is DBNull ? (int?)null : Convert.ToInt32(datos.Lector["IdArticulo"]);
                    v.FechaCanje = datos.Lector["FechaCanje"] is DBNull ? (DateTime?)null : Convert.ToDateTime(datos.Lector["FechaCanje"]);
                    return v;
                }
                return null;
            }
            finally { datos.cerrarConexion(); }
        }

        public bool RegistrarCanje(string codigoVoucher, int idCliente, int idArticulo)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"
UPDATE Vouchers
   SET IdCliente = @cli,
       IdArticulo = @art,
       FechaCanje = GETDATE()
 WHERE CodigoVoucher = @cod
   AND IdCliente IS NULL
   AND IdArticulo IS NULL
   AND FechaCanje IS NULL;

SELECT @@ROWCOUNT AS filas;");
                datos.setearParametro("@cli", idCliente);
                datos.setearParametro("@art", idArticulo);
                datos.setearParametro("@cod", codigoVoucher);

                datos.ejecutarLectura();
                int filas = 0;
                if (datos.Lector.Read())
                    filas = Convert.ToInt32(datos.Lector["filas"]);

                return filas == 1;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
