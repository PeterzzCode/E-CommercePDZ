<%@ Page Title="Registro" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="E_CommercePDZ.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-6 mx-auto mt-5">
            <h2>Registro de Usuario</h2>

            <div class="mb-3">
                <label>Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                <asp:Label ID="lblErrorEmail" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            
            <div class="mb-3">
                <label>Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" />
                <asp:Label ID="lblErrorPass" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            
            <div class="mb-3">
                <label>Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                <asp:Label ID="lblErrorNombre" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            
            <div class="mb-3">
                <label>Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
                <asp:Label ID="lblErrorApellido" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            
            <div class="mb-3">
                <label>Fecha de Nacimiento</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtFechaNacimiento" TextMode="Date" />
                <asp:Label ID="lblErrorFecha" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            
            <div class="mb-3">
                <label>Imagen de Perfil</label>
                <asp:FileUpload ID="fuImagenPerfil" runat="server" CssClass="form-control" />
            </div>

            <asp:Button Text="Registrarse" CssClass="btn btn-dark" ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" />
        </div>
    </div>
</asp:Content>