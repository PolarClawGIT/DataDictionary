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

Declare @Delimiter NVarChar(10) = '.'

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeDomainAttributeAlias]
	Insert Into @Values
	Select	Coalesce(D.[AttributeId], @AttributeId) As [AttributeId],
			NullIf(Trim(D.[AliasName]),'') As [AliasName],
			NullIf(Trim(D.[ScopeName]),'') As [ScopeName]
	From	@Data D
	Where	(@AttributeId is Null or D.[AttributeId] = @AttributeId) And
			(@ModelId is Null or @ModelId In (
				Select	[ModelId]
				From	[App_DataDictionary].[ModelAttribute]
				Where	(@AttributeId is Null Or [AttributeId] = @AttributeId)))

	-- Validation
--	If @ModelId is Null and @AttributeId is Null
--	Throw 50000, '@ModelId or @AttributeId must be specified', 1;

	Declare @Merge Table (
		[AliasId] UniqueIdentifier Not Null,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
		[AliasName] NVarChar(Max) Not Null,
		[AliasParentName] NVarChar(Max) Null
		Primary Key ([AliasId]))

	;With [Parse] As (
		-- Create NameSpaces for each level
		Select	N.[NameSpace]
		From	@Values V
				Outer Apply [App_DataDictionary].[funcSplitNameSpace](V.[AliasName],@Delimiter) N
		Group By N.[NameSpace]),
	[Alias] As (
		Select	Coalesce(D.[AliasId], NewId()) As [AliasId],
				A.[AliasElementName],
				A.[AliasName],
				A.[AliasParentName],
				Binary_CheckSum(IsNull(Upper(A.[AliasParentName]), '<ROOT>')) As [AliasCheckSum]
		From	[Parse] P
				Outer Apply (
					Select	IIF(CharIndex(@Delimiter,[NameSpace]) > 0,
								Right([NameSpace], CharIndex(@Delimiter,Reverse([NameSpace])) -1),
								[NameSpace])
								As [AliasElementName],
							[NameSpace] As [AliasName],
							IIF(CharIndex(@Delimiter,[NameSpace]) > 0,
								Left([NameSpace],Len([NameSpace]) - CharIndex(@Delimiter,Reverse([NameSpace]))),
								Null)
								As [AliasParentName]) A
				Left Join [App_DataDictionary].[DomainNameSpace] D
				On	A.[AliasName] = D.[AliasName])
	-- This is necessary to create a concert GUID that does not change.
	-- Otherwise, the GUID is not concret until after the statement executes. 
	-- Not as expected.
	Insert Into @Merge
	Select	[AliasId],
			[AliasElementName],
			[AliasName],
			[AliasParentName]
	From	[Alias]

	;With [Value] As (
		Select	A.[AliasId],
				P.[AliasId] As [AliasParentId],
				A.[AliasElementName]
		From	@Merge A
				Left Join @Merge P
				On	A.[AliasParentName] = P.[AliasName]),
	[Delta] As (
		Select	[AliasId],
				[AliasParentId],
				[AliasElementName]
		From	[Value]
		Except
		Select	[AliasId],
				[AliasParentId],
				[AliasElementName]
		From	[App_DataDictionary].[DomainAlias])
	Merge [App_DataDictionary].[DomainAlias] T
	Using [Delta] S
	On	T.[AliasId] = S.[AliasId]
	When Matched Then Update Set
		[AliasParentId] = S.[AliasParentId],
		[AliasElementName] = S.[AliasElementName]
	When Not Matched by Target Then
		Insert ([AliasId], [AliasParentId], [AliasElementName])
		Values ([AliasId], [AliasParentId], [AliasElementName]);
Print FormatMessage ('Delete [App_DataDictionary].[DomainAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Values] As (
		Select	V.[AttributeId],
				N.[AliasId],
				S.[ScopeId]
		From	@Values V
				Inner Join [App_DataDictionary].[DomainNameSpace] N
				On	V.[AliasName] = N.[AliasName]
				Inner Join [App_DataDictionary].[DomainScope] S
				On	V.[ScopeName] = S.[ScopeName]),
	[Delta] As (
		Select	[AttributeId],
				[AliasId],
				[ScopeId]
		From	[Values]
		Except
		Select	[AttributeId],
				[AliasId],
				[ScopeId]
		From	[App_DataDictionary].[DomainAttributeAlias]),
	[Data] As (
		Select	V.[AttributeId],
				V.[AliasId],
				V.[ScopeId],
				IIF(D.[AttributeId] is Null,0, 1) As [IsDiffrent]
		From	[Values] V
				Left Join [Delta] D
				On	V.[AttributeId] = D.[AttributeId] And
					V.[AliasId] = D.[AliasId])
	Merge [App_DataDictionary].[DomainAttributeAlias] T
	Using [Data] S
	On	T.[AttributeId] = T.[AttributeId] And
		T.[AliasId] = T.[AliasId]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[ScopeId] = S.[ScopeId]
	When Not Matched by Target Then
		Insert ([AttributeId], [AliasId], [ScopeId])
		Values ([AttributeId], [AliasId], [ScopeId])
	When Not Matched by Source And
		(@AttributeId = T.[AttributeId] Or
		 T.[AttributeId] In (
			Select	[AttributeId]
			From	[App_DataDictionary].[ModelAttribute]
			Where	[ModelId] = @ModelId))
		Then Delete;
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

