/** VERIFICATION STATE TYPES */

DECLARE
	@VerificationStateTypeId_Password INT = 1
	, @VerificationStateTypeDescription_Password NVARCHAR(50) = N'Password'
	, @VerificationStateTypeId_Email INT = 2
	, @VerificationStateTypeDescription_Email NVARCHAR(50) = N'Email'
	, @VerificationStateTypeId_AlternativeEmail INT = 3
	, @VerificationStateTypeDescription_AlternativeEmail NVARCHAR(50) = N'AlternativeEmail'

MERGE [user].[VerificationStateTypes] tgt
USING (
	SELECT
		[Id] = @VerificationStateTypeId_Password
		, [Description] = @VerificationStateTypeDescription_Password

	UNION
	SELECT
		[Id] = @VerificationStateTypeId_Email
		, [Description] = @VerificationStateTypeDescription_Email

	UNION
	SELECT
		[Id] = @VerificationStateTypeId_AlternativeEmail
		, [Description] = @VerificationStateTypeDescription_AlternativeEmail
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
