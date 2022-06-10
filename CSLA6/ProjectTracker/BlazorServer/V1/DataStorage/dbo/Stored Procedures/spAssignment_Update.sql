
CREATE PROCEDURE [dbo].[spAssignment_Update]

/**************************************************************************************************

	Description:		Update a single Assignment record
	Author:				Andrew
	Created date:		10/06/22

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	10/06/22		Andrew					Created

***************************************************************************************************/

	@Id AS int OUTPUT,
	@ProjectId AS int,
	@ResourceId AS int,
	@RoleId AS int,
	@Assigned AS datetime2,
	@CreatedAt AS datetime2 OUTPUT,
	@CreatedBy AS nvarchar(100),
	@UpdatedAt AS datetime2 OUTPUT,
	@UpdatedBy AS nvarchar(100)

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
DECLARE @Now DateTime2
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Get the current date and time for auditing purposes
	SET @Now = GETDATE()

    -- Insert the data provided
	UPDATE				[dbo].[Assignment] 
	SET					[ProjectId] = @ProjectId,
						[ResourceId] = @ResourceId,
						[RoleId] = @RoleId,
						[Assigned] = @Assigned,
						[UpdatedAt] = @Now,
						[UpdatedBy] = @UpdatedBy
	WHERE				[Assignment].[Id] = @Id
	AND					[Assignment].[UpdatedAt] = @UpdatedAt
	
	-- Check for errors and handle appropriately
	SELECT @Error = @@ERROR, @RowsAffected = @@ROWCOUNT
	IF @Error <> 0 RETURN 1
	IF @RowsAffected < 1
	BEGIN
		-- Concurrency error
		RETURN 3
	END
	IF @RowsAffected <> 1
	BEGIN
		RAISERROR('Incorrect number of rows affected; expected 1, actual %d', 11, 1, @RowsAffected)
		RETURN 2
	END

	-- Update the output parameters with the values to return to the object
	SELECT @UpdatedAt = @Now

	-- Return a value indicating success
	RETURN 0

END
GO

GRANT EXECUTE ON [dbo].[spAssignment_Update] TO [UIRights] AS [dbo]
GO
