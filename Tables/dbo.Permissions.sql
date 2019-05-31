CREATE TABLE [dbo].[Permissions]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permissions] ADD CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
