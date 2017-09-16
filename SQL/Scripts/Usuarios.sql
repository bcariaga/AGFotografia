USE [AGfotografia]
GO

--/****** Object:  Table [dbo].[Usuarios]    Script Date: 23/06/2017 23:41:37 ******/
--DROP TABLE [dbo].[Usuarios]
--GO

--/****** Object:  Table [dbo].[Usuarios]    Script Date: 23/06/2017 23:41:37 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[Usuarios](
--	[Usuario] [varchar](50) NOT NULL,
--	[Password] [varchar](512) NOT NULL,
--	[Desde] [datetime] NULL
--) 
--GO
ALTER TABLE dbo.Ususarios
	ADD Desde DATETIME NULL;

CREATE INDEX Usuarios_Idx
    ON dbo.Usuarios
    (Usuario, Password)

