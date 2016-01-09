/****** Object:  Table [dbo].[ProcessDefinitions]    Script Date: 9/21/2015 12:46:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProcessDefinitions](
	[ProcessId] [bigint] NOT NULL,
	[StepId] [bigint] NOT NULL,
	[Sequence] [int] NULL,
	[Status] [varchar](15) NULL,
	[Notification] [varchar](10) NULL,
	[IsLastStep] BIT NULL, 
 CONSTRAINT [PK_ProcessFlows] PRIMARY KEY CLUSTERED 
(
	[ProcessId] ASC,
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProcessDefinitions]  WITH CHECK ADD  CONSTRAINT [FK_ProcessDefinitions_Processes] FOREIGN KEY([ProcessId])
REFERENCES [dbo].[Processes] ([ProcessId])
GO

ALTER TABLE [dbo].[ProcessDefinitions]  WITH CHECK ADD  CONSTRAINT [FK_ProcessDefinitions_ProcessSteps] FOREIGN KEY([StepId])
REFERENCES [dbo].[ProcessSteps] ([StepId])
GO


