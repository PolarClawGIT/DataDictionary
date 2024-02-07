-- An Alias Name is the combination of Alias Elements delimited by period and qualified by square brackets.
-- Periods are possible within the qualified name (example [Help.ID]).
-- This value is of unknown length. Examples are:
-- Database: [DatabaseName].[SchemaName].[TableName].[ColumnName]
-- Library, C#: [NameSpace].[SubNameSpace].[ClassName].[PropertyName]
CREATE TYPE [App_DataDictionary].[typeNameSpaceFullName] FROM NVarchar(Max)
