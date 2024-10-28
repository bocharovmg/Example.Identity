CREATE PROCEDURE [user].[GetVerificationState]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name BIGINT = 1
		, @UserAttributeSectionId_Email BIGINT = 2
		, @UserAttributeSectionId_Password BIGINT = 3
		, @UserAttributeSectionId_AlternativeEmail BIGINT = 4


	SELECT TOP 1
		[VerificationState] = CASE
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Password THEN 1
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_AlternativeEmail THEN 3
			WHEN ua_.UserAttributeSectionId = @UserAttributeSectionId_Email THEN 2
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
