CREATE PROCEDURE [user].[SetupPassword]
	@UserId UNIQUEIDENTIFIER
	, @Password NVARCHAR(500)
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Password BIGINT = 3


	UPDATE ua_
	SET
		ua_.Value = @Password
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.UserId = @UserId
		AND ua_.UserAttributeSectionId = @UserAttributeSectionId_Password
END
