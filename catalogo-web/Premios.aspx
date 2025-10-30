<%@ Page Title="Elegir premio" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Premios.aspx.cs" Inherits="catalogo_web.Premios" %>

<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h3 class="mb-4">Elegí tu premio</h3>

  <style>
    .thumb { cursor: pointer; }
    .thumb.active { outline: 2px solid #0d6efd; }
  </style>

  <asp:Repeater ID="repPremios" runat="server" OnItemCommand="repPremios_ItemCommand">
    <ItemTemplate>
      <div class="card mb-3">
        <div class="row no-gutters">
          <!-- Columna de imágenes -->
          <div class="col-md-4 p-2 text-center">

            <!-- Imagen principal (id unico por item) -->
            <img id='main_<%# Container.ItemIndex %>'
                 class="img-fluid mb-2"
                 style="max-height:220px; object-fit:contain; width:100%;"
                 src='<%# Eval("ImagenUrl1") %>'
                 alt='<%# Eval("Nombre") %>' />

            <!-- Thumbs: hasta 3 (si una URL viene vacia, se oculta con style) -->
            <div class="d-flex justify-content-center flex-wrap">
              <img class="img-thumbnail m-1 thumb"
                   style='max-width:90px; max-height:90px; <%# string.IsNullOrEmpty(Eval("ImagenUrl1") as string) ? "display:none" : "" %>'
                   src='<%# Eval("ImagenUrl1") %>'
                   alt="thumb"
                   onclick="swapMain('main_<%# Container.ItemIndex %>', this)" />

              <img class="img-thumbnail m-1 thumb"
                   style='max-width:90px; max-height:90px; <%# string.IsNullOrEmpty(Eval("ImagenUrl2") as string) ? "display:none" : "" %>'
                   src='<%# Eval("ImagenUrl2") %>'
                   alt="thumb"
                   onclick="swapMain('main_<%# Container.ItemIndex %>', this)" />

              <img class="img-thumbnail m-1 thumb"
                   style='max-width:90px; max-height:90px; <%# string.IsNullOrEmpty(Eval("ImagenUrl3") as string) ? "display:none" : "" %>'
                   src='<%# Eval("ImagenUrl3") %>'
                   alt="thumb"
                   onclick="swapMain('main_<%# Container.ItemIndex %>', this)" />
            </div>
          </div>

          <!-- Columna texto + botón -->
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

  <!-- Mensaje cuando no hay premios -->
  <asp:Panel ID="pnlSinPremios" runat="server" Visible="false" CssClass="alert alert-info">
    No hay premios disponibles en este momento.
  </asp:Panel>

  <script>
    function swapMain(mainId, thumbEl) {
      var main = document.getElementById(mainId);
      if (!main) return;

      // Cambia la imagen grande
      main.src = thumbEl.src;

      // Marcar thumb activa (opcional, por feedback visual)
      var siblings = thumbEl.parentElement.querySelectorAll('.thumb');
      for (var i = 0; i < siblings.length; i++) siblings[i].classList.remove('active');
      thumbEl.classList.add('active');
    }
  </script>
</asp:Content>
