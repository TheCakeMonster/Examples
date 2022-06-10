
CREATE TABLE [dbo].[Project] 
(
	[Id] int IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(1000),
	[Started] datetime2,
	[Ended] datetime2,
	[CreatedAt] datetime2 NOT NULL,
	[CreatedBy] nvarchar(100) NOT NULL,
	[UpdatedAt] datetime2 NOT NULL,
	[UpdatedBy] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED
	(
		[Id]
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_CreatedAt] DEFAULT(getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_CreatedBy] DEFAULT(suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_UpdatedAt] DEFAULT(getdate()) FOR [UpdatedAt]
GO

ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_UpdatedBy] DEFAULT(suser_sname()) FOR [UpdatedBy]
GO


