CREATE TABLE [user].[UserAttributeSections]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK__user__UserAttributeSections] PRIMARY KEY ([Id] ASC)
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__user__UserAttributeSections_Description] ON [user].[UserAttributeSections] ([Description])
