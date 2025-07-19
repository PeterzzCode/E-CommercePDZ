<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="VentasAdmin.aspx.cs" Inherits="E_CommercePDZ.VentasAdmin1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rptVentas" runat="server">
    <HeaderTemplate>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Id Venta</th>
                    <th>Cliente</th>
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
            <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
            <td>$ <%# Eval("Total") %></td>
            <td><asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlEstados" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                    <asp:ListItem Value="Pendiente" Text="Pendiente" />
                    <asp:ListItem Value="Pago recibido" Text="Pago recibido" />
                    <asp:ListItem Value="Enviado" Text="Enviado" />
                    <asp:ListItem Value="Cerrado" Text="Cerrado" />
                    <asp:ListItem Value="Cancelado" Text="Cancelado" />
                </asp:DropDownList>
                <asp:HiddenField ID="hfIdVenta" runat="server" Value='<%# Eval("Id") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>
