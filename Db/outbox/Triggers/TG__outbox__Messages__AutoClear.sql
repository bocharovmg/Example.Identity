CREATE TRIGGER [outbox].[TG__outbox__Messages__AutoClear]
ON [outbox].[Messages]
AFTER INSERT
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_Success INT = 3
		, @MessageProcessingStatusId_Error INT = 4


	IF (
		SELECT
			COUNT(*)
		FROM
			[outbox].[Messages]
	) <= 50000
	BEGIN
		RETURN
	END


	DELETE
	FROM
		[outbox].[Messages]
	WHERE
		MessageProcessingStatusId IN (
			@MessageProcessingStatusId_Success
			, @MessageProcessingStatusId_Error
		)
END