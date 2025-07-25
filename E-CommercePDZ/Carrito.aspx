<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="E_CommercePDZ.Carrito" MasterPageFile="~/MiMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2>Carrito de compras</h2>
        <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
            <ItemTemplate>
                <div class="card mb-2 p-3">
                    <div class="d-flex align-items-center">
                        <img src='<%# Eval("ImagenUrl") %>' alt="Imagen" style="width: 100px; height: 100px; object-fit: cover;" class="me-3" />
                        <div>
                            <h5><%# Eval("Nombre") %></h5>
                            <p>Color: <%# Eval("DescripcionColor") %> | Talle: <%# Eval("DescripcionTalle") %></p>
                            <p>Cantidad: <%# Eval("Cantidad") %></p>
                            <p>Subtotal: $<%# Eval("Subtotal", "{0:0.00}") %></p>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                            CommandName="Eliminar"
                            CommandArgument='<%# Eval("IdRemera") %>'
                            CssClass="btn btn-danger btn-sm mt-2" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold fs-5 d-block my-3" />

        <asp:Button ID="btnSeguirComprando" runat="server" Text="Seguir comprando" CssClass="btn btn-secondary" OnClick="btnSeguirComprando_Click" />
        <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar compra" CssClass="btn btn-success ms-2" OnClick="btnFinalizarCompra_Click" />
    </div>
</asp:Content>