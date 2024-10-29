/** OUTBOX MESSAGE TYPES */

DECLARE
	@MessageTypeId_Email INT = 1
	, @MessageTypeDescription_Email NVARCHAR(50) = N'Email'

INSERT INTO [outbox].[MessageTypes]
(
	Id
	, Description
)
VALUES
(
	@MessageTypeId_Email
	, @MessageTypeDescription_Email
);
