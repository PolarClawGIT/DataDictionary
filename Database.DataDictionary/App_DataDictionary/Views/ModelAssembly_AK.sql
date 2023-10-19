CREATE VIEW [App_DataDictionary].[ModelAssembly_AK]
WITH SCHEMABINDING AS
-- Enforces rule: Assembly Name must be unique within the Model.
-- Allows methods to determine the correct CatalogId given the ModelId and Assembly Name.
Select	M.[ModelId],
		C.[LibraryId],
		C.[AssemblyName]
From	[App_DataDictionary].[LibrarySource] C
		Inner Join [App_DataDictionary].[ModelLibrary] M
		On	C.[LibraryId] = M.[LibraryId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelAssembly]
    ON [App_DataDictionary].[ModelAssembly_AK]([ModelId] ASC, [AssemblyName] ASC)
GO