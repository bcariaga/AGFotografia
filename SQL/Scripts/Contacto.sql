USE [AGfotografia]
GO

/****** Object:  Table [dbo].[Contacto]    Script Date: 23/06/2017 23:50:01 ******/
DROP TABLE [dbo].[Contacto]
GO

/****** Object:  Table [dbo].[Contacto]    Script Date: 23/06/2017 23:50:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacto](
	[Portada] [varchar](max) NULL,
	[Tel] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Facebook] [varchar](50) NULL,
	[Flickr] [varchar](50) NULL,
	[Titulo] [varchar](50) NULL,
	[Subtitulo] [varchar](50) NULL,
	[Texto1] [varchar](400) NULL,
	[Texto2] [varchar](400) NULL
)
GO




