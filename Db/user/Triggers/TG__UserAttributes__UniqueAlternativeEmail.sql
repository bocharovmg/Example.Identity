CREATE TRIGGER [user].[TG__UserAttributes__UniqueAlternativeEmail]
ON [user].[UserAttributes]
AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;


	DECLARE
		@UserSettingTypeId_MultipleAlternativeEmails INT = 1
		, @UserAttributeSectionDescription_Email INT = 2
		, @UserAttributeSectionId_AlternativeEmail INT = 4


	IF EXISTS (
		SELECT
			*
		FROM
			[user].UserAttributes ua_
		JOIN
			inserted i_ ON
				ua_.Value = i_.Value
				AND ua_.Id <> i_.Id
		WHERE
			ua_.UserAttributeSectionId IN (
				@UserAttributeSectionDescription_Email
				, @UserAttributeSectionId_AlternativeEmail
			)
			AND i_.UserAttributeSectionId IN (
				@UserAttributeSectionDescription_Email
				, @UserAttributeSectionId_AlternativeEmail
			)
			AND i_.Value IS NOT NULL
	)
	BEGIN;
		THROW 60005, 'Duplicate alternative email', 1;
	END
	ELSE IF EXISTS (
		SELECT
			i_.Value
		FROM
			inserted i_
		WHERE
			i_.UserAttributeSectionId IN (
				@UserAttributeSectionDescription_Email
				, @UserAttributeSectionId_AlternativeEmail
			)
			AND i_.Value IS NOT NULL
		GROUP BY
			i_.Value
		HAVING
			COUNT(i_.Value) > 1
	)
	BEGIN;
		THROW 60005, 'Duplicate alternative email', 1;
	END
END