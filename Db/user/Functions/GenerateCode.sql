CREATE FUNCTION [user].[GenerateCode]
(
	@ExcludedValues [user].[UT_StringValues] READONLY
)
RETURNS NVARCHAR(6)
AS
BEGIN
	DECLARE
		@Result NVARCHAR(6)

	WHILE (
		@Result IS NULL
		OR EXISTS (
			SELECT
				*
			FROM
				@ExcludedValues ev_
			WHERE
				ev_.Value IS NOT NULL
				AND ev_.Value = @Result
		)
	)
	BEGIN
		SELECT
			@Result = FORMAT(ABS(CAST(FLOOR(rv_.RandValue * 1000000 - 1) AS INT)), 'D6')
		FROM
			[user].RandView rv_
	END


	RETURN @Result
END
