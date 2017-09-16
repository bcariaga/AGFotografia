USE [AGfotografia]
GO

/****** Object:  Table [dbo].[Portadas]    Script Date: 23/06/2017 23:53:31 ******/
DROP TABLE [dbo].[Portadas]
GO

/****** Object:  Table [dbo].[Portadas]    Script Date: 23/06/2017 23:53:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Portadas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [varchar](50) NULL,
	[SRC] [varchar](max) NULL,
 CONSTRAINT [PK_Portadas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE INDEX Portadas_Idx
    ON dbo.Portadas
    (ID)
