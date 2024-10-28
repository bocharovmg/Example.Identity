CREATE PROCEDURE [user].[GetUserIdByVerificationCode]
	@VerificationCode NVARCHAR(6)
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name BIGINT = 1
		, @UserAttributeSectionId_Email BIGINT = 2


	SELECT
		ua_.UserId
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.VerificationCode = @VerificationCode
END
