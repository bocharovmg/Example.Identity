CREATE PROCEDURE [dbo].[GetProcessedMessages]
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_Success INT = 3
		, @MessageProcessingStatusId_Error INT = 4


	SELECT
		[MessageId] = m_.Id
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.MessageProcessingStatusId IN (
			@MessageProcessingStatusId_Success
			, @MessageProcessingStatusId_Error
		)
	ORDER BY
		m_.CreateDate
END
