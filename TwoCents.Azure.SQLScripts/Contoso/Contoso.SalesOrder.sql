/****** Object:  Table [dbo].[Contoso.SalesOrders]    Script Date: 9/21/2015 12:43:16 AM ******/
DROP TABLE [dbo].[Contoso.SalesOrders]
GO

/****** Object:  Table [dbo].[Contoso.SalesOrders]    Script Date: 9/21/2015 12:43:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Contoso.SalesOrders](
	[EntityId] [varchar](50) NOT NULL,
	[Entity] [varchar](20) NOT NULL,
	[SourceSystem] [varchar](20) NOT NULL,
	[SalesOrderNumber] [varchar](20) NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [varchar](30) NULL,
	[Product] [varchar](30) NULL,
	[Category] [varchar](20) NULL,
	[OrderQuantity] [int] NULL,
	[OrderDate] [datetime] NULL,
 CONSTRAINT [PK_ServiceOrders] PRIMARY KEY CLUSTERED 
(
	[EntityID] ASC,
	[Entity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
