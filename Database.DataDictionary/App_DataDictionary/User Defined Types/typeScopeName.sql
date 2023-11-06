-- Note: SQL Cannot cast or convert a value to a User Defined Type (aka Alias Data Type)
-- The limit chosen is for indexing purposes.
CREATE TYPE [App_DataDictionary].[typeScopeName] FROM NVarChar(450) NOT NULL
