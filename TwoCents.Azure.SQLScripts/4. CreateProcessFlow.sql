/****** Object:  Table [dbo].[ProcessFlow]    Script Date: 9/19/2015 8:54:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProcessFlow](
	[SequenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[Entity] [varchar](30) NULL,
	[EntityId] [varchar](50) NULL,
	[ProcessId] [bigint] NULL,
	[StepId] [bigint] NULL,
	[Sequence] [int] NULL,
	[Status] [varchar](15) NULL,
	[Notification] [varchar](15) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
        [IsLastStep] BIT NULL, 
 CONSTRAINT [PK_ProcessFlow] PRIMARY KEY CLUSTERED 
(
	[SequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


