<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="E_CommercePDZ.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .checkout-container { max-width: 700px; margin: auto; padding: 20px; }
        .checkout-card { padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="checkout-container">
        <div class="checkout-card bg-light">
            <h2>Finalizar compra</h2>
            <asp:Repeater ID="rptResumenCarrito" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Producto</th>
                                <th>Cantidad</th>
                                <th>Precio</th>
                                <th>Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Nombre") %></td>
                        <td><%# Eval("Cantidad") %></td>
                        <td>$<%# Eval("Precio") %></td>
                        <td>$<%# Eval("Subtotal") %></td>
                    </tr>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>

            <p class="fw-bold fs-5">Total: $<asp:Label ID="lblTotal" runat="server" /></p>

            <h4 class="mt-4">Confirmar datos</h4>
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />

            <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar compra" CssClass="btn btn-dark mt-3" OnClick="btnConfirmarCompra_Click" />
        </div>
    </div>
</asp:Content>