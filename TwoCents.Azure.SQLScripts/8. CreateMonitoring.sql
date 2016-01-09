/****** Object: Table [dbo].[Monitoring] Script Date: 11/23/2015 11:20:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Monitoring] (
    [ComponentType]  VARCHAR (30) NOT NULL,
    [Week]           INT          NOT NULL,
    [NumberOfOK]     INT          NULL,
    [NumberOfError]  INT	  NULL,
    [LastOccurrence] DATETIME     NULL,
    [Status]         VARCHAR (10) NULL,
    [LastCheck]      DATETIME     NULL, 
CONSTRAINT [PK_Monitoring] PRIMARY KEY CLUSTERED 
(
	[ComponentType] ASC,
	[Week] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


