<%@ Page Title="Compra Exitosa" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="CompraExitosa.aspx.cs" Inherits="E_CommercePDZ.CompraExitosa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .success-container {
            max-width: 600px;
            margin: 50px auto;
            text-align: center;
            padding: 40px;
            border-radius: 15px;
            background-color: #f8f9fa;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        }
        .success-container h1 {
            color: #28a745;
        }
        .btn-dark {
            margin-top: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="success-container">
        <h1>¡Gracias por tu compra!</h1>
        <p>Tu pedido fue procesado correctamente. Pronto recibirás una confirmación por email y te mantendremos informado del envío.</p>
        <asp:Button ID="btnVolverInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-dark" OnClick="btnVolverInicio_Click" />
    </div>
</asp:Content>
