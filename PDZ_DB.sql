use master
go
create database PDZ_DB
go
use PDZ_DB
go
USE [PDZ_DB]
GO

Select * from Usuario

CREATE TABLE Venta (
    Id [int] IDENTITY(1,1) PRIMARY KEY,
    IdUsuario [int] NOT NULL,
	NombreCliente [varchar] (50),
    Fecha [datetime] NOT NULL,
    Total [decimal](18, 2) NOT NULL,
    Estado [nvarchar](50) NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);

CREATE TABLE DetalleVenta (
    Id [int] IDENTITY(1,1) PRIMARY KEY,
    IdVenta [int] NOT NULL,
    IdProducto [int] NOT NULL,
    NombreProducto [nvarchar](100) NOT NULL,
    Cantidad [int] NOT NULL,
    PrecioUnitario [decimal](18, 2) NOT NULL,
    Subtotal [decimal](18, 2) NOT NULL,
    FOREIGN KEY (IdVenta) REFERENCES Venta(Id),
    FOREIGN KEY (IdProducto) REFERENCES Remera(Id)
);

select * from Venta

CREATE TABLE [dbo].[Talle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Talle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

go

CREATE TABLE [dbo].[Color](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

go

Create TABLE [dbo].[Remera](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](400) NOT NULL,
	[Precio] [float] NOT NULL,
	[IdUrlImagen] [int] null,
	[Activo] [bit] null,
 CONSTRAINT [PK_Remera] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

go

create TABLE [dbo].[Usuario] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    Email [varchar](100) NOT NULL,
    Pass [varchar](100) NOT NULL,
    [Nombre] [varchar](50) NOT NULL,
    [Apellido] [varchar](50) NOT NULL,
    FechaNacimiento [date] NOT NULL,
    ImagenPerfil [varchar](255),
    Admin [bit] NOT NULL default 0,
CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

go

create TABLE [dbo].[Stock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRemera] [int] foreign key references Remera(Id) NOT NULL,
	[IdColor] [int] foreign key references Color(Id) NOT NULL,
	[IdTalle] [int] foreign key references Talle(Id) NOT NULL,
	[Cantidad] [int] NOT NULL
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)

go

CREATE TABLE [dbo].[UrlImagen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRemera] [int] foreign key references Remera(Id) NOT NULL,
	[UrlImagen] [varchar](500) NOT NULL
	CONSTRAINT [PK_UrlImagen] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)

go

INSERT INTO Talle (Descripcion) VALUES ('S');
INSERT INTO Talle (Descripcion) VALUES ('M');
INSERT INTO Talle (Descripcion) VALUES ('L');

go

INSERT INTO Color (Descripcion) VALUES ('Negro');
INSERT INTO Color (Descripcion) VALUES ('Blanco');

go

INSERT INTO Remera (Nombre, Descripcion, Precio, IdUrlImagen, Activo) VALUES 
('PDZ Curve', 'Remera oversize Curva ideal para todos los d�as.', 14999.99, 1, 1),
('PDZ Mangas', 'Estilo urbano de Mangas con dise�o.', 14999.99, 2, 1),
('PDZ Classic', 'Dise�o limpio y minimalista.', 13999.99, 3, 1),
('PDZ Fallen', 'Remera oversize con detalle gr�fico.', 13999.99, 4, 1),
('White Blessed', 'Remera oversize con detalle gr�fico.', 16999.99, 5, 1),
('PDZ Anxiety', 'Impresi�n g�tica Trasera estilo PDZ.', 15999.99, 6, 1),
('PDZ Curve 2', 'Inspirada en la cultura skater y el confort.', 15999.99, 7, 0);

go

DECLARE @idRemera INT = 1;

WHILE @idRemera <= 7
BEGIN
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 1, 10);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 2, 15);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 3, 12);

    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 1, 8);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 2, 14);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 3, 11);

    SET @idRemera = @idRemera + 1;
END

go

INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (1, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o1');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (1, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o1.1');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (2, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o2');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (2, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o2.2');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (3, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o3');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (3, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o3.3');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (4, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o4');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (4, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o4.4');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (5, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o5');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (5, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o5.5');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (6, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o6');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (6, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o6.6');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (7, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o7');
INSERT INTO UrlImagen(IdRemera, DescripcionUrlImagen) VALUES (7, 'C:\Users\pedro\Documents\GitHub\E-CommercePDZ\E-CommercePDZ\Images\Catalogo\Dise�o7.7');

SELECT 
    R.Id AS Id, 
    R.Nombre, 
    R.Precio, 
    U.Id AS IdUrlImagen, 
    U.DescripcionUrlImagen, 
    U.IdRemera 
FROM Remera R
JOIN UrlImagen U ON R.Id = U.IdRemera;

delete from Usuario
WHERE Email = 'cliente5@pdz.com';

Update Venta
Set IdEstado = 2
WHERE NombreCliente = 'Invitado';

INSERT INTO [dbo].[Usuario] (Email, Pass, Nombre, Apellido, FechaNacimiento, ImagenPerfil, Admin)
VALUES ('admin1@pdz.com', 'admin123', 'Pedro', 'Dominguez', '2001-06-27', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSjsl0WdP2j7uKpG3zG72nKchym-2xwXTApSQ&s', 1);

INSERT INTO [dbo].[Usuario] (Email, Pass, Nombre, Apellido, FechaNacimiento, ImagenPerfil, Admin)
VALUES ('cliente1@pdz.com', 'cliente123', 'Carlos', 'Gomez', '1995-09-15', 'https://http2.mlstatic.com/D_NQ_NP_924266-MLA86724494807_062025-O.webp', 0);

select * from Color