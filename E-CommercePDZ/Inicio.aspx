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
                  <a href="Catalogo.aspx" class="a-remera-blanca">
                      <img src="/Images/RemeraBlanca1.jpg" class="remera-blanca" alt="Remera Blanca" />
                  </a>
              </div>
            </div>
            <div class="col">
              <div class="div-remera-blanca2">
                  <a href="Catalogo.aspx" class="a-remera-blanca2">
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
                  <a href="Catalogo.aspx" class="a-remera-negra">
                      <img src="/Images/RemeraNegra1.png" class="remera-negra" alt="Remera Negra" />
                  </a>
              </div>
            </div>
            <div class="col">
              <div class="div-remera-negra2">
                  <a href="Catalogo.aspx" class="a-remera-negra2">
                      <img src="/Images/RemeraNegra2.png" class="remera-negra2" alt="Remera Negra 2" />
                  </a>
              </div>
            </div>
          </div>
        </div>
        <div>
            <h2 class="titulo-nuevos-articulos">Nuevos Articulos</h2>
        </div>
        <div id="carouselExampleDark" class="carousel carousel-dark slide">
          <div class="carousel-inner">
        
            <div class="carousel-item active">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Catalogo.aspx">
                  <img src="/Images/RemeraBlanca2.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Blanca 2" />
                </a>
                <a href="Catalogo.aspx">
                  <img src="/Images/RemeraBlanca22.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Blanca 2" />
                </a>
              </div>
            </div>
        
            <div class="carousel-item">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Catalogo.aspx">
                  <img src="/Images/RemeraNegra1.png" class="img-fluid" style="max-height: 500px;" alt="Remera Negra" />
                </a>
                <a href="Catalogo.aspx">
                  <img src="/Images/RemeraNegra11.jpg" class="img-fluid" style="max-height: 500px;" alt="Remera Negra" />
                </a>
              </div>
            </div>
        
            <div class="carousel-item">
              <div class="d-flex justify-content-center align-items-center gap-4" style="min-height: 500px;">
                <a href="Catalogo.aspx">
                  <img src="/Images/RemeraNegra2.png" class="img-fluid" style="max-height: 500px;" alt="Remera Negra 2" />
                </a>
                <a href="Catalogo.aspx">
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

</asp:Content>
