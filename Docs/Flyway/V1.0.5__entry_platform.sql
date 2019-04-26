USE [lexicon]
GO

CREATE TABLE [dbo].[entry_platform](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[entry_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.entry(id),
	[platform_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.platform(id),
	[description] [varchar](500) NULL
) ON [PRIMARY]
GO


/*
INSERT INTO [dbo].[entry_platform]
(entry_id, platform_id, description)
VALUES
(1,2,'Browse categories'),
(1,3,'Browse categories'),
(1,4,'View category directory')
GO
*/