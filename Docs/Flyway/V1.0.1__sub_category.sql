USE [lexicon]
GO

CREATE TABLE [dbo].[sub_category](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO


/*
INSERT INTO [dbo].[sub_category]
(description)
VALUES
('Jobs')
GO
*/