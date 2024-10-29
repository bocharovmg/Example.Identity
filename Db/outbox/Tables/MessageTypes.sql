CREATE TABLE [outbox].[MessageTypes]
(
	[Id] INT NOT NULL ,
	[Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK__outbox__MessageTypes] PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__outbox__MessageTypes_Description] ON [outbox].[MessageTypes] ([Description])
