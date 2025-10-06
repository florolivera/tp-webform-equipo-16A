using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPWebForm_equipo_16A
{
    public partial class Premios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Voucher"] == null) { Response.Redirect("Voucher.aspx", false); return; }
            if (!IsPostBack) BindPremios();
            repPremios.ItemCommand += repPremios_ItemCommand;
        }

        private void BindPremios()
        {
            ArticuloNegocio art = new ArticuloNegocio();
            List<Articulo> arts = art.Listar();
            var imgNeg = new ImagenNegocio();

            // Proyectamos a un obj anónimo amigable al Repeater
            var modelo = arts.Select(a => new {
                a.Id,
                a.Nombre,
                a.Descripcion,
                ImagenUrl = imgNeg.ListarPorArticulo(a.Id).FirstOrDefault()?.ImagenUrl
            }).ToList();

            repPremios.DataSource = modelo;
            repPremios.DataBind();
        }

        protected void repPremios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Elegir")
            {
                int idArticulo = int.Parse((string)e.CommandArgument);
                Session["PremioId"] = idArticulo;
                Response.Redirect("Registro.aspx", false);
            }
        }
    }
}
