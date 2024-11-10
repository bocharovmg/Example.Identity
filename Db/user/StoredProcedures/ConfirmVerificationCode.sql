CREATE PROCEDURE [user].[ConfirmVerificationCode]
	@UserId UNIQUEIDENTIFIER
	, @VerificationCode NVARCHAR(6)
	, @UserAttributeSectionId INT
AS
BEGIN
	UPDATE ua_
	SET
		ua_.VerificationCode = NULL
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.UserId = @UserId
		AND ua_.VerificationCode = @VerificationCode
		AND @VerificationCode <> ''
		AND ua_.UserAttributeSectionId = @UserAttributeSectionId
END
