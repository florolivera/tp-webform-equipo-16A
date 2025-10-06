using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        string consultaPrincipal = "SELECT Id, Descripcion FROM MARCAS ORDER BY Descripcion";
        string consultaAgregar = "INSERT INTO MARCAS (Descripcion) VALUES (@d)";
        string consultaModificar = "UPDATE MARCAS SET Descripcion=@d WHERE Id=@id";
        string consultaEliminar = "DELETE FROM MARCAS WHERE Id=@id";

        public List<Marca> Listar()
        {
            var lista = new List<Marca>();
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaPrincipal);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                    lista.Add(new Marca { Id = (int)datos.Lector["Id"], Descripcion = datos.Lector["Descripcion"] as string });
                return lista;
            }
            finally { datos.cerrarConexion(); }
        }

        public void Agregar(Marca m)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaAgregar);
                datos.setearParametro("@d", m.Descripcion);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }

        public void Modificar(Marca m)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaModificar);
                datos.setearParametro("@d", m.Descripcion);
                datos.setearParametro("@id", m.Id);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaEliminar);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
