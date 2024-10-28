CREATE PROCEDURE [user].[CreateUser]
	@Name NVARCHAR(50)
	, @Email NVARCHAR(50)
	, @AlternativeEmail NVARCHAR(50)
	, @Password NVARCHAR(500)
AS
BEGIN
	DECLARE
		@UserAttributeSectionId_Name BIGINT = 1
		, @UserAttributeSectionId_Email BIGINT = 2
		, @UserAttributeSectionId_Password BIGINT = 3
		, @UserAttributeSectionId_AlternativeEmail BIGINT = 4
		--
		, @ExcludedValues [user].[UT_StringValues]


	INSERT INTO @ExcludedValues
	SELECT
		[VerificationCode] = ua_.VerificationCode
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.VerificationCode IS NOT NULL


	DECLARE @NewIds TABLE
	(
		Id UNIQUEIDENTIFIER PRIMARY KEY
	)


	DECLARE
		@UserId UNIQUEIDENTIFIER
		, @VerificationCode_Email NVARCHAR(6) = [user].[GenerateCode](@ExcludedValues)


	DECLARE
		@VerificationCode_AlternativeEmail NVARCHAR(6) = @VerificationCode_Email


	IF @AlternativeEmail IS NOT NULL
	BEGIN
		INSERT INTO @ExcludedValues
		VALUES (@VerificationCode_Email)

		SET @VerificationCode_AlternativeEmail = [user].[GenerateCode](@ExcludedValues)
	END


	SET @VerificationCode_AlternativeEmail = IIF(@AlternativeEmail IS NOT NULL, @VerificationCode_AlternativeEmail, NULL)


	INSERT INTO [user].Users
	(
		[Id]
	)
	OUTPUT inserted.Id INTO @NewIds
	VALUES
	(
		NEWID()
	)


	SELECT TOP 1
		@UserId = Id
	FROM
		@NewIds


	INSERT INTO [user].UserAttributes
	(
		[UserId]
		, [UserAttributeSectionId]
		, [Value]
		, [VerificationCode]
	)
	VALUES
	(
		@UserId
		, @UserAttributeSectionId_Name
		, @Name
		, NULL
	),
	(
		@UserId
		, @UserAttributeSectionId_Email
		, @Email
		, @VerificationCode_Email
	),
	(
		@UserId
		, @UserAttributeSectionId_AlternativeEmail
		, @AlternativeEmail
		, @VerificationCode_AlternativeEmail
	),
	(
		@UserId
		, @UserAttributeSectionId_Password
		, @Password
		, NULL
	);

	
	SELECT
		[UserId] = @UserId
END
