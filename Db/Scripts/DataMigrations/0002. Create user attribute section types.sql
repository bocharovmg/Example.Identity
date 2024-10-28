/** USER ATTRIBUTE SECTION TYPES */

DECLARE
	@UserAttributeSectionId_Name BIGINT = 1
	, @UserAttributeSectionDescription_Name NVARCHAR(50) = N'Name'
	, @UserAttributeSectionId_Email BIGINT = 2
	, @UserAttributeSectionDescription_Email NVARCHAR(50) = N'Email'
	, @UserAttributeSectionId_Password BIGINT = 3
	, @UserAttributeSectionDescription_Password NVARCHAR(50) = N'Password'
	, @UserAttributeSectionId_AlternativeEmail BIGINT = 4
	, @UserAttributeSectionDescription_AlternativeEmail NVARCHAR(50) = N'AlternativeEmail'

SET IDENTITY_INSERT [user].UserAttributeSections ON;

MERGE [user].UserAttributeSections tgt
USING (
	SELECT
		[Id] = @UserAttributeSectionId_Name
		, [Description] = @UserAttributeSectionDescription_Name

	UNION
	SELECT
		[Id] = @UserAttributeSectionId_Email
		, [Description] = @UserAttributeSectionDescription_Email

	UNION
	SELECT
		[Id] = @UserAttributeSectionId_Password
		, [Description] = @UserAttributeSectionDescription_Password

	UNION
	SELECT
		[Id] = @UserAttributeSectionId_AlternativeEmail
		, [Description] = @UserAttributeSectionDescription_AlternativeEmail
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

SET IDENTITY_INSERT [user].UserAttributeSections OFF;
