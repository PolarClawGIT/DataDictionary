Select	I.[TABLE_CATALOG] As [DatabaseName],
		I.[TABLE_SCHEMA] As [SchemaName],
		I.[TABLE_NAME] As [TableName],
		Case
			When H.[object_id] is Not Null Then 'History Table'
			When T.[history_table_id] is Not Null Then 'Temporal Table'
			When I.[TABLE_TYPE] in ('BASE TABLE') Then 'Table'
			When I.[TABLE_TYPE] in ('VIEW') Then 'View'
			Else I.[TABLE_TYPE]
			End As [TableType]
From	[INFORMATION_SCHEMA].[TABLES] I
	Left Join [sys].[Tables] T
	On	I.[TABLE_SCHEMA] = Object_Schema_Name(T.[object_id]) And
		I.[TABLE_NAME] = Object_Name(T.[object_id])
	Left Join [sys].[Tables] H
	On	I.[TABLE_SCHEMA] = Object_Schema_Name(H.[history_table_id]) And
		I.[TABLE_NAME] = Object_Name(H.[history_table_id])