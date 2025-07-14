<%@ Page Title="Registro" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="E_CommercePDZ.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-6 mx-auto mt-5">
            <h2>Registro de Usuario</h2>

            <div class="mb-3">
                <label>Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
            </div>

            <div class="mb-3">
                <label>Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" />
            </div>

            <div class="mb-3">
                <label>Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
            </div>

            <div class="mb-3">
                <label>Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
            </div>

            <div class="mb-3">
                <label>Fecha de Nacimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" />
            </div>

            <div class="mb-3">
                <label>URL Imagen de Perfil</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtImagenPerfil" />
            </div>

            <asp:Button Text="Registrarse" CssClass="btn btn-dark" ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" />
        </div>
    </div>
</asp:Content>