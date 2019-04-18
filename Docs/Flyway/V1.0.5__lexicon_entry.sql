USE [lexicon]
GO

CREATE TABLE [dbo].[lexicon_entry](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[category_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.category(id),
	[platform_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.platform(id),
	[sub_category_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.sub_category(id),
	[lexicon_entry_type_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.lexicon_entry_type(id),
	[description] [varchar](50) NULL
) ON [PRIMARY]
GO