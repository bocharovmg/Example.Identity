CREATE PROCEDURE [outbox].[SetMessageSuccessStatus]
	@MessageId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_Processing INT = 2
		, @MessageProcessingStatusId_Success INT = 3


	UPDATE m_
	SET
		m_.MessageProcessingStatusId = @MessageProcessingStatusId_Success
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.Id = @MessageId
		and m_.MessageProcessingStatusId = @MessageProcessingStatusId_Processing
END
