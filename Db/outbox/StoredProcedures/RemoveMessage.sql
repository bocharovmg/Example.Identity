CREATE PROCEDURE [outbox].[RemoveMessage]
	@MessageId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE m_
	FROM
		[outbox].[Messages] m_
	WHERE
		m_.Id = @MessageId
END
