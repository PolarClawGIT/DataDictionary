-- An NameSpace is the combination of NameSpace Members delimited by period and qualified by square brackets.
-- Periods are possible within the qualified name (example [Help.ID]).
-- This value is of unknown length. Examples are:
-- Database: [DatabaseName].[SchemaName].[TableName].[ColumnName]
-- Library, C#: [NameSpace].[SubNameSpace].[ClassName].[PropertyName]
-- Model: [SubjectArea].[Sub-SubjectArea].[ElementName]
--
-- Limited to 4000 characters because the use of FormatMessage to build the value.
CREATE TYPE [AppGeneral].[uddtNameSpacePath] FROM NVarchar(4000)
