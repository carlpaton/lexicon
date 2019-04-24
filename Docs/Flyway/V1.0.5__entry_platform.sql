USE [lexicon]
GO

CREATE TABLE [dbo].[entry_platform](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[entry_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.entry(id),
	[platform_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.platform(id),
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO