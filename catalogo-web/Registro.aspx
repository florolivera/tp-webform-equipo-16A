<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPWebForm_equipo_16A.Registro" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h3 class="mb-4">Completá tus datos</h3>
  <asp:ValidationSummary runat="server" CssClass="text-danger" />
  <div class="form-row">
    <div class="form-group col-md-4">
      <label>DNI</label>
      <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" MaxLength="20" />
    </div>
    <div class="form-group col-md-2 align-self-end">
      <asp:Button ID="btnBuscarDni" runat="server" Text="Buscar" CssClass="btn btn-secondary btn-block"
                  OnClick="btnBuscarDni_Click" />
    </div>
  </div>

  <div class="form-row">
    <div class="form-group col-md-4">
      <label>Nombre</label>
      <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group col-md-4">
      <label>Apellido</label>
      <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group col-md-4">
      <label>Email</label>
      <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
    </div>
  </div>

  <div class="form-row">
    <div class="form-group col-md-6">
      <label>Dirección</label>
      <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group col-md-4">
      <label>Ciudad</label>
      <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group col-md-2">
      <label>Código Postal</label>
      <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" TextMode="Number" />
    </div>
  </div>

  <div class="form-check mb-3">
    <asp:CheckBox ID="chkTyC" runat="server" CssClass="form-check-input" />
    <label class="form-check-label" for="chkTyC">Acepto Términos y Condiciones</label>
  </div>

  <asp:Button ID="btnParticipar" runat="server" CssClass="btn btn-primary"
              Text="¡Participar!"
              OnClick="btnParticipar_Click" />
</asp:Content>
