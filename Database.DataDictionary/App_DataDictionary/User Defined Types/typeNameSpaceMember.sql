﻿-- An NameSpace Element/Member is one segment of the NameSpace Full Name.
-- In SQL Db examples are: Database Name, Schema Name, Object Name or Column Name.
-- The maximum length is 128 characters (256 bytes)
-- In .Net examples are: single part of a NameSpace, Class Name, Property Name, Field Name or Method Name.
-- The maximum length is set to 1023 (2046 bytes) based on VB.Net definitions.
-- Fields of this type exceed the limits of a SQL Index (900 bytes for Clustered, 1700 bytes for non-clustered).
-- For NVarChar, the limit is 850 (two bytes per character).
-- Application uses one or two GUID's as a primary key (16 bytes each).
-- Available spaces is rounded down to 800 for non-clustered indexes.
-- A workaround is to create a Binary CheckSum on the value and index that with a unique part of the table.
CREATE TYPE [App_DataDictionary].[typeNameSpaceMember] FROM NVarChar(800) NOT NULL
