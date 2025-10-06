<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPWebForm_equipo_16A.Error" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
  <div class="text-center">
    <h3 class="text-danger mb-3">Ups…</h3>
    <asp:Label ID="lblMsg" runat="server" CssClass="lead d-block mb-4"></asp:Label>
    <a class="btn btn-primary" href="./Voucher.aspx">Volver al inicio</a>
  </div>
</asp:Content>
