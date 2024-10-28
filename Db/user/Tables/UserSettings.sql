CREATE TABLE [user].[UserSettings]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [UserSettingTypeId] INT NOT NULL, 
    [Value] NVARCHAR(4000) NULL, 
    [IsEnabled] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK__user__UserSettings] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK__user__UserSettings_UserSettingTypeId] UNIQUE ([UserSettingTypeId]), 
    CONSTRAINT [FK__user__UserSettings_UserSettingTypeId__To__user__UserSettingTypes_Id] FOREIGN KEY ([UserSettingTypeId]) REFERENCES [user].[UserSettingTypes]([Id])
)
