USE [AGfotografia]
GO

/****** Object:  Table [dbo].[SobreMi]    Script Date: 23/06/2017 23:54:26 ******/
DROP TABLE [dbo].[SobreMi]
GO

/****** Object:  Table [dbo].[SobreMi]    Script Date: 23/06/2017 23:54:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SobreMi](
	[Portada] [varchar](max) NULL,
	[Titulo] [varchar](100) NULL,
	[Subtitulo] [varchar](100) NULL,
	[Texto1] [varchar](max) NULL,
	[Texto2] [varchar](max) NULL,
	[Texto3] [varchar](max) NULL
) 
GO
