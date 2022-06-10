
CREATE PROCEDURE [dbo].[spProject_Fetch]

/**************************************************************************************************

	Description:		Fetch a single Project
	Author:				Andrew
	Created date:		10/06/22

	Change History:

	Date			Modified By				Description
	-------			-----------------		------------------------------------------------------
	10/06/22		Andrew					Created

***************************************************************************************************/

	@Id AS int

AS
BEGIN

DECLARE @Error Int
DECLARE @RowsAffected Int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Get the data that is required, matching on the parameters provided
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
	WHERE				[Project].[Id] = @Id

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

GRANT EXECUTE ON [dbo].[spProject_Fetch] TO [UIRights] AS [dbo]
GO
