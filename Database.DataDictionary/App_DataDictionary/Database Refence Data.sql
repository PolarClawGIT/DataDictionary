Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @Data [App_DataDictionary].[typeDatabaseReference]
	Declare @CatalogId UniqueIdentifier,
			@ObjectName NVarChar(512)

	Declare	@SQL NVarChar(Max) = '
			Begin Try;
			Declare @CatalogId UniqueIdentifier
			Select	@CatalogId = [CatalogId]
			From	[DataDictionary].[App_DataDictionary].[DatabaseCatalog]
			Where	[SourceDatabaseName] = Db_Name()

			Select	@CatalogId As [CatalogId],
				NewId() As [DependencyId],
				DB_Name() As [ReferencingDatabaseName],
				Object_Schema_Name(Object_id(@ObjectName)) As [ReferencingSchemaName],
				Object_Name(Object_id(@ObjectName)) As [ReferencingObjectName],
				D.[type_desc] As [ReferencingType],
				IIF(O.[object_id] is Null Or T.[user_type_id] is Null, R.[referenced_database_name], DB_Name()) As [ReferencedDatabaseName],
				R.[referenced_schema_name] As [ReferencedSchemaName],
				R.[referenced_entity_name] As [ReferencedObjectName],
				R.[referenced_minor_name] As [ReferencedColumnName],
				CASE
				WHEN C.[column_id] is not Null THEN ''COLUMN''
				WHEN T.[user_type_id] is not Null And T.[is_table_type] = 1 THEN ''TABLE_TYPE''
				WHEN T.[user_type_id] is not Null THEN ''TYPE''
				WHEN O.[object_id] is not null THEN O.[type_desc]
				ELSE R.[referenced_class_desc] END As [ReferencedType],
				R.[is_caller_dependent] As [IsCallerDependent],
				R.[is_ambiguous] As [IsAmbiguous],
				R.[is_selected] As [IsSelected],
				R.[is_updated] As [IsUpdated],
				R.[is_select_all] As [IsSelectAll],
				R.[is_all_columns_found] As [IsAllColumnsFound],
				R.[is_insert_all] As [IsInsertAll],
				R.[is_incomplete] As [IsIncomplete]
			From [sys].[dm_sql_referenced_entities] (@ObjectName, ''OBJECT'') R -- Must be run for each object. Does not work with Apply.
				Left Join [Sys].[Objects] D -- Referencing
				On	Object_id(@ObjectName) = D.[object_id]
				Left Join [Sys].[Objects] O -- Referenced Objects
				On R.[referenced_id] = O.[object_id]
				Left Join [Sys].[Columns] C -- Refrenced Object Columns
				On R.[referenced_id] = C.[object_id] And R.referenced_minor_id = C.[column_id]
				Left Join [Sys].[Types] T -- Referenced Types
				On	R.[referenced_id] = T.[user_type_id]
			Where R.[is_ambiguous] = 0 -- These cannot be determined except at runtime
			End Try
			Begin Catch
				Throw; -- Try/Catch is needed Because [sys].[dm_sql_referenced_entities] can generate error msg 2020 that the Try/Catch does not trap (by design).
			End Catch;'


Declare @Loop Table ([Control] Int Identity, [SchemaName] SysName Not Null, [ObjectName] SysName Not Null)
Insert Into @Loop ([SchemaName], [ObjectName])
Select	[SchemaName],
		[TableName] As [ObjectName]
From	[App_DataDictionary].[DatabaseTable_AK]
Union
Select	[SchemaName],
		[RoutineName] As [ObjectName]
From	[App_DataDictionary].[DatabaseRoutine_AK]

Declare @Control Int = (Select Min([Control]) From @Loop)

While @Control is not null
Begin
	Select	@ObjectName = FormatMessage('[%s].[%s]',[SchemaName], [ObjectName])
	From	@Loop
	Where	[Control] = @Control

	Insert Into @Data
	Exec [AdventureWorks]..[sp_executesql] @SQL,N'@ObjectName NVarChar(512)', @ObjectName = @ObjectName

	Insert Into @Data
	Exec [SampleDatabase]..[sp_executesql] @SQL,N'@ObjectName NVarChar(512)', @ObjectName = @ObjectName

	Set	@Control = (Select Min([Control]) From @Loop Where [Control] > @Control)
End


--Select '@Data', * From @Data
Exec [DataDictionary].[App_DataDictionary].[procSetDatabaseReference] @Data = @Data


	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	--Throw;
End Catch;
