CREATE PROCEDURE [outbox].[AddMessage]
	@MessageType INT
	, @Payload NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [outbox].[Messages]
	(
		[MessageTypeId]
		, [Payload]
	)
	VALUES
	(
		@MessageType
		, @Payload
	)
END
