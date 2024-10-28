CREATE PROCEDURE [user].[Auth]
	@Login NVARCHAR(50)
	, @Password NVARCHAR(500)
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name BIGINT = 1
		, @UserAttributeSectionId_Email BIGINT = 2
		, @UserAttributeSectionId_Password BIGINT = 3


	DECLARE
		@UserId UNIQUEIDENTIFIER


	SELECT
		@UserId = u_.Id
	FROM
		[user].Users u_
	JOIN
		[user].UserAttributes ua_name_ ON
			u_.Id = ua_name_.UserId
			AND ua_name_.UserAttributeSectionId IN (@UserAttributeSectionId_Name, @UserAttributeSectionId_Email)
	JOIN
		[user].UserAttributes ua_password_ ON
			u_.Id = ua_password_.UserId
			AND ua_password_.UserAttributeSectionId = @UserAttributeSectionId_Password
	WHERE
		u_.BlockTypeId IS NULL
		AND ua_name_.Value = @Login
		AND ua_password_.Value = @Password


	UPDATE ua_
	SET
		ua_.VerificationCode = NULL
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.UserId = @UserId
		AND ua_.UserAttributeSectionId = @UserAttributeSectionId_Password
		AND ua_.VerificationCode IS NOT NULL


	SELECT
		[UserId] = @UserId
END
