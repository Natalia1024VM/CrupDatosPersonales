create database PruebaCoink
GO

use PruebaCoink

GO
--tabla pais

CREATE TABLE [dbo].[Pais](
	[IdPais] [int] NOT NULL,
	[NombrePais] [varchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--tabla departamento
CREATE TABLE [dbo].[Departamento](
	[IdDepartamento] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[IdPais] [int] NOT NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Departamento]  WITH CHECK ADD  CONSTRAINT [FK_Departamento_Pais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[Pais] ([IdPais])
GO

ALTER TABLE [dbo].[Departamento] CHECK CONSTRAINT [FK_Departamento_Pais]
GO

-- tabla ciudad
CREATE TABLE [dbo].[Ciudad](
	[IdCiudad] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[IdPais] [int] NULL
) ON [PRIMARY]
GO
-- tabla persona

CREATE TABLE [dbo].[Persona](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[CodigoCiudad] [int] NOT NULL,
	[CodigoDepartamento] [int] NOT NULL,
	[CodigoPais] [int] NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

