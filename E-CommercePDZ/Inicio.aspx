<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="E_CommercePDZ.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <h2 class="titulo-blancas">Oversize Blancas</h2>
        </div>
        <div class="container text-center">
          <div class="row">
            <div class="col">
              <div class="div-remera-blanca">
                  <a href="Producto.aspx?id=4" class="a-remera-blanca">
                      <img src="/Images/RemeraBlanca1.jpg" class="remera-blanca" alt="Remera Blanca" />
                  </a>
              </div>
            </div>
            <div class="col">
              <div class="div-remera-blanca2">
                  <a href="Producto.aspx?id=3" class="a-remera-blanca2">
                      <img src="/Images/RemeraBlanca2.jpg" class="remera-blanca2" alt="Remera Blanca 2" />
                  </a>
              </div>
            </div>
          </div>
        </div>
        <div>
            <h2 class="titulo-negras">Oversize Negras</h2>
        </div>
        <div class="container text-center">
          <div class="row">
            <div class="col">
              <div class="div-remera-negra">
                  <a href="Producto.aspx?id=2" class="a-remera-negra">
                      <img src="/Images/RemeraNegra1.png" class="remera-negra" alt="Remera Negra" />
                  </a>
              </div>
            </div>
            <div class="col">
              <div class="div-remera-negra2">
                  <a href="Producto.aspx?id=5" class="a-remera-negra2">
                      <img src="/Images/RemeraNegra2.png" class="remera-negra2" alt="Remera Negra 2" />
                  </a>
              </div>
            </div>
          </div>
        </div>
        <div>
            <h2 class="titulo-nuevos">Nuevos Articulos</h2>
        </div>
        <div id="carouselExampleDark" class="carousel carousel-dark slide">
          <div class="carousel-inner">
        
            <div class="carousel-item active">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Producto.aspx?id=3">
                  <img src="/Images/RemeraBlanca2.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Blanca 2" />
                </a>
                <a href="Producto.aspx?id=3">
                  <img src="/Images/RemeraBlanca22.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Blanca 2" />
                </a>
              </div>
            </div>
        
            <div class="carousel-item">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Producto.aspx?id=2">
                  <img src="/Images/RemeraNegra1.png" class="img-fluid" style="max-height: 500px;" alt="Remera Negra" />
                </a>
                <a href="Producto.aspx?id=2">
                  <img src="/Images/RemeraNegra11.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Negra" />
                </a>
              </div>
            </div>
        
            <div class="carousel-item">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Producto.aspx?id=5">
                  <img src="/Images/RemeraNegra2.png" class="img-fluid" style="max-height: 500px;" alt="Remera Negra 2" />
                </a>
                <a href="Producto.aspx?id=5">
                  <img src="/Images/RemeraNegra22.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Negra 2" />
                </a>
              </div>
            </div>
        
          </div>
        
          <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Anterior</span>
          </button>
        
          <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Siguiente</span>
          </button>
        </div>
      </div>
    <footer>
        <div class="footer">
            <div class="redes-sociales">
                <div class="container text-center">
                  <div class="row">
                    <div class="col">
                        <a href="Inicio.aspx">
                           <img src="/Images/icon.png" class="logo-footer" alt="Logo" />
                        </a>
                    </div>
                    <div class="col">
                      <a href="Contacto.aspx">
                        <img src="https://static.vecteezy.com/system/resources/thumbnails/050/703/718/small/set-of-contact-us-button-hand-pointer-clicking-contact-us-web-buttons-png.png" class="contacto-footer" alt="Contacto" />
                      </a>
                    </div>
                    <div class="col">
                      <a href="https://w.app/pdz">
                        <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/WhatsApp_icon.png/500px-WhatsApp_icon.png" class="red" alt="WhatsApp" />
                      </a>
                      <a href="https://www.instagram.com/p.d.z__/">
                        <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a5/Instagram_icon.png/1200px-Instagram_icon.png" class="red" alt="Instagram" />
                      </a>
                      <a href="https://www.facebook.com/pedroix.ariel.dominguez">
                        <img src="https://images.vexels.com/content/223136/preview/facebook-icon-social-media-8dfafe.png" class="red" alt="Facebook" />
                      </a>
                    </div>
                  </div>
                </div>
            </div>
        </div>
    </footer>
</asp:Content>
