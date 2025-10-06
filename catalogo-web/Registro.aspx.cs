using System;
using negocio;
using dominio;

namespace TPWebForm_equipo_16A

{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Voucher"] == null || Session["PremioId"] == null)
            { Response.Redirect("Voucher.aspx", false); }
        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            var dni = (txtDocumento.Text ?? "").Trim();
            if (dni.Length == 0) return;
            var cli = new ClienteNegocio().ObtenerPorDocumento(dni);
            if (cli != null)
            {
                txtNombre.Text = cli.Nombre;
                txtApellido.Text = cli.Apellido;
                txtEmail.Text = cli.Email;
                txtDireccion.Text = cli.Direccion;
                txtCiudad.Text = cli.Ciudad;
                txtCP.Text = cli.CP.ToString();
            }
        }

        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            // Validaciones simples
            if (!chkTyC.Checked) { ModelState.AddModelError("", "Debés aceptar TyC."); return; }
            if (string.IsNullOrWhiteSpace(txtDocumento.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            { ModelState.AddModelError("", "DNI y Email son obligatorios."); return; }

            var c = new Cliente
            {
                Documento = txtDocumento.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                CP = int.TryParse(txtCP.Text, out var cp) ? cp : 0
            };

            var cNeg = new ClienteNegocio();
            int idCliente = cNeg.Guardar(c);

            string codigo = (string)Session["Voucher"];
            int idArticulo = (int)Session["PremioId"];
            bool ok = new VoucherNegocio().Canjear(codigo, idCliente, idArticulo);

            if (!ok)
            {
                Session["error"] = "El voucher no está disponible (posible uso previo).";
                Response.Redirect("Error.aspx", false);
                return;
            }

            // Limpiar estado y redirigir a éxito
            Session.Remove("Voucher");
            Session.Remove("PremioId");
            Response.Redirect("Exito.aspx", false);
        }
    }
}
