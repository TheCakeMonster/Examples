
CREATE PROCEDURE [dbo].[spProjectList_Fetch]

/**************************************************************************************************

	Description:		Fetch a list of Project records
	Author:				Andrew
	Created date:		10/06/22

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	10/06/22		Andrew					Created

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
						[Project].[Id],
						[Project].[Name],
						[Project].[Description],
						[Project].[Started],
						[Project].[Ended],
						[Project].[CreatedAt],
						[Project].[CreatedBy],
						[Project].[UpdatedAt],
						[Project].[UpdatedBy]
	FROM				[dbo].[Project]

	-- Check for errors and handle appropriately
	SELECT @Error = @@ERROR, @RowsAffected = @@ROWCOUNT
	IF @Error <> 0 RETURN 1

	-- Return a value indicating success
	RETURN 0

END
GO

GRANT EXECUTE ON [dbo].[spProjectList_Fetch] TO [UIRights] AS [dbo]
GO
