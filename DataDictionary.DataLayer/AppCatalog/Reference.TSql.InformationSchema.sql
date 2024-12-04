Set NoCount On;

Declare @Object Table (
	[object_id] int Not Null,
	[SchemaName] SysName Not Null,
	[ObjectName] SysName Not Null,
	Primary Key ([object_id]))

Declare @Result Table (
	-- [sys].[dm_sql_referenced_entities]
	[referencing_id]			int Not Null,
	[referencing_minor_id]		int Null,
	[referenced_server_name]	sysname Null,
	[referenced_database_name]	sysname Null,
	[referenced_schema_name]	sysname Null,
	[referenced_entity_name]	sysname Null,
	[referenced_minor_name]		sysname Null,
	[referenced_id]				int Null,
	[referenced_minor_id]		int Null,
	[referenced_class]			tinyint Null,
	[referenced_class_desc]		nvarchar(60) Null,
	[is_caller_dependent]		bit Null,
	[is_ambiguous]				bit Null,
	[is_selected]				bit Null,
	[is_updated]				bit Null,
	[is_select_all]				bit Null,
	[is_all_columns_found]		bit Null,
	[is_insert_all]				bit Null,
	[is_incomplete]				bit Null)

Declare @object_id int,
		@referencing_entity_name  NVarChar(517)

Insert Into @Object
Select	[object_id],
		Object_Schema_Name([object_id]),
		Object_Name([object_id])
From	[Sys].[Objects]
Where	[type_desc] In (
		'AGGREGATE_FUNCTION',
		--'CHECK_CONSTRAINT',
		--'CLR_SCALAR_FUNCTION',
		--'CLR_STORED_PROCEDURE',
		--'CLR_TABLE_VALUED_FUNCTION',
		--'CLR_TRIGGER',
		--'DEFAULT_CONSTRAINT',
		--'EDGE_CONSTRAINT',
		--'EXTENDED_STORED_PROCEDURE',
		--'FOREIGN_KEY_CONSTRAINT',
		--'INTERNAL_TABLE',
		--'PLAN_GUIDE',
		--'PRIMARY_KEY_CONSTRAINT',
		--'REPLICATION_FILTER_PROCEDURE',
		--'RULE',
		--'SEQUENCE_OBJECT',
		--'SERVICE_QUEUE',
		'SQL_INLINE_TABLE_VALUED_FUNCTION',
		'SQL_SCALAR_FUNCTION',
		'SQL_STORED_PROCEDURE',
		'SQL_TABLE_VALUED_FUNCTION',
		'SQL_TRIGGER',
		--'SYNONYM',
		--'SYSTEM_TABLE',
		--'TYPE_TABLE',
		--'UNIQUE_CONSTRAINT',
		'USER_TABLE',
		'VIEW')

Select	@object_id = Min([object_id])
From	@Object

While @object_id is not null
Begin -- [sys].[dm_sql_referenced_entities] Must be executed for each item or it will generated errors (msg 2020)
	Select	@referencing_entity_name = FormatMessage('[%s].[%s]', [SchemaName], [ObjectName])
	From	@Object
	Where	[object_id] = @object_id

	Begin Try -- [sys].[dm_sql_referenced_entities] can throw exceptions
		Insert Into @Result
		Select	@object_id As [referencing_id],
				NullIf([referencing_minor_id],0) As [referencing_minor_id],
				[referenced_server_name],
				[referenced_database_name],
				[referenced_schema_name],
				[referenced_entity_name],
				[referenced_minor_name],
				[referenced_id],
				NullIf([referenced_minor_id],0) As [referenced_minor_id],
				[referenced_class],
				[referenced_class_desc],
				[is_caller_dependent],
				[is_ambiguous],
				[is_selected],
				[is_updated],
				[is_select_all],
				[is_all_columns_found],
				[is_insert_all],
				[is_incomplete]
		From	[sys].[dm_sql_referenced_entities] (@referencing_entity_name, 'OBJECT') 
		Where	[is_ambiguous] = 0
	End Try
	Begin Catch
		-- Discard anything that generated an exception
		Print FormatMessage('%s threw an Exception and was discarded', @referencing_entity_name)
	End Catch

	Select	@object_id = Min([object_id])
	From	@Object
	Where	[object_id] > @object_id
End

Select	DB_Name() As [DatabaseName],
		S.[SchemaName],
		S.[ObjectName],
		-- Column Name cannot be determined
		D.[type_desc] As [ObjectType],
		IIF(O.[object_id] is Null And T.[user_type_id] is Null, R.[referenced_database_name], DB_Name()) As [ReferencedDatabaseName],
		R.[referenced_schema_name] As [ReferencedSchemaName],
		R.[referenced_entity_name] As [ReferencedObjectName],
		R.[referenced_minor_name] As [ReferencedColumnName],
		Convert(NVarChar(60), CASE
			WHEN C.[column_id] is not Null THEN N'COLUMN'
			WHEN T.[user_type_id] is not Null And T.[is_table_type] = 1 THEN N'TABLE_TYPE'
			WHEN T.[user_type_id] is not Null THEN N'TYPE'
			WHEN O.[object_id] is not null THEN Convert(NVarChar(60),O.[type_desc]) Collate database_default 
			ELSE Convert(NVarChar(60),R.[referenced_class_desc]) Collate database_default 
			END) As [ReferencedType],
		R.[is_caller_dependent] As [IsCallerDependent],
		R.[is_ambiguous] As [IsAmbiguous],
		R.[is_selected] As [IsSelected],
		R.[is_updated] As [IsModified],
		R.[is_select_all] As [IsSelectAll],
		R.[is_all_columns_found] As [IsAllColumnsFound],
		R.[is_insert_all] As [IsInsertAll],
		R.[is_incomplete] As [IsIncomplete]
From	@Object S
		Inner Join @Result R
		On	S.[object_id] = R.[referencing_id]
		Left Join [Sys].[Objects] D -- Referencing
		On	R.[referencing_id] = D.[object_id]
		Left Join [Sys].[Objects] O -- Referenced Objects
		On R.[referenced_id] = O.[object_id]
		Left Join [Sys].[Columns] C -- Referenced Object Columns
		On R.[referenced_id] = C.[object_id] And R.referenced_minor_id = C.[column_id]
		Left Join [Sys].[Types] T -- Referenced Types
		On	R.[referenced_id] = T.[user_type_id]
Union -- Temporal Tables
Select	DB_Name() As [DatabaseName],
		Object_Schema_Name(H.[object_id]) As [SchemaName],
		Object_Name(H.[object_id]) As [ObjectName],
		H.[type_desc] As [ObjectType],
		DB_Name() As [ReferencedDatabaseName],
		Object_Schema_Name(T.[object_id]) As [ReferencedSchemaName],
		Object_Name(T.[object_id]) As [ReferencedObjectName],
		Convert(SysName, Null) As [ReferencedColumnName],
		T.[type_desc] As [ReferencedType],
		Convert(Bit, 0) As [IsCallerDependent],
		Convert(Bit, 0) As [IsAmbiguous],
		Convert(Bit, 1) As [IsSelected],
		Convert(Bit, 0) As [IsModified],
		Convert(Bit, 0) As [IsSelectAll],
		Convert(Bit, 1) As [IsAllColumnsFound],
		Convert(Bit, 0) As [IsInsertAll],
		Convert(Bit, 0) As [IsIncomplete]
From	[sys].[Tables] H
		Inner Join [sys].[Tables] T
		On H.[object_id] = T.[history_table_id]
Union -- Temporal Table Columns
Select	DB_Name() As [DatabaseName],
		Object_Schema_Name(H.[object_id]) As [SchemaName],
		Object_Name(H.[object_id]) As [ObjectName],
		H.[type_desc] As [ObjectType],
		DB_Name() As [ReferencedDatabaseName],
		Object_Schema_Name(T.[object_id]) As [ReferencedSchemaName],
		Object_Name(T.[object_id]) As [ReferencedObjectName],
		I.[name] As [ReferencedColumnName],
		Convert(NVarChar(60), 'COLUMN') As [ReferencedType],
		Convert(Bit, 0) As [IsCallerDependent],
		Convert(Bit, 0) As [IsAmbiguous],
		Convert(Bit, IIF(C.[is_hidden] = 1,0,1)) As [IsSelected],
		Convert(Bit, 0) As [IsModified],
		Convert(Bit, 0) As [IsSelectAll],
		Convert(Bit, 1) As [IsAllColumnsFound],
		Convert(Bit, 0) As [IsInsertAll],
		Convert(Bit, 0) As [IsIncomplete]
From	[sys].[Tables] H
		Inner Join [sys].[Tables] T
		On H.[object_id] = T.[history_table_id]
		Left Join [Sys].[Columns] C -- Temporal Table Columns
		On T.[object_id] = C.[object_id]
		Left Join [Sys].[Columns] I -- History Table Columns
		On H.[object_id] = I.[object_id] And C.[name] = I.[name]
