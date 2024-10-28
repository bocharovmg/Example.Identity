CREATE PROCEDURE [user].[GenerateVerificationCode]
	@UserId UNIQUEIDENTIFIER
	, @VerificationField INT
AS
BEGIN
	DECLARE
		@ExcludedValues [user].[UT_StringValues]
		, @NewVeirficationCode NVARCHAR(6)


	INSERT INTO @ExcludedValues
	SELECT
		[VerificationCode] = ua_.VerificationCode
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.VerificationCode IS NOT NULL


	SET @NewVeirficationCode = [user].[GenerateCode](@ExcludedValues)


	UPDATE ua_
	SET
		ua_.VerificationCode = @NewVeirficationCode
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.UserId = @UserId
		AND ua_.UserAttributeSectionId = @VerificationField


	SELECT
		[VerificationCode] = @NewVeirficationCode
END
