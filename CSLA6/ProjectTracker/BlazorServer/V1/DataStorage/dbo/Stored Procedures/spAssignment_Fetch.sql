
CREATE PROCEDURE [dbo].[spAssignment_Fetch]

/**************************************************************************************************

	Description:		Fetch a single Assignment
	Author:				Andrew
	Created date:		10/06/22

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	10/06/22		Andrew					Created

***************************************************************************************************/

	@AssignmentId AS int

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Get the data that is required, matching on the parameters provided
	SELECT
						[Assignment].[Id],
						[Assignment].[ResourceId],
						[Assignment].[RoleId],
						[Assignment].[Assigned],
						[Assignment].[CreatedAt],
						[Assignment].[CreatedBy],
						[Assignment].[UpdatedAt],
						[Assignment].[UpdatedBy]
	FROM				[dbo].[Assignment]
	WHERE				[Assignment].[Id] = @AssignmentId

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

GRANT EXECUTE ON [dbo].[spAssignment_Fetch] TO [UIRights] AS [dbo]
GO
