<%@ Page Title="Mis Compras" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="E_CommercePDZ.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .venta-box {
            border: 1px solid #ccc;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mis Compras</h2>

    <asp:Panel ID="pnlSinCompras" runat="server" Visible="false">
        <div class="alert alert-info" role="alert">
            Aún no tenés compras registradas.
        </div>
    </asp:Panel>

    <asp:Repeater ID="rptMisCompras" runat="server" OnItemDataBound="rptMisCompras_ItemDataBound">
        <ItemTemplate>
            <div class="venta-box">
                <strong>Fecha:</strong> <%# Eval("Fecha", "{0:dd/MM/yyyy}") %><br />
                <strong>Total:</strong> $<%# Eval("Total") %><br />
                <strong>Estado:</strong> <%# Eval("Estado") %>

                <asp:Repeater ID="rptDetalles" runat="server">
                    <HeaderTemplate>
                        <table class="table mt-3">
                            <thead>
                                <tr>
                                    <th>Producto</th>
                                    <th>Cantidad</th>
                                    <th>Precio Unitario</th>
                                    <th>Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("NombreProducto") %></td>
                            <td><%# Eval("Cantidad") %></td>
                            <td>$<%# Eval("PrecioUnitario") %></td>
                            <td>$<%# Eval("Subtotal") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>