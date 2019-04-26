USE [lexicon]
GO

/* TODO ~ depending on the data, normalize `[recommendation]` & `[notes]` */

CREATE TABLE [dbo].[entry](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[category_id] [int] NOT NULL FOREIGN KEY REFERENCES dbo.category(id),
	[sub_category_id] [int] NOT NULL,
	[lexicon_function] [varchar](500) NULL,
	[recommendation] [varchar](500) NULL,
	[notes] [varchar](500) NULL
) ON [PRIMARY]
GO


/*
INSERT INTO [dbo].[entry]
(category_id,sub_category_id,lexicon_function,recommendation,notes)
VALUES
(1,0,'View category directory','All categories','Access to the third level tree links.')
GO
*/