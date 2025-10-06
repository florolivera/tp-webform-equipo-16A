using System;
using negocio;

namespace catalogo_web
{
    public partial class Voucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("Voucher");
                Session.Remove("PremioId");
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            string codigo = (txtCodigo.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                // Mensaje simple sin necesitar controles extra
                ClientScript.RegisterStartupScript(
                    GetType(), "voucherEmpty", "alert('Ingresá un código.');", true);
                return;
            }

            var vn = new VoucherNegocio();
            var v = vn.Obtener(codigo);

            bool usado = v != null && (v.IdCliente.HasValue || v.IdArticulo.HasValue || v.FechaCanje.HasValue);
            if (v == null || usado)
            {
                Session["error"] = "El código es inválido o ya fue utilizado.";
                Response.Redirect("./Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            Session["Voucher"] = v.CodigoVoucher;
            Response.Redirect("./Premios.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
