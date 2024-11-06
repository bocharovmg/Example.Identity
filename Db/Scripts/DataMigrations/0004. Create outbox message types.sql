/** OUTBOX MESSAGE TYPES */

DECLARE
	@MessageTypeId_Email INT = 1
	, @MessageTypeDescription_Email NVARCHAR(50) = N'Email'

MERGE [outbox].[MessageTypes] tgt
USING (
	SELECT
		[Id] = @MessageTypeId_Email
		, [Description] = @MessageTypeDescription_Email
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
