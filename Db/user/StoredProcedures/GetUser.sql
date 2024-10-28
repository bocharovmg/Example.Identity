CREATE PROCEDURE [user].[GetUser]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name INT = 1
		, @UserAttributeSectionId_Email INT = 2
		, @UserAttributeSectionId_AlternativeEmail INT = 4


	SELECT
		[UserId] = u_.Id
		, [CreateDate] = u_.CreateDate
		, [IsFirstVerificationCompleted] = CASE
			WHEN ua_email_.Id IS NULL
				THEN 0
			WHEN ua_email_.VerificationCode IS NOT NULL
				THEN 0
			ELSE 1
		END
		, [BlockTypeId] = u_.BlockTypeId
		, [Name] = ua_name_.Value
		, [Email] = ua_email_.Value
		, [AlternativeEmail] = ua_altemail_.Value
	FROM
		[user].Users u_

	-- Attribute links
	LEFT JOIN
		[user].UserAttributes ua_name_ ON
			u_.Id = ua_name_.UserId
			AND ua_name_.UserAttributeSectionId = @UserAttributeSectionId_Name
	LEFT JOIN
		[user].UserAttributes ua_email_ ON
			u_.Id = ua_email_.UserId
			AND ua_email_.UserAttributeSectionId = @UserAttributeSectionId_Email
	LEFT JOIN
		[user].UserAttributes ua_altemail_ ON
			u_.Id = ua_altemail_.UserId
			AND ua_altemail_.UserAttributeSectionId = @UserAttributeSectionId_AlternativeEmail

	WHERE
		u_.Id = @UserId
END
