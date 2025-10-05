// negocio/ClienteNegocio.cs
using System;
using negocio;
using dominio;
using Negocio;

namespace negocio
{
    public class ClienteNegocio
    {
        public Cliente ObtenerPorDocumento(string documento)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP
                                       FROM Clientes WHERE Documento = @doc");
                datos.setearParametro("@doc", documento);
                datos.ejecutarLectura();
                if (!datos.Lector.Read()) return null;

                return new Cliente
                {
                    Id = (int)datos.Lector["Id"],
                    Documento = (string)datos.Lector["Documento"],
                    Nombre = (string)datos.Lector["Nombre"],
                    Apellido = (string)datos.Lector["Apellido"],
                    Email = (string)datos.Lector["Email"],
                    Direccion = (string)datos.Lector["Direccion"],
                    Ciudad = (string)datos.Lector["Ciudad"],
                    CP = (int)datos.Lector["CP"]
                };
            }
            finally { datos.cerrarConexion(); }
        }

        /// Inserta si no existe (por Documento). Si existe, actualiza y devuelve Id.
        public int Guardar(Cliente c)
        {
            var existente = ObtenerPorDocumento(c.Documento);
            var datos = new AccesoDB();

            try
            {
                if (existente == null)
                {
                    datos.setearConsulta(@"
INSERT INTO Clientes(Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP)
OUTPUT INSERTED.Id
VALUES(@doc, @nom, @ape, @em, @dir, @ciu, @cp)");
                }
                else
                {
                    c.Id = existente.Id;
                    // patrón del profe: update + SELECT @id; para tener un escalar que leer
                    datos.setearConsulta(@"
UPDATE Clientes
   SET Nombre=@nom, Apellido=@ape, Email=@em, Direccion=@dir, Ciudad=@ciu, CP=@cp
 WHERE Id=@id;
SELECT @id;");
                    datos.setearParametro("@id", c.Id);
                }

                datos.setearParametro("@doc", c.Documento);
                datos.setearParametro("@nom", c.Nombre);
                datos.setearParametro("@ape", c.Apellido);
                datos.setearParametro("@em", c.Email);
                datos.setearParametro("@dir", c.Direccion);
                datos.setearParametro("@ciu", c.Ciudad);
                datos.setearParametro("@cp", c.CP);

                // Igual que en los ejemplos: leer el escalar con ejecutarLectura()/Lector.Read()
                datos.ejecutarLectura();                        // ← usa el “lector” como en el profe
                datos.Lector.Read();
                return Convert.ToInt32(datos.Lector[0]);        // ← Id devuelto
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
