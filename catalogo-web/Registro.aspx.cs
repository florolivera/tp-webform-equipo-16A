using dominio;
using negocio;
using System;
using System.Web.UI.WebControls;

namespace TPWebForm_equipo_16A
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Voucher"] == null || Session["PremioId"] == null)
            {
                Response.Redirect("Voucher.aspx", false);
                return;
            }
        }

        protected void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            LimpiarCamposCliente();
        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            string dni = (txtDocumento.Text ?? "").Trim();
            if (string.IsNullOrEmpty(dni))
            {
                CustomValidator cv = new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = "El DNI es obligatorio."
                };
                Page.Validators.Add(cv);
                return;
            }

            var cNeg = new ClienteNegocio();
            var cliente = cNeg.ObtenerPorDni(dni);

            if (cliente == null)
            {
                LimpiarCamposCliente(keepDni: true);
                return;
            }

            txtNombre.Text = cliente.Nombre ?? "";
            txtApellido.Text = cliente.Apellido ?? "";
            txtEmail.Text = cliente.Email ?? "";
            txtDireccion.Text = cliente.Direccion ?? "";
            txtCiudad.Text = cliente.Ciudad ?? "";
            txtCP.Text = cliente.CP > 0 ? cliente.CP.ToString() : "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string dni = (txtDocumento.Text ?? "").Trim();
            string nombre = (txtNombre.Text ?? "").Trim();
            string apellido = (txtApellido.Text ?? "").Trim();
            string email = (txtEmail.Text ?? "").Trim();

            if (string.IsNullOrEmpty(dni))
            {
                AddError("El DNI es obligatorio.");
                return;
            }
            if (string.IsNullOrEmpty(nombre))
            {
                AddError("El nombre es obligatorio.");
                return;
            }
            if (string.IsNullOrEmpty(apellido))
            {
                AddError("El apellido es obligatorio.");
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                AddError("El email es obligatorio.");
                return;
            }

            var cliente = new Cliente
            {
                Documento = dni,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Direccion = (txtDireccion.Text ?? "").Trim(),
                Ciudad = (txtCiudad.Text ?? "").Trim(),
                CP = int.TryParse(txtCP.Text, out var cp) ? cp : 0
            };

            var cNeg = new ClienteNegocio();
            int idCliente = cNeg.Guardar(cliente);

            string codigo = (string)Session["Voucher"];
            int idArticulo = (int)Session["PremioId"];

            var vNeg = new VoucherNegocio();

            var v = vNeg.Obtener(codigo);
            if (v == null)
            {
                Session["error"] = "El voucher no existe o ya fue utilizado.";
                Response.Redirect("Error.aspx", false);
                return;
            }

            bool ok = vNeg.RegistrarCanje(codigo, idCliente, idArticulo);
            if (!ok)
            {
                Session["error"] = "El voucher no está disponible (posible uso previo).";
                Response.Redirect("Error.aspx", false);
                return;
            }

            Session.Remove("Voucher");
            Session.Remove("PremioId");
            Response.Redirect("Exito.aspx", false);
        }

        private void LimpiarCamposCliente(bool keepDni = false)
        {
            if (!keepDni) txtDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtCP.Text = "";
        }

        private void AddError(string msg)
        {
            var cv = new System.Web.UI.WebControls.CustomValidator
            {
                IsValid = false,
                ErrorMessage = msg
            };
            Page.Validators.Add(cv);
        }
    }
}
