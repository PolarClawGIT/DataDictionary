CREATE TABLE [App_DataDictionary].[DatabaseExtendedProperty]
(
		[CatalogId]      UniqueIdentifier Not Null,
		-- Parameters for [fn_listextendedproperty]
		[Level0Type]     SysName Null,
		[Level0Name]     SysName Null,
		[Level1Type]     SysName Null,
		[Level1Name]     SysName Null,
		[Level2Type]     SysName Null,
		[Level2Name]     SysName Null,
		-- Results from [fn_listextendedproperty]
		[ObjType]        SysName Not Null,
		[ObjName]        SysName Not Null,
		[PropertyName]   SysName Not Null,
		[PropertyValue]  NVarChar(Max) Null,
		-- Keys
		CONSTRAINT [FK_DatabaseExtendedPropertyCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
GO
-- There is no key structure returned by the function [fn_listextendedproperty]
-- This may not work as it may be possible to have multiple elements with the same name but different types.
CREATE UNIQUE INDEX [UX_DatabaseExtendedProperty]
    ON [App_DataDictionary].[DatabaseExtendedProperty]([CatalogId] ASC, [Level0Name], [Level1Name], [Level2Name], [PropertyName]);
GO
/*
-- Each item must be queried by itself. No way to get all. 
-- All the data is in sys.extended_properties but it is not easily handled.
SELECT	Db_Name() [CatalogName],
		@Level0Type [Level0Type],
		@Level0Name [Level0Name],
		@Level1Type [Level1Type],
		@Level1Name [Level1Name],
		@Level2Type [Level2Type],
		@Level2Name [Level2Name],
		[objtype],
		[objname],
		[name],
		[value]
FROM	[fn_listextendedproperty](@PropertyName, @Level0Type, @Level0Name, @Level1Type, @Level1Name, @Level2Type, @Level2Name)
*/

