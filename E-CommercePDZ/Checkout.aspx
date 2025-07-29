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
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <p class="fw-bold fs-5">Total: $<asp:Label ID="lblTotal" runat="server" /></p>

            <h4 class="mt-4">Método de pago</h4>
            <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select mb-3">
                <asp:ListItem Text="Transferencia" Value="Transferencia" />
                <asp:ListItem Text="Efectivo" Value="Efectivo" />
            </asp:DropDownList>
            
            <h4>Envío</h4>
            <asp:RadioButtonList ID="rblEnvio" runat="server" CssClass="mb-3" AutoPostBack="true" OnSelectedIndexChanged="rblEnvio_SelectedIndexChanged">
                <asp:ListItem Text="Envío a domicilio" Value="envio" />
                <asp:ListItem Text="Retiro en tienda" Value="retiro" />
            </asp:RadioButtonList>
            
            <asp:Panel ID="pnlDireccion" runat="server" Visible="false" CssClass="mb-3">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingresá tu dirección completa"></asp:TextBox>
            </asp:Panel>
            
            <asp:Panel ID="pnlRetiro" runat="server" Visible="false" CssClass="mb-3 text-success">
                <p>Podés retirar tu compra en: Pilar, Calle 123</p>
            </asp:Panel>
            
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
            
            <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar compra" CssClass="btn btn-dark mt-3" OnClick="btnConfirmarCompra_Click" />

        </div>
    </div>
</asp:Content>