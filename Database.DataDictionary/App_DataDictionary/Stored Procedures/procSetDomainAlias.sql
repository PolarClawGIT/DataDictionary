CREATE PROCEDURE [App_DataDictionary].[procSetDomainAlias]
		@Data [App_DataDictionary].[typeDomainAlias] ReadOnly
With RECOMPILE
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAlias.
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

Print FormatMessage ('Debug- Start %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

	Declare @Parse Table (
		[ParseId] Int Not Null IDentity,		
		[AliasParentName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasChildName] [App_DataDictionary].[typeNameSpace] Null,
		[ParentCheckSum] As Binary_CheckSum([AliasParentName]),
		[ChildCheckSum] As Binary_CheckSum([AliasChildName]),
		Primary Key ([ParseId]),
		Unique ([ChildCheckSum], [ParseId]),
		Unique ([ParentCheckSum], [ParseId]))


	;With [Parse] As (
		Select	[AliasName] As [AliasParentName],
				Convert(NVarChar(Max),Null) As [AliasChildName]
		From	@Data
		Union All
		Select	N.[AliasParentName],
				P.[AliasParentName] As [AliasChildName]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[AliasParentName]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[AliasParentName])) = 0  Then Null
						-- No more brackets
						When CharIndex(']',Reverse(P.[AliasParentName])) = 0 And
							CharIndex('[',Reverse(P.[AliasParentName])) = 0  
							Then Left(P.[AliasParentName], Len(P.[AliasParentName]) - CharIndex('.',Reverse(P.[AliasParentName])) -0)
						-- Period is after the bracket
						When CharIndex(']',Reverse(P.[AliasParentName])) > 0 And
							CharIndex(']',Reverse(P.[AliasParentName])) > CharIndex('.', Reverse(P.[AliasParentName]))
							Then Left(P.[AliasParentName], Len(P.[AliasParentName]) - CharIndex('.',Reverse(P.[AliasParentName])) -0)
						-- Period is before the bracket
						When CharIndex(']',Reverse(P.[AliasParentName])) > 0 And
							CharIndex('[.', Reverse(P.[AliasParentName])) > 0
							Then Left(P.[AliasParentName], Len(P.[AliasParentName]) - CharIndex('[.',Reverse(P.[AliasParentName])) -1)
						Else Null
						End As [AliasParentName] ) N
		Where	NullIf(P.[AliasParentName],'') is Not Null)
	Insert Into @Parse ([AliasParentName], [AliasChildName])
	Select	[AliasParentName],
			[AliasChildName]
	From	[Parse]

Print FormatMessage ('Debug- Insert @Parse: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

	Declare @Format Table (-- Work Table to address peformance issues
		[FormatId] Int Not Null IDentity,		
		[AliasParentName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasChildName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Null,
		[ParentCheckSum] As Binary_CheckSum([AliasParentName]),
		[ChildCheckSum] As Binary_CheckSum([AliasChildName]),
		Primary Key ([FormatId]),
		Unique ([ChildCheckSum], [FormatId]),
		Unique ([ParentCheckSum], [FormatId]))


	;With [Format] As (
		Select	[AliasParentName],
				[AliasChildName],
				Replace(Replace(
					IIF([AliasParentName] is Null,
						[AliasChildName],
						Right([AliasChildName],Len([AliasChildName]) - Len([AliasParentName]) -1)),
					'[',''),']','')
					As [AliasElementName]
		From	@Parse
		Where	[AliasChildName] is Not Null)
	Insert Into @Format ([AliasParentName], [AliasChildName], [AliasElementName])
	Select	[AliasParentName],
			[AliasChildName],
			[AliasElementName]
	From	[Format]

Print FormatMessage ('Debug- Insert @Format: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

	Declare @Tree Table ( -- Work Table to address peformance issues.
		[TreeId] Int Not Null Identity,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Null,
		[AliasParentName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasChildName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasName] [App_DataDictionary].[typeNameSpace] Null,
		[ParentCheckSum] As Binary_CheckSum([AliasParentName]),
		[ChildCheckSum] As Binary_CheckSum([AliasChildName]),
		Primary Key ([TreeId]),
		Unique ([ChildCheckSum], [TreeId]),
		Unique ([ParentCheckSum], [TreeId]))

	-- TODO: Currently the most expensive/slowest step. This needs to be re-thought.
	;With [Tree] As (
		Select	[AliasElementName],
				[AliasParentName],
				[AliasChildName],
				FormatMessage('[%s]',[AliasElementName]) As [AliasName]
		From	@Format
		Where	[AliasParentName] is Null
		Union All
		Select	F.[AliasElementName],
				F.[AliasParentName],
				F.[AliasChildName],
				FormatMessage('%s.[%s]',T.[AliasName],F.[AliasElementName]) As [AliasName]
		From	[Tree] T
				Inner Join @Format F
				On	T.[AliasChildName] = F.[AliasParentName])
	Insert Into @Tree ([AliasElementName], [AliasParentName], [AliasChildName], [AliasName])
	Select	Distinct
			[AliasElementName],
			[AliasParentName],
			[AliasChildName],
			[AliasName]
	From	[Tree]

Print FormatMessage ('Debug- Insert @Tree: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

Declare @RowId Table (
		[WorkId] Int Not Null,
		[SubId] Int Not Null,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Null,
		[AliasName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasParentName] [App_DataDictionary].[typeNameSpace] Null,
		[AliasChildName] [App_DataDictionary].[typeNameSpace] Null,
		[ParentCheckSum] As Binary_CheckSum([AliasParentName]),
		[ChildCheckSum] As Binary_CheckSum([AliasChildName]),
		Primary Key ([WorkId], [SubId]),
		Unique ([ChildCheckSum], [WorkId], [SubId]),
		Unique ([ParentCheckSum], [WorkId], [SubId]))

	;With [RowId] As (
		Select	Dense_Rank() Over (Order By [AliasName]) As [WorkId],
				[AliasElementName],
				[AliasName],
				[AliasParentName],
				[AliasChildName]
		From	@Tree)
	Insert Into @RowId ([WorkId], [SubId], [AliasElementName], [AliasName], [AliasParentName], [AliasChildName])
	Select	[WorkId],
			Row_Number() Over (Partition By [WorkId] Order By [AliasElementName]) As [SubId],
			[AliasElementName],
			[AliasName],
			[AliasParentName],
			[AliasChildName]
	From	[RowId]

Print FormatMessage ('Debug- Insert @RowId: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

	Declare @Value Table (
		[AliasId] UniqueIdentifier Not Null,
		[WorkId] Int Not Null,
		[ParentWorkId] Int Null,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
		[AliasName] [App_DataDictionary].[typeNameSpace] Not Null,
		[AliasCheckSum] As Binary_CheckSum([AliasName]),
		Primary Key ([WorkId]),
		Unique ([AliasId]),
		Unique ([AliasCheckSum], [AliasId]))

	;With [Source] As (
		Select	Distinct -- Suspected of being very slow
				C.[WorkId],
				P.[WorkId] As [ParentWorkId],
				C.[AliasElementName],
				C.[AliasName]
		From	@RowId C
				Left Join @RowId P
				On	C.[ParentCheckSum] = P.[ChildCheckSum] And
					C.[AliasParentName] = P.[AliasChildName]) 
	Insert Into @Value
	Select	NewID() As [AliasId],
			[WorkId],
			[ParentWorkId],
			[AliasElementName],
			[AliasName]
	From	[Source]


Print FormatMessage ('Debug- Insert @Value: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

	-- A Direct Join on CTE Using a NVarChar(Max) does not work, for unknown reasons.
	-- Making a Temp Table resolves issue.
	Declare @Target Table (
		[AliasId]          UniqueIdentifier Not Null,
		[AliasParentId]    UniqueIdentifier Null,
		[AliasElementName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
		[AliasName]        [App_DataDictionary].[typeNameSpace] Not Null,
		[AliasCheckSum] As Binary_CheckSum([AliasName]),
		Primary Key ([AliasId]),
		Unique ([AliasCheckSum], [AliasId]))

	; With [Target] As (
		Select	[AliasId],
				[AliasParentId],
				[AliasElementName],
				Convert(NVarChar(Max),FormatMessage('[%s]',[AliasElementName])) As [AliasName]
		From	[App_DataDictionary].[DomainAlias]
		Where	[AliasParentId] is Null
		Union All
		Select	A.[AliasId],
				A.[AliasParentId],
				A.[AliasElementName],
				Convert(NVarChar(Max),FormatMessage('%s.[%s]',T.[AliasName], A.[AliasElementName])) As [AliasName]
		From	[Target] T
				Inner Join [App_DataDictionary].[DomainAlias] A
				On	T.[AliasId] = A.[AliasParentId])
	Insert Into @Target ([AliasId], [AliasParentId], [AliasElementName], [AliasName])
	Select	[AliasId],
			[AliasParentId],
			[AliasElementName],
			[AliasName]
	From	[Target]

	Print FormatMessage ('Debug- Insert @Target: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));;

	Insert Into [App_DataDictionary].[DomainAlias]
	Select	IsNull(T.[AliasId], S.[AliasId]) As [AliasId],
			IsNull(G.[AliasId], P.[AliasId]) As [AliasParentId],
			S.[AliasElementName]
	From	@Value S
			Left Join @Value P
			On	S.[ParentWorkId] = P.[WorkId]
			Left Join @Target T
			On	S.[AliasCheckSum] = T.[AliasCheckSum] and
				S.[AliasName] = T.[AliasName]
			Left Join @Target G
			On	P.[AliasCheckSum] = G.[AliasCheckSum] And
				P.[AliasName] = G.[AliasName]
	Where	T.[AliasId] Is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate(),121));

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

/* DEBUG Script.

Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare	@Data [App_DataDictionary].[typeDomainAlias]
	Insert Into @Data Values
		('UnitTest.DataDictionary.App_DataDictionary.Help.[Help.Id]'),
		('UnitTest.DataDictionary.[App.DataDictionary].Help.[Help.Id]'),
		('UnitTest.DataDictionary.App_DataDictionary.Help.HelpId'),
		('UnitTest.[DataDictionary].[App_DataDictionary].[Help].[HelpId]')

	Exec [App_DataDictionary].[procSetDomainAlias] @Data
Print 'Debug 0';	
/*
	Select	*
	From	[App_DataDictionary].[ModelNameSpace]
	Where	[AliasName] Like 'UnitTest%'*/
Print 'Debug 1';	
	Insert Into @Data Values
		('UnitTest.[DataDictionary.App_DataDictionary]'),
		('UnitTest.DataDictionary.App_DataDictionary'),
		('UnitTest.DataDictionary.[App_DataDictionary].Help.[HelpId]'),
		('UnitTest.[DataDictionary.App_DataDictionary].Help.HelpId'),
		('UnitTest.DataDictionary.App_DataDictionary.Help.HelpId'),
		('UnitTest.[DataDictionary].[App_DataDictionary].[Help].[HelpId]')

Print 'Debug 2';	

	Exec [App_DataDictionary].[procSetDomainAlias] @Data

	/*
	-- Stress Test. Can it handle a small database.
	Insert Into @Data 
	Select	FormatMessage('(''[%s].[%s].[%s].[%s]''),', Db_Name(), object_Schema_Name(object_id), object_Name(object_id), name)
	From	Sys.Columns
	Where	object_Schema_Name(object_id) Not like 'sys'
	Union
	Select	FormatMessage('(''[%s].[%s].[%s]''),', Db_Name(), object_Schema_Name(object_id), object_Name(object_id))
	From	Sys.tables
	Where	object_Schema_Name(object_id) Not like 'sys'*/

Print 'Debug 4';	
	Select	*
	From	[App_DataDictionary].[ModelNameSpace]
	Where	[AliasName] Like 'UnitTest.%'
Print 'Debug 5';		

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