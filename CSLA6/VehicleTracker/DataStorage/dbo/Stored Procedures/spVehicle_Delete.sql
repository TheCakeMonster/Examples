
CREATE PROCEDURE [dbo].[spVehicle_Delete]

/**************************************************************************************************

	Description:		Delete a single Vehicle record
	Author:				Andrew
	Created date:		05/12/21

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	05/12/21		Andrew					Created

***************************************************************************************************/

	@VehicleId AS int

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Delete the appropriate row
	DELETE				[dbo].[Vehicle] 
	WHERE				[Vehicle].[VehicleId] = @VehicleId

	-- Check for errors and handle appropriately
	SELECT @Error = @@ERROR, @RowsAffected = @@ROWCOUNT
	IF @Error <> 0 RETURN 1
	IF @RowsAffected <> 1
	BEGIN
		RAISERROR('Incorrect number of rows affected; expected 1, actual %d', 11, 1, @RowsAffected)
		RETURN 2
	END

	-- Return a value indicating success
	RETURN 0

END
GO

GRANT EXECUTE ON [dbo].[spVehicle_Delete] TO [UIRights] AS [dbo]
GO
