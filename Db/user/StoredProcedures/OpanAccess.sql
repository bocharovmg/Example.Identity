CREATE PROCEDURE [user].[OpanAccess]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE u_
	SET
		u_.BlockTypeId = NULL
		, u_.BlockReason = NULL
		, u_.BlockUntil = NULL
	FROM
		[user].Users u_
	WHERE
		u_.Id = @UserId
END
