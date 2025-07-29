<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="VentasAdmin.aspx.cs" Inherits="E_CommercePDZ.VentasAdmin1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rptVentas" runat="server" OnItemDataBound="rptVentas_ItemDataBound">
    <HeaderTemplate>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Id Venta</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Email</th>
                    <th>Fecha</th>
                    <th>Total</th>
                    <th>Estado</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Eval("Id") %></td>
            <td><%# Eval("NombreCliente") %></td>
            <td><%# Eval("ApellidoCliente") %></td>
            <td><%# Eval("EmailCliente") %></td>
            <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
            <td>$ <%# Eval("Total") %></td>
            <td><asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label></td>
            <td>
                <asp:HiddenField ID="hfIdVenta" runat="server" Value='<%# Eval("Id") %>' />
            
                <asp:Button ID="btnPendiente" runat="server" Text="Pendiente" CssClass="btn btn-outline-secondary btn-sm me-1 mb-1" OnClick="btnCambiarEstado_Click" CommandArgument="Pendiente" />
                <asp:Button ID="btnPagoRecibido" runat="server" Text="Pago recibido" CssClass="btn btn-outline-secondary btn-sm me-1 mb-1" OnClick="btnCambiarEstado_Click" CommandArgument="Pago recibido" />
                <asp:Button ID="btnEnviado" runat="server" Text="Enviado" CssClass="btn btn-outline-secondary btn-sm me-1 mb-1" OnClick="btnCambiarEstado_Click" CommandArgument="Enviado" />
                <asp:Button ID="btnCerrado" runat="server" Text="Cerrado" CssClass="btn btn-outline-secondary btn-sm me-1 mb-1" OnClick="btnCambiarEstado_Click" CommandArgument="Cerrado" />
            
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Pedido" CssClass="btn btn-outline-danger btn-sm ms-2" OnClick="btnCancelar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="8">
                <strong>Detalles:</strong>
                <asp:Repeater ID="rptDetalles" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr>
                                <th>Producto</th>
                                <th>Cantidad</th>
                                <th>Precio Unitario</th>
                                <th>Subtotal</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("NombreProducto") %></td>
                            <td><%# Eval("Cantidad") %></td>
                            <td>$ <%# Eval("PrecioUnitario") %></td>
                            <td>$ <%# Eval("Subtotal") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
    </asp:Repeater>
</asp:Content>
