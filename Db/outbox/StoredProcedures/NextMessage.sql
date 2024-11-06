CREATE PROCEDURE [outbox].[NextMessage]
	@MessageType INT
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_NotProcessed INT = 1
		, @MessageProcessingStatusId_Processing INT = 2
		, @MessageId UNIQUEIDENTIFIER
		

	SELECT TOP 1
		@MessageId = m_.Id
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.MessageTypeId = @MessageType
		and m_.MessageProcessingStatusId = @MessageProcessingStatusId_NotProcessed
	ORDER BY
		m_.CreateDate


	UPDATE [outbox].[Messages]
	SET
		MessageProcessingStatusId = @MessageProcessingStatusId_Processing
	WHERE
		Id = @MessageId
		

	SELECT TOP 1
		[MessageId] = m_.Id
		, [Payload] = m_.Payload
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.Id = @MessageId
END
