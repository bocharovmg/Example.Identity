CREATE TABLE [user].[UserSettingTypes]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK__user__UserSettingTypes] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK__user__UserSettingTypes_Description] UNIQUE ([Description])
)
