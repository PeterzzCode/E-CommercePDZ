<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="E_CommercePDZ.Contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5" style="max-width: 600px;">
        <h2 class="mb-4">Contacto</h2>

        <div class="form-floating mb-3">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="email@example.com" TextMode="Email"></asp:TextBox>
            <label for="txtEmail">Tu Email</label>
        </div>

        <div class="form-floating mb-3">
            <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control" placeholder="Asunto"></asp:TextBox>
            <label for="txtAsunto">Asunto</label>
        </div>

        <div class="form-floating mb-3">
            <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Comentario"></asp:TextBox>
            <label for="txtComentario">Comentario</label>
        </div>

        <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-dark" Text="Enviar mensaje" OnClick="btnEnviar_Click" />
    </div>
</asp:Content>