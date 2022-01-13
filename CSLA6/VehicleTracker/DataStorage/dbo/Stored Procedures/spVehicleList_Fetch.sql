
CREATE PROCEDURE [dbo].[spVehicleList_Fetch]

/**************************************************************************************************

	Description:		Fetch a list of Vehicle records
	Author:				Andrew
	Created date:		05/12/21

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	05/12/21		Andrew					Created

***************************************************************************************************/

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Get all of the rows required
	SELECT
						[Vehicle].[VehicleId],
						[Vehicle].[NickName],
						[Vehicle].[KeyReference],
						[Vehicle].[CreatedAt],
						[Vehicle].[CreatedBy],
						[Vehicle].[UpdatedAt],
						[Vehicle].[UpdatedBy]
	FROM				[dbo].[Vehicle]

	-- Check for errors and handle appropriately
	SELECT @Error = @@ERROR, @RowsAffected = @@ROWCOUNT
	IF @Error <> 0 RETURN 1

	-- Return a value indicating success
	RETURN 0

END
GO

GRANT EXECUTE ON [dbo].[spVehicleList_Fetch] TO [UIRights] AS [dbo]
GO
