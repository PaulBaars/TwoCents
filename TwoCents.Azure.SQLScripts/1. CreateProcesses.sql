/****** Object:  Table [dbo].[Processes]    Script Date: 9/21/2015 12:49:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Processes](
	[ProcessId] [bigint] NOT NULL,
	[SourceSystem] [varchar](20) NULL,
	[MessageType] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[ResolveProcess] [bit] NULL,
 CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED 
(
	[ProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO








