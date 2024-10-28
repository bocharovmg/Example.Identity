CREATE TABLE [user].[UserAttributes]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1), 
    [ModifyDate] DATETIME2(7) NULL,
    [ModifiedBy] UNIQUEIDENTIFIER NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [UserAttributeSectionId] INT NOT NULL, 
    [Value] NVARCHAR(500) NULL,
    [VerificationCode] NVARCHAR(6) NULL,
    CONSTRAINT [PK__user__UserAttributes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__user__UserAttributes_ModifiedBy__To__user__Users_Id] FOREIGN KEY ([ModifiedBy]) REFERENCES [user].[Users]([Id]), 
    CONSTRAINT [FK__user__UserAttributes_UserId__To__user__Users_Id] FOREIGN KEY ([UserId]) REFERENCES [user].[Users]([Id]), 
    CONSTRAINT [FK__user__UserAttributes_UserAttributeSectionId__To__user__UserAttributeSections_Id] FOREIGN KEY ([UserAttributeSectionId]) REFERENCES [user].[UserAttributeSections]([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__auth__UserAttributes_UserId_UserAttributeId] ON [user].[UserAttributes] ([UserId], [UserAttributeSectionId])

GO

CREATE UNIQUE NONCLUSTERED INDEX [UIX__auth__UserAttributes_UserAttributeSectionId_Value] ON [user].[UserAttributes] ([UserAttributeSectionId], [Value])
    WHERE [Value] IS NOT NULL AND [UserAttributeSectionId] IN (1, 2)

GO