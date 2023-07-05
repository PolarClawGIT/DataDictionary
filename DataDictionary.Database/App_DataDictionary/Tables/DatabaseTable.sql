﻿CREATE TABLE [App_DataDictionary].[DatabaseTable]
(
	-- Note: GetSchema combines Views and Tables.
	-- [INFORMATION_SCHEMA] both a [INFORMATION_SCHEMA].[TABLES] & [INFORMATION_SCHEMA].[VIEWS]
	-- but only a [INFORMATION_SCHEMA].[COLUMNS] representing columns from both.
	-- For Referential Integrity purposes, the GetSchema approach is used where both views and tables are returned in a single list.
	-- Temporal and History tables as different table types.
	[CatalogId]         UniqueIdentifier Not Null,
	[SchemaName]        SysName Not Null,
	[TableName]         SysName Not Null,
	[TableType]         NVarChar(60) Null, -- BASE TABLE, VIEW, HISTORY TABLE, TEMPTORAL TABLE
	-- Keys
	CONSTRAINT [PK_DatabaseTable] PRIMARY KEY CLUSTERED ([CatalogId] ASC, [SchemaName] ASC, [TableName] ASC),
	CONSTRAINT [FK_DatabaseTableCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [App_DataDictionary].[DatabaseCatalog] ([CatalogId]),
)
/*
Select	Convert(UniqueIdentifier,Null) As [CatalogId],
		I.[TABLE_CATALOG],
		I.[TABLE_SCHEMA],
		I.[TABLE_NAME],
		Case
			When H.[object_id] is Not Null Then 'HISTORY TABLE'
			When T.[history_table_id] is Not Null Then 'TEMPORAL TABLE'
			Else I.[TABLE_TYPE]
			End As [TABLE_TYPE]
From	[INFORMATION_SCHEMA].[TABLES] I
		Left Join [sys].[Tables] T
		On	I.[TABLE_SCHEMA] = Object_Schema_Name(T.[object_id]) And
			I.[TABLE_NAME] = Object_Name(T.[object_id])
		Left Join [sys].[Tables] H
		On	I.[TABLE_SCHEMA] = Object_Schema_Name(H.[history_table_id]) And
			I.[TABLE_NAME] = Object_Name(H.[history_table_id])
UNION
Select	Convert(UniqueIdentifier,Null) As [CatalogId],
		[TABLE_CATALOG],
		[TABLE_SCHEMA],
		[TABLE_NAME],
		'VIEW' As [TableType]
From	[INFORMATION_SCHEMA].[VIEWS]
*/