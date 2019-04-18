USE [lexicon]
GO

CREATE TABLE [dbo].[lexicon_entry_type](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[lexicon_entry_type]
(description)
VALUES
('function'),
('recommendation'),
('notes')
GO