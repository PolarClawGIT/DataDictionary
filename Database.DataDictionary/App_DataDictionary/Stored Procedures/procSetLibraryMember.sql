﻿CREATE PROCEDURE [App_DataDictionary].[procSetLibraryMember]
		@ModelId UniqueIdentifier = null,
		@LibraryId UniqueIdentifier = null,
		@Data [App_DataDictionary].[typeLibraryMember] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on LibraryMember.
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeLibraryMember]
	Insert Into @Values
	Select	P.[LibraryId],
			P.[AssemblyName],
			NullIf(Trim(D.[MemberNameSpace]),'') As [MemberNameSpace],
			NullIf(Trim(D.[MemberName]),'') As [MemberName],
			NullIf(Trim(D.[MemberType]),'') As [MemberType],
			D.[MemberData]
	From	@Data D
			Left Join [App_DataDictionary].[LibrarySource] P
			On	D.[AssemblyName] = P.[AssemblyName]
	Where	(@LibraryId is Null or P.[LibraryId] = @LibraryId)

	-- Validation
	If @ModelId is Null and @LibraryId is Null
	Throw 50000, '@ModelId or @LibraryId must be specified', 1;

	If Exists (
		Select	[LibraryId], [MemberNameSpace], [MemberName]
		From	@Values
		Group By [LibraryId], [MemberNameSpace], [MemberName]
		Having	Count(*) > 1)
	Throw 50000, '[MemberName] cannot be duplicate', 2;

	-- Work out the Member NameSpace and assign ID
	Declare @NameSpace Table (
			[LibraryId] UniqueIdentifier Not Null,
			[NameSpaceId] Int Not Null,
			[ParentId] Int Null,
			[NameSpace] NVarChar(500) Not Null,
			[MemberNameSpace] NVarChar(Max) Not Null,
			Primary Key ([LibraryId], [NameSpaceId]))

	;with [NameSpace] As (
		Select	[LibraryId],
				[MemberNameSpace],
				IIF(CharIndex('.',[MemberNameSpace]) > 0,
					Left([MemberNameSpace],Len([MemberNameSpace]) - CharIndex('.',Reverse([MemberNameSpace]))),
					Null)
					As[ParentNameSpace],
				Convert(Int,1) As [Level]
		From	@Values
		Union All
		Select	[LibraryId],
				[ParentNameSpace] As [MemberNameSpace],
				IIF(CharIndex('.',[ParentNameSpace]) > 0,
					Left([ParentNameSpace],Len([ParentNameSpace]) - CharIndex('.',Reverse([ParentNameSpace]))),
					Null)
					As[ParentNameSpace],
				[Level] + 1 As [Level]
		From	[NameSpace]
		Where	[ParentNameSpace] is Not Null),
	[Grouping] As (
		Select	[LibraryId],
				[MemberNameSpace],
				[ParentNameSpace]
		From	[NameSpace]
		Group By [LibraryId],
				[MemberNameSpace],
				[ParentNameSpace]),
	[Exists] As (
		Select	[LibraryId],
				[NameSpaceId],
				[NameSpaceParentId],
				Convert(NVarChar(Max),[NameSpace]) As [MemberNameSpace],
				Convert(Int,1) As [Level]
		From	[App_DataDictionary].[LibraryNameSpace]
		Where	[NameSpaceParentId] is Null
		Union All
		Select	C.[LibraryId],
				C.[NameSpaceId],
				C.[NameSpaceParentId],
				Convert(NVarChar(Max),FormatMessage('%s.%s',P.[MemberNameSpace],C.[NameSpace])) As [MemberNameSpace],
				P.[Level] + 1 As [Level]
		From	[App_DataDictionary].[LibraryNameSpace] C
				Inner Join [Exists] P
				On	C.[LibraryId] = P.[LibraryId] And
					C.[NameSpaceParentId] = P.[NameSpaceId]),
	[Member] As (
		Select	D.[LibraryId],
				IsNull(E.[NameSpaceId],
					(Select IsNull(Max([NameSpaceId]),0) From [App_DataDictionary].[LibraryNameSpace] Where [LibraryId] = D.[LibraryId]) +
					Dense_Rank() Over (
						Partition By D.[LibraryId], E.[NameSpaceId]
						Order By D.[MemberNameSpace]))
					As [NameSpaceId],
				D.[MemberNameSpace],
				D.[ParentNameSpace]
		From	[Grouping] D
				Left Join [Exists] E
				On	D.[LibraryId] = E.[LibraryId] And
					D.[MemberNameSpace] = E.[MemberNameSpace])
	Insert Into @NameSpace
	Select	M.[LibraryId],
			M.[NameSpaceId],
			P.[NameSpaceId] As [ParentId],
			IIF(CharIndex('.',M.[MemberNameSpace]) > 0,
				Right(M.[MemberNameSpace], CharIndex('.',Reverse(M.[MemberNameSpace])) -1),
				M.[MemberNameSpace])
				As [NameSpace],
			M.[MemberNameSpace]
	From	[Member] M
			Left Join [Member] P
			On	M.[LibraryId] = P.[LibraryId] And
				M.[ParentNameSpace] = P.[MemberNameSpace]

	-- Cascade Delete
	Delete From [App_DataDictionary].[LibraryMember]
	From	[App_DataDictionary].[LibraryMember] L
			Left Join @NameSpace N
			On	L.[LibraryId] = N.[LibraryId] and
				L.[NameSpaceId] = N.[NameSpaceId]
			Left Join @Values V
			On	N.[MemberNameSpace] = V.[MemberNameSpace] And
				L.[MemberName] = V.[MemberName]
			Left Join [App_DataDictionary].[ModelLibrary] M
			On	L.[LibraryId] = M.[LibraryId]
	Where	V.[LibraryId] is Null And
			((@LibraryId is Null And M.[ModelId] = @ModelId) or
			 (L.[LibraryId] = @LibraryId And M.[ModelId] is Null))
	Print FormatMessage ('Delete [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[LibraryNameSpace]
	From	[App_DataDictionary].[LibraryNameSpace] L
			Left Join @NameSpace N
			On	L.[LibraryId] = N.[LibraryId] and
				L.[NameSpaceId] = N.[NameSpaceId]
	Where	L.[LibraryId] = @LibraryId And
			N.[LibraryId] is Null;
	Print FormatMessage ('Delete [App_DataDictionary].[LibraryNameSpace]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	;With [Delta] As (
		Select	[LibraryId],
				[NameSpaceId],
				[ParentId],
				[NameSpace]
		From	@NameSpace
		Except
		Select	[LibraryId],
				[NameSpaceId],
				[NameSpaceParentId],
				[NameSpace]
		From	[App_DataDictionary].[LibraryNameSpace]),
	[Data] As (
		Select	V.[LibraryId],
				V.[NameSpaceId],
				V.[ParentId],
				V.[NameSpace],
				IIF(D.[LibraryId] is Null,0, 1) As [IsDiffrent]
		From	@NameSpace V
				Left Join [Delta] D
				On	V.[LibraryId] = D.[LibraryId] And
					V.[NameSpaceId] = D.[NameSpaceId])
	Merge [App_DataDictionary].[LibraryNameSpace] T
	Using [Data] S
	On	T.[LibraryId] = S.[LibraryId] And
		T.[NameSpaceId] = S.[NameSpaceId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[NameSpaceParentId] = S.[ParentId],
			[NameSpace] = S.[NameSpace]
	When Not Matched by Target Then
		Insert ([LibraryId], [NameSpaceId], [NameSpaceParentId], [NameSpace])
		Values ([LibraryId], [NameSpaceId], [ParentId], [NameSpace]);
	Print FormatMessage ('Merge [App_DataDictionary].[LibraryNameSpace]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Values] As (
		Select	V.[LibraryId],
				S.[NameSpaceId],
				IsNull(M.[MemberId],
					(Select IsNull(Max([MemberId]),0) From [App_DataDictionary].[LibraryMember] Where [LibraryId] = V.[LibraryId] And [NameSpaceId] = S.[NameSpaceId]) +
					Row_Number() Over (
						Partition By V.[LibraryId], S.[NameSpaceId], M.[MemberId]
						Order By V.[LibraryId], S.[NameSpaceId], V.[MemberName]))
					As [MemberId],
				V.[MemberName],
				V.[MemberType],
				V.[MemberData]
		From	@Values V
				Inner Join @NameSpace S
				On	V.[LibraryId] = S.[LibraryId] And
					V.[MemberNameSpace] = S.[MemberNameSpace]
				Left Join [App_DataDictionary].[LibraryMember] M
				On	V.[LibraryId] = M.[LibraryId] And
					S.[NameSpaceId] = M.[NameSpaceId] And
					V.[MemberName] = M.[MemberName]),
	[Delta] As (
		Select	[LibraryId],
				[NameSpaceId],
				[MemberId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[Values] V
		Except
		Select	[LibraryId],
				[NameSpaceId],
				[MemberId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[App_DataDictionary].[LibraryMember]),
	[Data] As (
		Select	V.[LibraryId],
				V.[NameSpaceId],
				V.[MemberId],
				V.[MemberName],
				V.[MemberType],
				V.[MemberData],
				IIF(D.[LibraryId] is Null,1, 0) As [IsDiffrent]
		From	[Values] V
				Left Join [Delta] D
				On	V.[LibraryId] = D.[LibraryId] And
					V.[NameSpaceId] = D.[NameSpaceId] And
					V.[MemberId] = D.[MemberId])
	Merge [App_DataDictionary].[LibraryMember] T
	Using [Data] S
	On	T.[LibraryId] = S.[LibraryId] And
		T.[NameSpaceId] = S.[NameSpaceId] And
		T.[MemberId] = S.[MemberId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[MemberName] = S.[MemberName],
			[MemberType] = S.[MemberType],
			[MemberData] = S.[MemberData]
	When Not Matched by Target Then
		Insert ([LibraryId], [NameSpaceId], [MemberId], [MemberName], [MemberType], [MemberData])
		Values ([LibraryId], [NameSpaceId], [MemberId], [MemberName], [MemberType], [MemberData]);
	Print FormatMessage ('Merge [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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