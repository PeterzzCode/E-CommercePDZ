<%@ Page Title="Producto" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="E_CommercePDZ.Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6">
                <div id="carouselImagenes" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="rptImagenes" runat="server">
                            <ItemTemplate>
                                <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                    <img src='<%# Eval("DescripcionUrlImagen") %>' class="d-block w-100" style="height: 400px; object-fit: cover;" alt="Imagen" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselImagenes" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselImagenes" data-bs-slide="next">
                        <span class="carousel-control-next-icon"></span>
                    </button>
                </div>
            </div>

            <div class="col-md-6">
                <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                <h2 class="mb-3"><asp:Label ID="lblNombre" runat="server" /></h2>
                <p><asp:Label ID="lblDescripcion" runat="server" /></p>
                <h4 class="text-dark">Precio: $<asp:Label ID="lblPrecio" runat="server" /></h4>

                <div class="mb-3">
                    <label for="ddlColor" class="form-label">Color:</label>
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label for="ddlTalle" class="form-label">Talle:</label>
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTalle_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label">Stock disponible:</label>
                    <asp:Label ID="lblStock" runat="server" CssClass="fw-bold text-success" />
                </div>

                <div class="mb-3">
                    <label for="txtCantidad" class="form-label">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1" TextMode="Number" Min="1"></asp:TextBox>
                </div>

                <asp:Button ID="btnComprar" runat="server" CssClass="btn btn-dark" Text="Comprar" OnClick="btnComprar_Click" />
            </div>
        </div>
    </div>
</asp:Content>