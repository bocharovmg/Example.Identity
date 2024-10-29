/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF NOT EXISTS(SELECT * FROM sys.sysusers WHERE name = 'identitySystemUser')
BEGIN
    BEGIN TRY
        CREATE LOGIN identitySystemUser   
            WITH PASSWORD = ':r9X~NK%4-W?Uo1X';  

        CREATE USER identitySystemUser FOR LOGIN identitySystemUser;  
    END TRY
    BEGIN CATCH
    END CATCH

    EXEC sp_adduser 'identitySystemUser';
END
GO  


GRANT DELETE ON SCHEMA :: [user] TO identitySystemUser WITH GRANT OPTION;
GRANT EXECUTE ON SCHEMA :: [user] TO identitySystemUser WITH GRANT OPTION;
GRANT INSERT ON SCHEMA :: [user] TO identitySystemUser WITH GRANT OPTION;
GRANT SELECT ON SCHEMA :: [user] TO identitySystemUser WITH GRANT OPTION;
GRANT UPDATE ON SCHEMA :: [user] TO identitySystemUser WITH GRANT OPTION;


GRANT DELETE ON SCHEMA :: [outbox] TO identitySystemUser WITH GRANT OPTION;
GRANT EXECUTE ON SCHEMA :: [outbox] TO identitySystemUser WITH GRANT OPTION;
GRANT INSERT ON SCHEMA :: [outbox] TO identitySystemUser WITH GRANT OPTION;
GRANT SELECT ON SCHEMA :: [outbox] TO identitySystemUser WITH GRANT OPTION;
GRANT UPDATE ON SCHEMA :: [outbox] TO identitySystemUser WITH GRANT OPTION;


:SETVAR ScriptName ".\DataMigrations\0001. Create block types"
GO

IF NOT EXISTS (   SELECT *
                    FROM dbo.MigrationsHistory
                   WHERE ScriptName = '$(ScriptName)' AND DatabaseName = DB_NAME())
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION

            :r $(ScriptName)".SQL"
            INSERT INTO dbo.MigrationsHistory
            VALUES ('$(ScriptName)', DB_NAME(), SYSDATETIME());

            COMMIT
        END TRY
        BEGIN CATCH
            ROLLBACK

            DECLARE @err VARCHAR(MAX) = ERROR_MESSAGE();

            RAISERROR('One time script $(ScriptName).sql failed %s', 16, 1, @err);
        END CATCH;
    END;
GO


:SETVAR ScriptName ".\DataMigrations\0002. Create user attribute section types"
GO

IF NOT EXISTS (   SELECT *
                    FROM dbo.MigrationsHistory
                   WHERE ScriptName = '$(ScriptName)' AND DatabaseName = DB_NAME())
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION

            :r $(ScriptName)".SQL"
            INSERT INTO dbo.MigrationsHistory
            VALUES ('$(ScriptName)', DB_NAME(), SYSDATETIME());

            COMMIT
        END TRY
        BEGIN CATCH
            ROLLBACK

            DECLARE @err VARCHAR(MAX) = ERROR_MESSAGE();

            RAISERROR('One time script $(ScriptName).sql failed %s', 16, 1, @err);
        END CATCH;
    END;
GO


:SETVAR ScriptName ".\DataMigrations\0003. Create user settings"
GO

IF NOT EXISTS (   SELECT *
                    FROM dbo.MigrationsHistory
                   WHERE ScriptName = '$(ScriptName)' AND DatabaseName = DB_NAME())
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION

            :r $(ScriptName)".SQL"
            INSERT INTO dbo.MigrationsHistory
            VALUES ('$(ScriptName)', DB_NAME(), SYSDATETIME());

            COMMIT
        END TRY
        BEGIN CATCH
            ROLLBACK

            DECLARE @err VARCHAR(MAX) = ERROR_MESSAGE();

            RAISERROR('One time script $(ScriptName).sql failed %s', 16, 1, @err);
        END CATCH;
    END;
GO


:SETVAR ScriptName ".\DataMigrations\0004. Create outbox message types"
GO

IF NOT EXISTS (   SELECT *
                    FROM dbo.MigrationsHistory
                   WHERE ScriptName = '$(ScriptName)' AND DatabaseName = DB_NAME())
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION

            :r $(ScriptName)".SQL"
            INSERT INTO dbo.MigrationsHistory
            VALUES ('$(ScriptName)', DB_NAME(), SYSDATETIME());

            COMMIT
        END TRY
        BEGIN CATCH
            ROLLBACK

            DECLARE @err VARCHAR(MAX) = ERROR_MESSAGE();

            RAISERROR('One time script $(ScriptName).sql failed %s', 16, 1, @err);
        END CATCH;
    END;
GO
