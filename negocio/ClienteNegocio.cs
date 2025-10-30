using dominio;
using System;

namespace negocio
{
    public class ClienteNegocio
    {
        public Cliente ObtenerPorDni(string dni)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"
SELECT TOP 1 Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP
FROM Clientes
WHERE Documento = @dni;");
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    var c = new Cliente();
                    c.Id = Convert.ToInt32(datos.Lector["Id"]);
                    c.Documento = datos.Lector["Documento"]?.ToString();
                    c.Nombre = datos.Lector["Nombre"]?.ToString();
                    c.Apellido = datos.Lector["Apellido"]?.ToString();
                    c.Email = datos.Lector["Email"]?.ToString();
                    c.Direccion = datos.Lector["Direccion"]?.ToString();
                    c.Ciudad = datos.Lector["Ciudad"]?.ToString();
                    c.CP = datos.Lector["CP"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["CP"]);
                    return c;
                }
                return null;
            }
            finally { datos.cerrarConexion(); }
        }

        public int Guardar(Cliente c)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(@"
DECLARE @id INT;

SELECT @id = Id FROM Clientes WHERE Documento = @doc;

IF @id IS NULL
BEGIN
    INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP)
    VALUES (@doc, @nom, @ape, @mail, @dir, @ciu, @cp);
    SET @id = SCOPE_IDENTITY();
END
ELSE
BEGIN
    UPDATE Clientes
       SET Nombre = @nom,
           Apellido = @ape,
           Email = @mail,
           Direccion = @dir,
           Ciudad = @ciu,
           CP = @cp
     WHERE Id = @id;
END

SELECT @id AS Id;");
                datos.setearParametro("@doc", c.Documento ?? "");
                datos.setearParametro("@nom", c.Nombre ?? "");
                datos.setearParametro("@ape", c.Apellido ?? "");
                datos.setearParametro("@mail", c.Email ?? "");
                datos.setearParametro("@dir", c.Direccion ?? "");
                datos.setearParametro("@ciu", c.Ciudad ?? "");
                datos.setearParametro("@cp", c.CP);

                datos.ejecutarLectura();
                int id = 0;
                if (datos.Lector.Read())
                    id = Convert.ToInt32(datos.Lector["Id"]);
                return id;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
