CREATE PROCEDURE [outbox].[SetMessageErrorStatus]
	@MessageId UNIQUEIDENTIFIER
	, @MessageError NVARCHAR(MAX)
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_Processing INT = 2
		, @MessageProcessingStatusId_Error INT = 4


	UPDATE m_
	SET
		m_.MessageProcessingStatusId = @MessageProcessingStatusId_Error
		, m_.MessageError = @MessageError
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.Id = @MessageId
		and m_.MessageProcessingStatusId = @MessageProcessingStatusId_Processing
END
