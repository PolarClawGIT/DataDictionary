-- Represents a File Path.
-- The maximum allowed limit is dependent on file system.
-- The value selected here is intended to be a compremise and cover most scenarios.
-- MS Windows prior to Windows 10: 260 characters
-- MS Windows 10 and higher: 32,767 characters
CREATE TYPE [AppGeneral].[uddtFilePath] FROM NVarchar(500)
