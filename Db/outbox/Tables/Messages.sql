CREATE TABLE [outbox].[Messages]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[CreateDate] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
	[MessageProcessingStatusId] INT NOT NULL DEFAULT (1),
	[MessageError] NVARCHAR(MAX) NULL,
	[MessageTypeId] INT NOT NULL,
	[Payload] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK__outbox__Messages_MessageProcessingStatusId__To__outbox__MessageProcessingStatuses_Id] FOREIGN KEY ([MessageProcessingStatusId]) REFERENCES [outbox].[MessageProcessingStatuses]([Id]), 
    CONSTRAINT [FK__outbox__Messages_MessageTypeId__To__outbox__MessageTypes_Id] FOREIGN KEY ([MessageTypeId]) REFERENCES [outbox].[MessageTypes]([Id])
)
