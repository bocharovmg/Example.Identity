CREATE TABLE [dbo].[MigrationsHistory]
(
	[ScriptName] VARCHAR(255) NOT NULL PRIMARY KEY, 
    [DatabaseName] [sys].[sysname] NOT NULL, 
    [ExecutionTime] DATETIME2 NOT NULL
)
