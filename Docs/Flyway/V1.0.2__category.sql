USE [lexicon]
GO

CREATE TABLE [dbo].[category](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO

/*
INSERT INTO [dbo].[category]
(description)
VALUES
('Browse')
GO
*/