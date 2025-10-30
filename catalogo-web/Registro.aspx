<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPWebForm_equipo_16A.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Registro de Canje</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container my-4">

        <h2 class="mb-3">Registro de Canje</h2>

        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="text-danger mb-3" DisplayMode="List" ValidationGroup="vgGuardar" />

        <div class="form-group">
            <label for="txtDocumento">DNI</label>
            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control"
                         MaxLength="20" AutoPostBack="true" OnTextChanged="txtDocumento_TextChanged" />
            <small class="form-text text-muted">Ingresá el DNI y podés buscar si ya existe.</small>

            <asp:RequiredFieldValidator ID="rfvDni" runat="server"
                ControlToValidate="txtDocumento" ErrorMessage="El DNI es obligatorio."
                CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGuardar" />
        </div>

        <div class="form-group">
            <asp:Button ID="btnBuscarDni" runat="server" Text="Buscar DNI" CssClass="btn btn-secondary"
                        OnClick="btnBuscarDni_Click" CausesValidation="false" />
        </div>

        <hr />

        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio."
                CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGuardar" />
        </div>

        <div class="form-group">
            <label for="txtApellido">Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
            <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio."
                CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGuardar" />
        </div>

        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="120" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio."
                CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGuardar" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                ControlToValidate="txtEmail" ErrorMessage="El email no tiene formato valido."
                CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                ValidationGroup="vgGuardar" />
        </div>

        <div class="form-group">
            <label for="txtDireccion">Direccion</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" MaxLength="120" />
        </div>

        <div class="form-group">
            <label for="txtCiudad">Ciudad</label>
            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" MaxLength="80" />
        </div>

        <div class="form-group">
            <label for="txtCP">Codigo Postal</label>
            <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" MaxLength="10" />
        </div>

        <div class="form-group mt-3">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar y Canjear"
                        CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" ValidationGroup="vgGuardar" />
        </div>

    </form>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
