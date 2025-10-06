<%@ Page Title="Ingresar voucher" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="catalogo_web.Voucher" %>

<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="row justify-content-center">
    <div class="col-md-6">
      <h3 class="mb-3">Ingresá el código de tu voucher</h3>

      <asp:ValidationSummary runat="server" CssClass="text-danger" ShowModelStateErrors="true" />

      <div class="form-group">
        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="50"
                     placeholder="Ej: ABC123XYZ"></asp:TextBox>
      </div>

      <asp:Button ID="btnSiguiente" runat="server" CssClass="btn btn-primary"
                  Text="Siguiente" OnClick="btnSiguiente_Click" />
    </div>
  </div>
</asp:Content>
