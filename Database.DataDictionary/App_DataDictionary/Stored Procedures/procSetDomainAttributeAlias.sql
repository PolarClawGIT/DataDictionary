CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainAttributeAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAttributeAlias.
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

	-- Clean Data
	Declare @Alias [App_DataDictionary].[typeDomainAlias]

	Declare @Values Table (
		[AttributeId]       UniqueIdentifier Not Null,
		[AliasId]           UniqueIdentifier Not Null,
		[AliasName]	        [App_DataDictionary].[typeNameSpacePath] Not Null,
		[ScopeName]         [App_DataDictionary].[typeScopeName] Null,
		Primary Key ([AttributeId], [AliasId]))

	Insert Into @Alias
	Select	[AliasName],
			[ScopeName]
	From	@Data
	Group By [AliasName],
			[ScopeName]

	Exec [App_DataDictionary].[procSetDomainAlias] @Alias

	;With [Alias] As (
		Select	A.[AliasId],
				F.[AliasName],
				F.[ScopeName]
		From	[App_DataDictionary].[DomainAlias] A
				Cross Apply [App_DataDictionary].[funcGetAliasName](A.[AliasId]) F)
	Insert Into @Values
	Select	Coalesce(D.[AttributeId], @AttributeId, NewId()) As [AttributeId],
			A.[AliasId],
			A.[AliasName],
			D.[ScopeName]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitNameSpace](D.[AliasName]) N
			Left Join [Alias] A
			On	N.[NameSpace] = A.[AliasName]
	Where	N.[IsBase] = 1

	-- Apply Changes
	Delete From [App_DataDictionary].[DomainAttributeAlias]
	From	[App_DataDictionary].[DomainAttributeAlias] T
			Left Join @Values V
			On	T.[AttributeId] = V.[AttributeId] And
				T.[AliasId] = V.[AliasId]
	Where	V.[AttributeId] is Null And
			T.[AttributeId] In (
			Select	A.[AttributeId]
			From	[App_DataDictionary].[DomainAttribute] A
					Left Join [App_DataDictionary].[ModelAttribute] C
					On	A.[AttributeId] = C.[AttributeId]
			Where	(@AttributeId is Null Or @AttributeId = A.[AttributeId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainAttributeAlias] ([AttributeId], [AliasId])
	Select	V.[AttributeId],
			V.[AliasId]
	From	@Values V
			Left Join [App_DataDictionary].[DomainAttributeAlias] T
			On	V.[AttributeId] = T.[AttributeId] And
				V.[AliasId] = T.[AliasId]
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

declare @Model App_DataDictionary.typeModel
insert into @Model values('D13DBF75-1C92-4ABE-B07D-A9C23835E43A',N'Sample Model',NULL)

exec [App_DataDictionary].[procSetModel] @ModelId='D13DBF75-1C92-4ABE-B07D-A9C23835E43A',@Data=@Model

declare @Attribute App_DataDictionary.typeDomainAttribute
insert into @Attribute values('E8F4AFF5-DE14-4E4F-93BC-D1ED4C48C1CA',N'SampleAttribute',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)

exec [App_DataDictionary].[procSetDomainAttribute] @ModelId='D13DBF75-1C92-4ABE-B07D-A9C23835E43A',@Data=@Attribute

declare @Alias App_DataDictionary.typeDomainAttributeAlias
insert into @Alias values('E8F4AFF5-DE14-4E4F-93BC-D1ED4C48C1CA',N'[SampleLibrary].[SampleClass].[SampleProperty]',N'Library.NameSpace.Type.Property')

insert into @Alias values('E8F4AFF5-DE14-4E4F-93BC-D1ED4C48C1CA',N'[SampleLibrary].[SampleClass].[Dummy1].[Dummy2].[Dummy3].[Dummy4].[InnerClass].[SampleProperty]',N'Library.NameSpace.Type.Property')


insert into @Alias values('E8F4AFF5-DE14-4E4F-93BC-D1ED4C48C1CA',N'[SampleLibrary].[SampleClass].[SampleMethod].[sampleParm01]',N'Library.NameSpace.Type.Parameter')
insert into @Alias values('E8F4AFF5-DE14-4E4F-93BC-D1ED4C48C1CA',N'[SampleLibrary].[SampleClass].[SampleGenericMethod``1].[data]',N'Library.NameSpace.Type.Parameter')

exec [App_DataDictionary].[procSetDomainAttributeAlias] @ModelId='D13DBF75-1C92-4ABE-B07D-A9C23835E43A',@Data=@Alias

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
*/