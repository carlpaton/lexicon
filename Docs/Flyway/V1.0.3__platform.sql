USE [lexicon]
GO

CREATE TABLE [dbo].[platform](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO

/*
INSERT INTO [dbo].[platform]
(description)
VALUES
('FrEnd'),
('Classic'),
('iOS YAP'),
('iOS Green App'),
('Andriod YAP'),
('Andriod Green App')
GO
*/