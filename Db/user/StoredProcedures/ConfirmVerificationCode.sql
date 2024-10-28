CREATE PROCEDURE [user].[ConfirmVerificationCode]
	@VerificationCode NVARCHAR(6)
AS
BEGIN
	UPDATE ua_
	SET
		ua_.VerificationCode = NULL
	FROM
		[user].UserAttributes ua_
	WHERE
		ua_.VerificationCode = @VerificationCode
END
