CREATE TABLE [App_DataDictionary].[ExtendedPropertyType]
(
	[ObjectTypeId] Int Not Null,
    [ObjectTypeName] AS (CONVERT([sysname],case when [Level0Type] IS NULL then NULL when [Level1Type] IS NULL then formatmessage('[%s]',[Level0Type]) when [Level2Type] IS NULL then formatmessage('[%s].[%s]',[Level0Type],[Level1Type]) else formatmessage('[%s].[%s].[%s]',[Level0Type],[Level1Type],[Level2Type]) end)),
	-- These match the values from https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
	-- This is all varations that the application supports
	[Level0Type] VarChar(128) Not Null,
	[Level1Type] VarChar(128) Null,
	[Level2Type] VarChar(128) Null,
	CONSTRAINT [PK_ExtendedPropertyType] PRIMARY KEY CLUSTERED ([ObjectTypeId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ExtendedPropertyType]
    ON [App_DataDictionary].[ExtendedPropertyType]([Level0Type] ASC,[Level1Type] ASC,[Level2Type] ASC);
GO