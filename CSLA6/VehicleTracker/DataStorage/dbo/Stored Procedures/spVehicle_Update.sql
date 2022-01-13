
CREATE PROCEDURE [dbo].[spVehicle_Update]

/**************************************************************************************************

	Description:		Update a single Vehicle record
	Author:				Andrew
	Created date:		05/12/21

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	05/12/21		Andrew					Created

***************************************************************************************************/

	@VehicleId AS int OUTPUT,
	@NickName AS nvarchar(100),
	@KeyReference AS nvarchar(15),
	@CreatedAt AS datetime2 OUTPUT,
	@CreatedBy AS varchar(100),
	@UpdatedAt AS datetime2 OUTPUT,
	@UpdatedBy AS varchar(100)

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
	UPDATE				[dbo].[Vehicle] 
	SET					[NickName] = @NickName,
						[KeyReference] = @KeyReference,
						[UpdatedAt] = @Now,
						[UpdatedBy] = @UpdatedBy
	WHERE				[Vehicle].[VehicleId] = @VehicleId
	AND					[Vehicle].[UpdatedAt] = @UpdatedAt
	
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

GRANT EXECUTE ON [dbo].[spVehicle_Update] TO [UIRights] AS [dbo]
GO
