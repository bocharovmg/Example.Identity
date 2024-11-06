/** OUTBOX MESSAGE PROCESSING STATUSES */

DECLARE
	@MessageProcessingStatusId_NotProcessed INT = 1
	, @MessageProcessingStatusDescription_NotProcessed NVARCHAR(50) = N'Not processed'
	, @MessageProcessingStatusId_Processing INT = 2
	, @MessageProcessingStatusDescription_Processing NVARCHAR(50) = N'Processing'
	, @MessageProcessingStatusId_Success INT = 3
	, @MessageProcessingStatusDescription_Success NVARCHAR(50) = N'Success'
	, @MessageProcessingStatusId_Error INT = 4
	, @MessageProcessingStatusDescription_Error NVARCHAR(50) = N'Error'

MERGE [outbox].[MessageProcessingStatuses] tgt
USING (
	SELECT
		[Id] = @MessageProcessingStatusId_NotProcessed
		, [Description] = @MessageProcessingStatusDescription_NotProcessed

	UNION
	SELECT
		[Id] = @MessageProcessingStatusId_Processing
		, [Description] = @MessageProcessingStatusDescription_Processing

	UNION
	SELECT
		[Id] = @MessageProcessingStatusId_Success
		, [Description] = @MessageProcessingStatusDescription_Success

	UNION
	SELECT
		[Id] = @MessageProcessingStatusId_Error
		, [Description] = @MessageProcessingStatusDescription_Error
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
