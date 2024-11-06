CREATE PROCEDURE [outbox].[RemoveProcessedMessages]
	@MessageIds [outbox].[UT_GuidIdentifier] READONLY
AS
BEGIN
	DECLARE
		@MessageProcessingStatusId_Success INT = 3
		, @MessageProcessingStatusId_Error INT = 4


	DELETE m_
	FROM
		[outbox].[Messages] m_
	JOIN
		@MessageIds mi_ ON
			m_.Id = mi_.Id
	WHERE
		m_.MessageProcessingStatusId IN (
			@MessageProcessingStatusId_Success
			, @MessageProcessingStatusId_Error
		)
END
