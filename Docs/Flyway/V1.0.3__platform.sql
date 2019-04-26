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
('iOS'),
('Android'),
('Desktop'),
('iOS orange app'),
('iOS green app'),
('Apple watch')
GO
*/