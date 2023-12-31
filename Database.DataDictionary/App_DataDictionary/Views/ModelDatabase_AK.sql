﻿CREATE VIEW [App_DataDictionary].[ModelDatabase_AK]
WITH SCHEMABINDING AS
-- Enforces rule: DatabaseName must be unique within the Model.
-- Allows methods to determine the correct CatalogId given the ModelId and DatabaseName.
Select	M.[ModelId],
		C.[CatalogId],
		C.[SourceDatabaseName] As [DatabaseName]
From	[App_DataDictionary].[DatabaseCatalog] C
		Inner Join [App_DataDictionary].[ModelCatalog] M
		On	C.[CatalogId] = M.[CatalogId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelDatabase]
    ON [App_DataDictionary].[ModelDatabase_AK]([ModelId] ASC, [DatabaseName] ASC)
GO