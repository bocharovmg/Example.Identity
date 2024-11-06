CREATE TABLE [outbox].[MessageProcessingStatuses]
(
	[Id] INT NOT NULL, 
	[Description] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK__outbox__MessageProcessingStatuses] PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__outbox__MessageProcessingStatuses_Description] ON [outbox].[MessageProcessingStatuses] ([Description])
