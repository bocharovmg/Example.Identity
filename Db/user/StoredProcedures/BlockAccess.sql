CREATE PROCEDURE [user].[BlockAccess]
	@UserId UNIQUEIDENTIFIER
	, @BlockType INT
	, @BlockReason NVARCHAR(MAX)
	, @BlockUntil DATE
AS
BEGIN
	DECLARE
		@BlockTypeId_Temporary INT = 2


	UPDATE u_
	SET
		u_.BlockTypeId = @BlockType
		, u_.BlockReason = @BlockReason
		, u_.BlockUntil = IIF(@BlockType = @BlockTypeId_Temporary, @BlockUntil, NULL)
	FROM
		[user].Users u_
	WHERE
		u_.Id = @UserId
		AND @BlockType IS NOT NULL
END
