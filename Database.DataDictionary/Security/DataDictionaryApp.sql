-- Application Role
-- In order to activate an Application role:
-- * User must be a member of the database (no database level permissions are needed)
-- * Application passes the sp_SetAppRole with the correct password
-- Note: The password is assumed to be passed unencrypted.
--       With the correct network setup, the connection can be made encrypted.
--       An encrypted connection encrypts all calls to SQL, thus protects the password.
--       In unencrypted Scenarios, all user must be setup with Windows Authentication.
--       This insures that even if a bad-actor has the password, they still need
--       to be a valid Windows user of the database.
--       Other Authentication will likely work but have not been explored.
--       Password can be changed so long as it is changed here and in the Application source code.
CREATE APPLICATION ROLE [DataDictionaryApp]
    WITH PASSWORD = N'MyApp131*99', DEFAULT_SCHEMA = [App_DataDictionary];
GO

