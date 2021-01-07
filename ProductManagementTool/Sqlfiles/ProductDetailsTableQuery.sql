USE [ProductmanagmentDB]
GO

/****** Object:  Table [dbo].[ProductDetails]    Script Date: 07-01-2021 7.12.44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductDetails]') AND type in (N'U'))
DROP TABLE [dbo].[ProductDetails]
GO

/****** Object:  Table [dbo].[ProductDetails]    Script Date: 07-01-2021 7.12.44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductDetails](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Short_Description] [varchar](100) NOT NULL,
	[Long_Description] [varchar](255) NULL,
	[Small_Image_Path] [varchar](100) NOT NULL,
	[Large_Image_Path] [varchar](100) NULL,
 CONSTRAINT [PK_ProductDetails] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


