-- An Alias Scope Name is the combination of Alias Scope Elements delimited by period.
-- These are short as then are used to give context to a Alias Name.
-- Example, a Database Column Alias would have a Scope of: Database.Schema.Table.Column
CREATE TYPE [App_DataDictionary].[typeScopeName] FROM NVarChar(128) NOT NULL
