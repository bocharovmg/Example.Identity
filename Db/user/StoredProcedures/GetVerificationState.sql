CREATE PROCEDURE [user].[GetVerificationState]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE
		-- User attribute sections
		@UserAttributeSectionId_Email BIGINT = 2
		, @UserAttributeSectionId_Password BIGINT = 3
		, @UserAttributeSectionId_AlternativeEmail BIGINT = 4
		-- Verification state types
		, @VerificationStateTypeId_Password INT = 1
		, @VerificationStateTypeId_Email INT = 2
		, @VerificationStateTypeId_AlternativeEmail INT = 3


	SELECT
		[VerificationState] = CASE
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Password THEN @VerificationStateTypeId_Password
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_AlternativeEmail THEN @VerificationStateTypeId_Email
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Email THEN @VerificationStateTypeId_AlternativeEmail
		END
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.UserId = @UserId
		AND ua_.VerificationCode IS NOT NULL
	ORDER BY
		CASE
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Password THEN 1
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_AlternativeEmail THEN 2
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Email THEN 3
				ELSE 999
		END
END
