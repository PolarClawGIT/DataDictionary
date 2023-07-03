CREATE TABLE [App_DataDictionary].[ApplicationProperty]
(
	-- Works as a lookup to create/define an Extended Property. Application Scoped, may be coded into C#?
	[PropertyId] Int NOT Null,
	[PropertyTitle] NVarChar(100) Not Null,
	-- This is based on https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- Not-normalized to better translate to SQL call.
	[PropertyName] SysName Null, -- Name for the Extended property. Most interested in: MS_Description
	[ScopeType] SysName Null, -- Type of Level0. Examples is Schema, Synonym, and User
	[ObjectType] SysName Null, -- Type of Level1. Examples are Table, View, Procedure and Function
	[ElementType] SysName Null, -- Type of Level2. Examples are Column and Parameter
	-- Keys
	CONSTRAINT [PK_ApplicationProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationProperty]
    ON [App_DataDictionary].[ApplicationProperty]([PropertyTitle] ASC);
GO