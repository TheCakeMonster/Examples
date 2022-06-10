
CREATE PROCEDURE [dbo].[spAssignmentList_Fetch]

/**************************************************************************************************

	Description:		Fetch a list of Assignment records
	Author:				Andrew
	Created date:		10/06/22

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	10/06/22		Andrew					Created

***************************************************************************************************/

	@ProjectId Int

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Get all of the rows required
	SELECT
						[Assignment].[Id],
						[Assignment].[ProjectId],
						[Assignment].[ResourceId],
						[Assignment].[RoleId],
						[Assignment].[Assigned],
						[Assignment].[CreatedAt],
						[Assignment].[CreatedBy],
						[Assignment].[UpdatedAt],
						[Assignment].[UpdatedBy]
	FROM				[dbo].[Assignment]
	WHERE				[Assignment].[ProjectId] = @ProjectId

	-- Check for errors and handle appropriately
	SELECT @Error = @@ERROR, @RowsAffected = @@ROWCOUNT
	IF @Error <> 0 RETURN 1

	-- Return a value indicating success
	RETURN 0

END
GO

GRANT EXECUTE ON [dbo].[spAssignmentList_Fetch] TO [UIRights] AS [dbo]
GO
