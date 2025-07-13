<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="E_CommercePDZ.Catalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="max-width: 1000px;">
    <div class="row">
        <asp:Repeater ID="rptRemeras" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card shadow-sm" style="min-height: 560px;">
                        <div id='carousel<%# Eval("Id") %>' class="carousel slide" data-bs-ride="carousel" style="height: 380px;">
                            <div class="carousel-inner" style="height: 380px;">
                                <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("UrlImagen") %>'>
                                    <ItemTemplate>
                                        <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>' style="height: 380px;">
                                            <img src='<%# Eval("DescripcionUrlImagen") %>' 
                                                 class="d-block w-100" 
                                                 style="width: 100%; height: 100%; object-fit: cover; border-radius: 6px;" 
                                                 alt="Imagen" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target='#carousel<%# Eval("Id") %>' data-bs-slide="prev">
                                <span class="carousel-control-prev-icon"></span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target='#carousel<%# Eval("Id") %>' data-bs-slide="next">
                                <span class="carousel-control-next-icon"></span>
                            </button>
                        </div>
                        <div class="card-body" style="padding: 10px;">
                            <h6 class="card-title"><%# Eval("Nombre") %></h6>
                            <p class="card-text" style="font-size: 14px;"><%# Eval("Descripcion") %></p>
                            <p class="fw-bold text-dark" style="font-size: 16px;">$<%# Eval("Precio") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</asp:Content>
