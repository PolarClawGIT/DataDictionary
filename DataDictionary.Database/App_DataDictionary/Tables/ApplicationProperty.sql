CREATE TABLE [App_DataDictionary].[ApplicationProperty]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]         Int NOT Null,
	[PropertyTitle]      NVarChar(100) Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[ModelId]            UniqueIdentifier Null, -- Null = Default for all models. ModelID is for that Model only.
	-- This is based on https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	[IsExtendedProperty] As (CONVERT([bit],case when [PropertyName] IS NULL then (0) else (1) end)),
	[PropertyName]       SysName Null, -- Name for the Extended property. Most interested in: MS_Description
	-- TODO: This needs to go into a child table. ApplicationPropertyConfiguration? Is it needed?
	[ScopeType]          SysName Null, -- Type of Level0. Examples is Schema, Synonym, and User
	[ObjectType]         SysName Null, -- Type of Level1. Examples are Table, View, Procedure and Function
	[ElementType]        SysName Null, -- Type of Level2. Examples are Column and Parameter
 	-- It is not intended to delete things belonging to the Null model. But they can be marked Obsolete.
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	[ObsoleteDate]       DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	-- Keys
	CONSTRAINT [PK_ApplicationProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationProperty]
    ON [App_DataDictionary].[ApplicationProperty]([PropertyTitle] ASC, [ModelId] ASC);
GO