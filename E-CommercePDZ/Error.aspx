<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="E_CommercePDZ.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-5">
    <div class="alert alert-danger p-5 rounded-4 shadow">
        <h1 class="display-4">¡Ups! 😢</h1>
        <p class="lead mt-3">Hemos tenido un error.</p>
        <p>Le pedimos mil disculpas, estamos intentando solucionarlo lo antes posible.</p>
        <a href="Inicio.aspx" class="btn btn-dark mt-4">Volver al inicio</a>
    </div>
</div>
</asp:Content>
