-- 1023 is believed to be the maximum length of any given component of a .Net NameSpace value.
-- There is no actual total limit to the length at the coding level.
-- Each component is put into a separate row and the data is organized into a hierarchy.
-- Key size in TSQL is limited to 1700 characters. 128 characters is a compromise and matches SysName size.
CREATE TYPE [App_DataDictionary].[typeNameSpaceElement] FROM NVarChar(128) NOT NULL
