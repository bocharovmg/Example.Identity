CREATE PROCEDURE [outbox].[NextMessage]
	@MessageType INT
AS
BEGIN
	SELECT TOP 1
		[MessageId] = m_.Id
		, [Payload] = m_.Payload
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.MessageTypeId = @MessageType
	ORDER BY
		m_.CreateDate
END
