<%@ Page Title="Catálogo Admin" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="CatalogoAdmin.aspx.cs" Inherits="E_CommercePDZ.CatalogoAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Catalogo Admin - Editar y Agregar</h2>

    <asp:Button ID="btnNuevaRemera" runat="server" Text="Agregar nueva remera" CssClass="btn btn-success mb-3" OnClick="btnNuevaRemera_Click" />

    <asp:Panel ID="pnlAgregarEditar" runat="server" CssClass="mb-4 border p-3">
        <asp:HiddenField ID="hfIdRemera" runat="server" />

        <div class="mb-3">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            <asp:Label ID="lblErrorNombre" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <div class="mb-3">
            <label for="txtDescripcion" class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" />
            <asp:Label ID="lblErrorDescripcion" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <div class="mb-3">
            <label for="txtPrecio" class="form-label">Precio</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
            <asp:Label ID="lblErrorPrecio" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <asp:Panel ID="pnlImagenes" runat="server" CssClass="mb-4">
            <h5>Imágenes</h5>
            <asp:Repeater ID="rptEditarImagenes" runat="server" OnItemCommand="rptEditarImagenes_ItemCommand">
                <ItemTemplate>
                    <div class="input-group mb-2">
                        <asp:TextBox ID="txtEditarUrlImagen" runat="server" CssClass="form-control"
                            ReadOnly="true" Text='<%# Eval("DescripcionUrlImagen") %>' />
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Container.ItemIndex %>' />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CommandName="Guardar" CommandArgument='<%# Container.ItemIndex %>' Visible="false" />
                        <asp:Button ID="btnEliminarImagen" runat="server" CssClass="btn btn-danger" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Container.ItemIndex %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="lblErrorImagenes" runat="server" CssClass="text-danger"></asp:Label>

            <asp:TextBox ID="txtNuevaUrlImagen" runat="server" CssClass="form-control mb-2" Placeholder="Nueva URL de imagen" />
            <asp:Button ID="btnAgregarImagen" runat="server" CssClass="btn btn-primary mb-3" Text="Agregar Imagen"
                OnClick="btnAgregarImagen_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlStock" runat="server" CssClass="mb-4">
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" placeholder="Cantidad" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnAgregarStock" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregarStock_Click" />
                </div>
            </div>
            <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="False" CssClass="table mt-3" OnRowCommand="gvStock_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Talle" HeaderText="Talle" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <div class="mb-3 form-check">
            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" />
            <label for="chkActivo" class="form-check-label">Activo</label>
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2" OnClick="btnCancelar_Click" />
    </asp:Panel>

    <hr />

    <div class="container" style="max-width: 1000px;">
        <div class="row">
            <asp:Repeater ID="rptRemeras" runat="server" OnItemCommand="rptRemeras_ItemCommand">
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

                                <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-warning" Text="Editar" />
                                <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-danger ms-2" Text="Eliminar" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>