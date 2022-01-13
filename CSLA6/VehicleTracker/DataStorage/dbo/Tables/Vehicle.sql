
CREATE TABLE [dbo].[Vehicle] 
(
	[VehicleId] int IDENTITY(1,1) NOT NULL,
	[NickName] nvarchar(100) NOT NULL,
	[KeyReference] nvarchar(15) NOT NULL,
	[CreatedAt] datetime2 NOT NULL,
	[CreatedBy] varchar(100) NOT NULL,
	[UpdatedAt] datetime2 NOT NULL,
	[UpdatedBy] varchar(100) NOT NULL,
	CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED
	(
		[VehicleId]
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Vehicle] ADD  CONSTRAINT [DF_Vehicle_CreatedAt] DEFAULT(getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Vehicle] ADD  CONSTRAINT [DF_Vehicle_CreatedBy] DEFAULT(suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Vehicle] ADD  CONSTRAINT [DF_Vehicle_UpdatedAt] DEFAULT(getdate()) FOR [UpdatedAt]
GO

ALTER TABLE [dbo].[Vehicle] ADD  CONSTRAINT [DF_Vehicle_UpdatedBy] DEFAULT(suser_sname()) FOR [UpdatedBy]
GO


