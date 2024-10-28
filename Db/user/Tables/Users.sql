CREATE TABLE [user].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedBy] BIGINT NULL, 
    [ModifyDate] DATETIME2 NULL, 
    [BlockTypeId] INT NULL , 
    [BlockReason] NVARCHAR(MAX) NULL, 
    [BlockUntil] DATE NULL, 
    CONSTRAINT [FK__user__Users_BlockTypeId__To__user__BlockTypes_Id] FOREIGN KEY ([BlockTypeId]) REFERENCES [user].[BlockTypes]([Id]), 
    CONSTRAINT [PK__user__Users] PRIMARY KEY ([Id])
)

GO
