USE [MvpConf2021]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 06/11/2021 16:26:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](60) NULL,
	[LocationCode] [nvarchar](5) NULL,
	[LocationPoint] [geography] NULL,
	[Polygon] [geography] NULL,
	[LocationType] [int] NOT NULL,
	[Latitude] [decimal](15, 9) NULL,
	[Longitude] [decimal](15, 9) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


