CREATE TABLE [user].[VerificationStateTypes]
(
	[Id] INT NOT NULL,
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK__user__VerificationStateTypes] PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__user__VerificationStateTypes_Description] ON [user].[VerificationStateTypes] ([Description])
