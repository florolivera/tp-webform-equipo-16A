using dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public partial class Premios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Voucher"] == null)
            {
                Response.Redirect("Voucher.aspx", false);
                return;
            }

            if (!IsPostBack)
                BindPremios();

            repPremios.ItemDataBound += repPremios_ItemDataBound;
        }

        private void BindPremios()
        {
            var artNeg = new ArticuloNegocio();
            var imgNeg = new ImagenNegocio();

            var articulos = artNeg.Listar();

            // Normalizador: si viene solo nombre de archivo, lo mapeamos a ~/imagenes/<archivo>
            Func<string, string> normalize = (raw) =>
            {
                if (string.IsNullOrWhiteSpace(raw)) return null;
                var u = raw.Trim();
                if (u.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    u.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                    u.StartsWith("/", StringComparison.Ordinal) ||
                    u.Contains("/"))
                {
                    return ResolveClientUrl(u);
                }
                // solo nombre -> carpeta del sitio
                return ResolveClientUrl("~/imagenes/" + u);
            };

            // Proyectamos hasta 3 URLs por producto al modelo del Repeater
            var modelo = new List<dynamic>();
            foreach (var a in articulos)
            {
                var imgs = imgNeg.ListarPorArticulo(a.Id) ?? new List<Imagen>();
                string u1 = imgs.Count > 0 ? normalize(imgs[0].ImagenUrl) : ResolveClientUrl("~/img/no-image.png");
                string u2 = imgs.Count > 1 ? normalize(imgs[1].ImagenUrl) : null;
                string u3 = imgs.Count > 2 ? normalize(imgs[2].ImagenUrl) : null;

                modelo.Add(new
                {
                    a.Id,
                    a.Nombre,
                    a.Descripcion,
                    ImagenUrl1 = u1,
                    ImagenUrl2 = u2,
                    ImagenUrl3 = u3
                });
            }

            repPremios.DataSource = modelo;
            repPremios.DataBind();

            pnlSinPremios.Visible = (modelo.Count == 0);
        }

        // Seteamos visibilidad/URL de los 3 thumbs en cada item
        protected void repPremios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            string u1 = DataBinder.Eval(e.Item.DataItem, "ImagenUrl1") as string;
            string u2 = DataBinder.Eval(e.Item.DataItem, "ImagenUrl2") as string;
            string u3 = DataBinder.Eval(e.Item.DataItem, "ImagenUrl3") as string;

            var imgA = (Image)e.Item.FindControl("imgThumbA");
            var imgB = (Image)e.Item.FindControl("imgThumbB");
            var imgC = (Image)e.Item.FindControl("imgThumbC");

            if (!string.IsNullOrEmpty(u1)) { imgA.ImageUrl = u1; imgA.Visible = true; } else imgA.Visible = false;
            if (!string.IsNullOrEmpty(u2)) { imgB.ImageUrl = u2; imgB.Visible = true; } else imgB.Visible = false;
            if (!string.IsNullOrEmpty(u3)) { imgC.ImageUrl = u3; imgC.Visible = true; } else imgC.Visible = false;
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

