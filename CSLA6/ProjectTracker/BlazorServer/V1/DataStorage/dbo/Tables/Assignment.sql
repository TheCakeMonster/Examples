
CREATE TABLE [dbo].[Assignment] 
(
	[Id] int IDENTITY(1,1) NOT NULL,
	[ProjectId] int NOT NULL,
	[ResourceId] int NOT NULL,
	[RoleId] int NOT NULL,
	[Assigned] datetime2 NOT NULL,
	[CreatedAt] datetime2 NOT NULL,
	[CreatedBy] nvarchar(100) NOT NULL,
	[UpdatedAt] datetime2 NOT NULL,
	[UpdatedBy] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED
	(
		[Id]
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Assignment] ADD  CONSTRAINT [DF_Assignment_CreatedAt] DEFAULT(getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Assignment] ADD  CONSTRAINT [DF_Assignment_CreatedBy] DEFAULT(suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Assignment] ADD  CONSTRAINT [DF_Assignment_UpdatedAt] DEFAULT(getdate()) FOR [UpdatedAt]
GO

ALTER TABLE [dbo].[Assignment] ADD  CONSTRAINT [DF_Assignment_UpdatedBy] DEFAULT(suser_sname()) FOR [UpdatedBy]
GO


