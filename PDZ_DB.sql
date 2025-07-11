use master
go
create database PDZ_DB
go
use PDZ_DB
go
USE [PDZ_DB]
GO

Select * from Remera 

CREATE TABLE [dbo].[Talle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Talle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go

CREATE TABLE [dbo].[Color](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go

Create TABLE [dbo].[Stock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRemera] [int] foreign key references Remera(Id) NOT NULL,
	[IdColor] [int] foreign key references Color(Id) NOT NULL,
	[IdTalle] [int] foreign key references Talle(Id) NOT NULL,
	[Cantidad] [int] NOT NULL
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go

CREATE TABLE [dbo].[UrlImagen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRemera] [int] foreign key references Remera(Id) NOT NULL,
	[UrlImagen] [varchar](500) NOT NULL
	CONSTRAINT [PK_UrlImagen] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go

INSERT INTO Talle (Descripcion) VALUES ('S');
INSERT INTO Talle (Descripcion) VALUES ('M');
INSERT INTO Talle (Descripcion) VALUES ('L');

go

INSERT INTO Color (Descripcion) VALUES ('Negro');
INSERT INTO Color (Descripcion) VALUES ('Blanco');

go

INSERT INTO Remera (Nombre, Descripcion, Precio, IdUrlImagen, Activo) VALUES 
('PDZ Curve', 'Remera oversize Curva ideal para todos los días.', 14999.99, 1, 1),
('PDZ Mangas', 'Estilo urbano de Mangas con diseño.', 14999.99, 2, 1),
('PDZ Classic', 'Diseño limpio y minimalista.', 13999.99, 3, 1),
('PDZ Fallen', 'Remera oversize con detalle gráfico.', 13999.99, 4, 1),
('White Blessed', 'Remera oversize con detalle gráfico.', 16999.99, 5, 1),
('PDZ Anxiety', 'Impresión gótica Trasera estilo PDZ.', 15999.99, 6, 1),
('PDZ Curve 2', 'Inspirada en la cultura skater y el confort.', 15999.99, 7, 1);

go

DECLARE @idRemera INT = 1;

WHILE @idRemera <= 7
BEGIN
    -- Color 1 (Negro)
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 1, 10);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 2, 15);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 1, 3, 12);

    -- Color 2 (Blanco)
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 1, 8);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 2, 14);
    INSERT INTO Stock (IdRemera, IdColor, IdTalle, Cantidad) VALUES (@idRemera, 2, 3, 11);

    SET @idRemera = @idRemera + 1;
END

go

INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (1, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (1, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (2, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (2, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (3, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (3, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (4, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (4, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (5, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (5, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (6, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (6, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (7, '');
INSERT INTO UrlImagen(IdRemera, UrlImagen) VALUES (7, '');