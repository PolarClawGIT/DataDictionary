-- Represents a File Name
-- The maximum allowed limit is dependent on file system.
-- MS Windows: 255 characters
-- MS SQL TableFile: 255 characters
CREATE TYPE [AppGeneral].[uddtFileName] FROM NVarchar(255)
