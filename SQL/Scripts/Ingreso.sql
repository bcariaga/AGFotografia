USE [AGfotografia]
GO

/****** Object:  Table [dbo].[Ingreso]    Script Date: 23/06/2017 23:52:12 ******/
DROP TABLE [dbo].[Ingreso]
GO

/****** Object:  Table [dbo].[Ingreso]    Script Date: 23/06/2017 23:52:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ingreso](
	[IngresoId] [int] IDENTITY(1,1) NOT NULL,
	[UserAgent] [varchar](2000) NOT NULL,
	[FechaIngreso] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IngresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE INDEX Ingreso_Idx
    ON dbo.Ingreso
    (FechaIngreso)


