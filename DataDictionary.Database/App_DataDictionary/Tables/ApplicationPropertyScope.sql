CREATE TABLE [App_DataDictionary].[ApplicationPropertyScope]
( -- TODO: Is this needed?
	[PropertyId]         UniqueIdentifier NOT Null,
	-- This is based on https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	[ScopeType]          SysName Null, -- Type of Level0. Examples is Schema, Synonym, and User
	[ObjectType]         SysName Null, -- Type of Level1. Examples are Table, View, Procedure and Function
	[ElementType]        SysName Null, -- Type of Level2. Examples are Column and Parameter
	CONSTRAINT [FK_ApplicationPropertyScope] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[ApplicationProperty] ([PropertyId]),
)
GO
CREATE UNIQUE CLUSTERED INDEX [UX_ApplicationPropertyScope]
    ON [App_DataDictionary].[ApplicationPropertyScope]([PropertyId] ASC, [ScopeType] ASC, [ObjectType] ASC, [ElementType] ASC);
GO
