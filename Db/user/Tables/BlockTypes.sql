CREATE TABLE [user].[BlockTypes]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK__user__BlockTypes] PRIMARY KEY ([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__user__BlockTypes_Description] ON [user].[BlockTypes] ([Description])
