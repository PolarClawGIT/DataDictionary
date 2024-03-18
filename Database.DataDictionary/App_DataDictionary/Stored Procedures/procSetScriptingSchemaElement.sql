CREATE PROCEDURE [App_DataDictionary].[procSetScriptingSchemaElement]
		@SchemaId UniqueIdentifier = Null,
		@ElementId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingSchemaElement] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingSchemaElement.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Clean the Data, helps performance
	Declare @Values Table (
			[ElementId]             UniqueIdentifier NOT NULL,
			[SchemaId]              UniqueIdentifier NOT NULL,
			[ScopeId]               Int Not Null,
			[ColumnName]            SysName Not Null,
			[DataName]              SysName Null,
			[DataType]              SysName Null,
			[DataNillable]          Bit Null,
			[AsElement]             Bit Null,
			[DataAsText]            Bit Not Null,
			[DataAsCData]           Bit Not Null,
			[DataAsXml]             Bit Not Null,
			Primary Key ([ElementId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	X.[ElementId],
			X.[SchemaId],
			S.[ScopeId],
			D.[ColumnName],
			D.[DataName],
			D.[DataType],
			D.[DataNillable],
			D.[AsElement],
			D.[DataAsText],
			D.[DataAsCData],
			D.[DataAsXml]
	From	@Data D
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Cross apply (
				Select	Coalesce(D.[ElementId], @ElementId, NewId()) As [ElementId],
						Coalesce(D.[SchemaId], @SchemaId) As [SchemaId]) X
	Where	(@ElementId is Null or X.[ElementId] = @ElementId) And
			(@SchemaId is Null or X.[SchemaId] = @SchemaId)

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingSchemaElement]
	From	[App_DataDictionary].[ScriptingSchemaElement] T
			Left Join @Values S
			On	T.[ElementId] = S.[ElementId]
	Where	S.[ElementId] is Null And
			(@ElementId is Null or T.[ElementId] = @ElementId)
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ElementId],
				[SchemaId],
				[ScopeId],
				[ColumnName],
				[DataName],
				[DataType],
				[DataNillable],
				[AsElement],
				[DataAsText],
				[DataAsCData],
				[DataAsXml]
		From	@Values
		Except
		Select	[ElementId],
				[SchemaId],
				[ScopeId],
				[ColumnName],
				[ElementName],
				[ElementType],
				[ElementNillable],
				[AsElement],
				[DataAsText],
				[DataAsCData],
				[DataAsXml]
		From	[App_DataDictionary].[ScriptingSchemaElement])
	Update [App_DataDictionary].[ScriptingSchemaElement]
	Set		[SchemaId] = S.[SchemaId],
			[ScopeId] = S.[ScopeId],
			[ColumnName] = S.[ColumnName],
			[ElementName] = S.[DataName],
			[ElementType] = S.[DataType],
			[ElementNillable] = S.[DataNillable],
			[AsElement] = S.[AsElement],
			[DataAsText] = S.[DataAsText],
			[DataAsCData] = S.[DataAsCData],
			[DataAsXml] = S.[DataAsXml]
	From	[App_DataDictionary].[ScriptingSchemaElement] T
			Inner Join [Delta] S
			On	T.[ElementId] = S.[ElementId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingSchemaElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingSchemaElement] (
			[ElementId],
			[SchemaId],
			[ScopeId],
			[ColumnName],
			[ElementName],
			[ElementType],
			[ElementNillable],
			[AsElement],
			[DataAsText],
			[DataAsCData],
			[DataAsXml])
	Select	S.[ElementId],
			S.[SchemaId],
			S.[ScopeId],
			S.[ColumnName],
			S.[DataName],
			S.[DataType],
			S.[DataNillable],
			S.[AsElement],
			S.[DataAsText],
			S.[DataAsCData],
			S.[DataAsXml]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingSchemaElement] T
			On	S.[ElementId] = T.[ElementId]
	Where	T.[ElementId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingSchemaElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Commit Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, commit it
		If XAct_State() = -1 Throw 103930, 'The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction. (Msg- 3930)', 100
		Commit Transaction
		Print FormatMessage ('Commit Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Commit Transaction
	  -- This is a nested transaction, must be committed by outer transaction
	Else Print FormatMessage ('Commit Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
End Try
Begin Catch
	-- Debug Data
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID))
	Print FormatMessage (' Message- %s', ERROR_MESSAGE())
	Print FormatMessage (' Number- %i', ERROR_NUMBER())
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY())
	Print FormatMessage (' State- %i', ERROR_STATE())
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE())
	Print FormatMessage (' Line- %i', ERROR_LINE())
	Print FormatMessage (' @@TranCount - %i', @@TranCount)
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel)
	Print FormatMessage (' Original_Login - %s', Original_Login())
	Print FormatMessage (' Current_User - %s', Current_User)
	Print FormatMessage (' XAct_State - %i', XAct_State())
	Print '*** Debug Report ***'

	Print FormatMessage ('*** End Report: %s ***', Object_Name(@@ProcID))

	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_SEVERITY() Not In (0, 11) Throw -- Re-throw the Error
End Catch
GO