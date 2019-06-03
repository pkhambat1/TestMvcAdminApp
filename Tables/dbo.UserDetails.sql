CREATE TABLE [dbo].[UserDetails]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[FirstName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Mobile] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserDetails] ADD CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
