<%@ Page Title="Elegir premio" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Premios.aspx.cs" Inherits="catalogo_web.Premios" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h3 class="mb-4">Elegí tu premio</h3>
  <asp:Repeater ID="repPremios" runat="server">
    <ItemTemplate>
      <div class="card mb-3">
        <div class="row no-gutters">
          <div class="col-md-4">
            <img class="card-img" src='<%# Eval("ImagenUrl") %>' alt='<%# Eval("Nombre") %>' />
          </div>
          <div class="col-md-8">
            <div class="card-body">
              <h5 class="card-title"><%# Eval("Nombre") %></h5>
              <p class="card-text"><%# Eval("Descripcion") %></p>
              <asp:Button runat="server" CssClass="btn btn-primary"
                          Text="¡Quiero este!"
                          CommandName="Elegir"
                          CommandArgument='<%# Eval("Id") %>' />
            </div>
          </div>
        </div>
      </div>
    </ItemTemplate>
  </asp:Repeater>
</asp:Content>
