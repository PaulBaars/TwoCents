/****** Object:  Table [dbo].[Faults]    Script Date: 9/21/2015 12:43:16 AM ******/
DROP TABLE [dbo].[Faults]
GO

/****** Object:  Table [dbo].[Faults]    Script Date: 9/21/2015 12:43:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Faults](
	[FaultId] bigint IDENTITY(1,1) NOT NULL,
	[ErrorType] [varchar](20) NULL,
	[Entity] [varchar](20) NULL,
	[EntityId] [varchar](50) NULL,
	[ErrorMessage] [varchar](max) NULL,
	[ErrorDetails] [varchar](max) NULL,
	[ErrorDate] [datetime] NULL,
 CONSTRAINT [PK_Faults] PRIMARY KEY CLUSTERED 
(
	[FaultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

