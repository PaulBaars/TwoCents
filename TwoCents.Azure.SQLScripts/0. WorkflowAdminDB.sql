/****** Object:  Table [dbo].[ProcessFlow]    Script Date: 9/19/2015 8:54:57 AM ******/
DROP TABLE [dbo].[ProcessFlow]
GO

/****** Object:  Table [dbo].[ProcessFlowBU]    Script Date: 9/19/2015 8:54:57 AM ******/
DROP TABLE [dbo].[ProcessFlowBU]
GO

ALTER TABLE [dbo].[ProcessDefinitions] DROP CONSTRAINT [FK_ProcessDefinitions_ProcessSteps]
GO

ALTER TABLE [dbo].[ProcessDefinitions] DROP CONSTRAINT [FK_ProcessDefinitions_Processes]
GO

/****** Object:  Table [dbo].[ProcessDefinitions]    Script Date: 9/21/2015 12:46:40 AM ******/
DROP TABLE [dbo].[ProcessDefinitions]
GO

/****** Object:  Table [dbo].[Processes]    Script Date: 9/21/2015 12:49:04 AM ******/
DROP TABLE [dbo].[Processes]
GO

/****** Object:  Table [dbo].[ProcessSteps]    Script Date: 9/19/2015 8:55:23 AM ******/
DROP TABLE [dbo].[ProcessSteps]
GO

/****** Object:  Table [dbo].[Faults]    Script Date: 9/21/2015 12:43:16 AM ******/
DROP TABLE [dbo].[Faults]
GO

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

/****** Object:  Table [dbo].[ProcessSteps]    Script Date: 9/19/2015 8:55:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProcessSteps](
	[StepId] [bigint] NOT NULL,
	[Name] [varchar](30) NULL,
	[Version] [varchar](3) NULL,
 CONSTRAINT [PK_ProcessSteps] PRIMARY KEY CLUSTERED 
(
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

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

/****** Object:  Table [dbo].[ProcessFlowBU]    Script Date: 9/19/2015 8:54:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProcessFlowBU](
	[SequenceId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_ProcessFlowBU] PRIMARY KEY CLUSTERED 
(
	[SequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
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

