USE [AGfotografia]
GO

ALTER TABLE [dbo].[Fotos] DROP CONSTRAINT [FK_Fotos_Albunes]
GO

/****** Object:  Table [dbo].[Fotos]    Script Date: 23/06/2017 23:50:58 ******/
DROP TABLE [dbo].[Fotos]
GO

/****** Object:  Table [dbo].[Fotos]    Script Date: 23/06/2017 23:50:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fotos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SRC] [varchar](max) NULL,
	[ID_Album] [int] NOT NULL,
	[Miniatura] [varchar](max) NULL
) 
GO


CREATE INDEX Fotos_Idx
    ON dbo.Fotos
    (ID, ID_Album)
