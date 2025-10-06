using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PremiosNegocio
    {
        public List<Articulo> ListarConPrimeraImagen()
        {
            var arts = new ArticuloNegocio().Listar();
            var imgNeg = new ImagenNegocio();
            foreach (var a in arts)
                a.Imagenes = imgNeg.ListarPorArticulo(a.Id);
            return arts;
        }
    }
}
