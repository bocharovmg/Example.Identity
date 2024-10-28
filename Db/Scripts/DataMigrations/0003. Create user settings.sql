/** USER SETTING TYPES */

DECLARE
	@UserSettingTypeId_MultipleAlternativeEmails INT = 1
	, @UserSettingTypeDescription_MultipleAlternativeEmails NVARCHAR(50) = N'Multiple alternative emails enabled'
	, @UserSettingTypeId_AlternativeEmailsAsMain INT = 2
	, @UserSettingTypeDescription_AlternativeEmailsAsMain NVARCHAR(50) = N'Use alternative email as main for notification'

SET IDENTITY_INSERT [user].UserSettingTypes ON;

MERGE [user].UserSettingTypes tgt
USING (
	SELECT
		[Id] = @UserSettingTypeId_MultipleAlternativeEmails
		, [Description] = @UserSettingTypeDescription_MultipleAlternativeEmails

	UNION
	SELECT
		[Id] = @UserSettingTypeId_AlternativeEmailsAsMain
		, [Description] = @UserSettingTypeDescription_AlternativeEmailsAsMain
) src
ON (
	tgt.Id = src.Id
)
WHEN NOT MATCHED BY TARGET
	THEN
		INSERT
		(
			[Id]
			, [Description]
		)
		VALUES
		(
			src.Id
			, src.Description
		)
WHEN MATCHED
	THEN
		UPDATE
		SET
			tgt.Description = src.Description
;

SET IDENTITY_INSERT [user].UserSettingTypes OFF;


/** USER SETTINGS */

DECLARE
	@UserSettingId_MultipleAlternativeEmails INT = 1
	, @UserSettingValue_MultipleAlternativeEmails NVARCHAR(4000) = N'0'
	, @UserSettingId_AlternativeEmailsAsMain INT = 2
	, @UserSettingValue_AlternativeEmailsAsMain NVARCHAR(4000) = N'0'

SET IDENTITY_INSERT [user].UserSettings ON;

MERGE [user].UserSettings tgt
USING (
	SELECT
		[Id] = @UserSettingId_MultipleAlternativeEmails
		, [UserSettingTypeId] = @UserSettingTypeId_MultipleAlternativeEmails
		, [Value] = @UserSettingValue_MultipleAlternativeEmails

	UNION
	SELECT
		[Id] = @UserSettingId_AlternativeEmailsAsMain
		, [UserSettingTypeId] = @UserSettingTypeId_AlternativeEmailsAsMain
		, [Value] = @UserSettingValue_AlternativeEmailsAsMain
) src
ON (
	tgt.Id = src.Id
)
WHEN NOT MATCHED BY TARGET
	THEN
		INSERT
		(
			[Id]
			, [UserSettingTypeId]
			, [Value]
		)
		VALUES
		(
			src.Id
			, src.UserSettingTypeId
			, src.Value
		)
WHEN MATCHED
	THEN
		UPDATE
		SET
			tgt.UserSettingTypeId = src.UserSettingTypeId
			, tgt.Value = src.Value
;

SET IDENTITY_INSERT [user].UserSettings OFF;
