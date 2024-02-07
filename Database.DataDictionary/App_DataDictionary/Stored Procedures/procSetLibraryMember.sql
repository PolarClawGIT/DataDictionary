CREATE PROCEDURE [App_DataDictionary].[procSetLibraryMember]
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

	-- Validation
	If @ModelId is Null and @LibraryId is Null
	Throw 50000, '@ModelId or @LibraryId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberParentId]        UniqueIdentifier Null,
		[MemberName]            [App_DataDictionary].[typeNameSpaceElement] Not Null,
		[ScopeId]               Int Not Null,
		[MemberData]            XML Null,
		Primary Key ([MemberId]))

	Declare @NameSpace Table (
		[MemberId]              UniqueIdentifier Not Null,
		[LibraryId]             UniqueIdentifier Not Null,
		[MemberName]		    [App_DataDictionary].[typeNameSpaceElement] Not Null,
		[NameSpace]             [App_DataDictionary].[typeNameSpaceFullName] Not Null,
		[ParentNameSpace]       [App_DataDictionary].[typeNameSpaceFullName] Null,
		[ScopeId]               Int Not Null,
		Primary Key ([MemberId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				G.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName] (S.[ScopeId]) G),
	[NameSpace] As (
		Select	[LibraryId],
				[MemberId],
				[MemberParentId],
				[MemberName],
				[ScopeId],
				Convert(NVarChar(max),FormatMessage('[%s]', [MemberName])) As [NameSpace],
				Convert(NVarChar(max), Null) As [ParentNameSpace]
		From	[App_DataDictionary].[LibraryMember] L
		Where	[MemberParentId] is Null And
				[LibraryId] In (
					Select	@LibraryId As [LibraryId]
					Union
					Select	[LibraryId]
					From	@Data
					Where	[LibraryId] is Not Null
					Group By [LibraryId]) And
				[ScopeId] In (Select [ScopeId] From [Scope] Where [ScopeName] In ('Library.NameSpace','Library.Type'))
		Union All
		Select	P.[LibraryId],
				C.[MemberId],
				C.[MemberParentId],
				C.[MemberName],
				C.[ScopeId],
				Convert(NVarChar(max),FormatMessage('%s.[%s]', P.[NameSpace], C.[MemberName])) As [NameSpace],
				P.[NameSpace] As [ParentNameSpace]
		From	[NameSpace] P
				Inner Join [App_DataDictionary].[LibraryMember] C
				On	P.[LibraryId] = C.[LibraryId] And
					P.[MemberId] = C.[MemberParentId] And
				C.[ScopeId] In (Select [ScopeId] From [Scope] Where [ScopeName] In ('Library.NameSpace','Library.Type'))
				),
	[Alias] As (
		Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				IIF(X.[IsBase] = 1, D.[MemberId], Null) As [MemberId],
				X.[AliasElement] As [MemberName],
				X.[AliasName] As [NameSpace],
				X.[ParentAliasName] As [ParentNameSpace],
				S.[ScopeId],
				Row_Number() Over (
					Partition By 
						Coalesce(D.[LibraryId], @LibraryId),
						X.[AliasElement],
						X.[AliasName]
					Order By
						X.[AliasElement],
						X.[AliasName],
						X.[IsBase] Desc)
					As [RankIndex]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName] (
					IIF(D.[NameSpace] is Null, D.[MemberName], FormatMessage('%s.%s',D.[NameSpace], D.[MemberName]))) X
				Left Join @Data P
				On	Coalesce(D.[LibraryId], @LibraryId) = Coalesce(P.[LibraryId], @LibraryId) And
					D.[MemberParentId] = P.[MemberId]
				Left Join [Scope] S
				On	Coalesce(D.[ScopeName],'Library.NameSpace') = S.[ScopeName]
		Where	Coalesce(D.[ScopeName],'Library.NameSpace') In ('Library.NameSpace','Library.Type')			
				),
	[Rank] As (
		Select	Coalesce(N.[MemberId], A.[MemberId], NewId()) As [MemberId],
				IsNull(N.[LibraryId],A.[LibraryId]) As [LibraryId],
				IsNull(N.[MemberName], A.[MemberName]) As [MemberName],
				IsNull(N.[NameSpace], A.[NameSpace]) As [NameSpace],
				IsNull(N.[ParentNameSpace], A.[ParentNameSpace]) As [ParentNameSpace],
				IsNull(N.[ScopeId], A.[ScopeId]) As [ScopeId],
				Row_Number() Over (
					Partition By 
						IsNull(N.[LibraryId],A.[LibraryId]),
						IsNull(N.[MemberName], A.[MemberName]),
						IsNull(N.[NameSpace], A.[NameSpace])
					Order By Case
								When N.[MemberId] is Not Null Then 1
								When A.[MemberId] is Not Null Then 2
								Else 255 End)
					As [RankIndex]
		From	[Alias] A
				--Full Outer Join [NameSpace] N
				Left Join [NameSpace] N
				On	A.[LibraryId] = N.[LibraryId] And
					A.[NameSpace] = N.[NameSpace] And
					A.[MemberName] = N.[MemberName] And
					A.[RankIndex] = 1
					)
	Insert Into @NameSpace
	Select	[MemberId],
			[LibraryId],
			[MemberName],
			[NameSpace],
			[ParentNameSpace],
			[ScopeId]
	From	[Rank]
	Where	[RankIndex] = 1

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F),
	[Data] As (
		Select	Coalesce(D.[LibraryId], @LibraryId) As [LibraryId],
				D.[MemberId],
				D.[MemberParentId],
				X.[ParentAliasName] As [ParentNameSpace],
				X.[AliasName] As [NameSpace],
				X.[AliasElement] As [MemberName],
				S.[ScopeId],
				D.[MemberData]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName] (
					IIF(D.[NameSpace] is Null, D.[MemberName],
						FormatMessage('%s.%s',D.[NameSpace], D.[MemberName]))) X
				Left Join [Scope] S
				On	D.[ScopeName] = S.[ScopeName]
		Where	D.[ScopeName] Not In ('Library.NameSpace','Library.Type') And
				X.[IsBase] = 1)
	Insert Into @Values
	Select	D.[MemberId],
			D.[LibraryId],
			Coalesce(D.[MemberParentId], P.[MemberId]) As [MemberParentId],
			D.[MemberName],
			D.[ScopeId],
			D.[MemberData]
	From	[Data] D
			Left Join @NameSpace P
			On	D.[LibraryId] = P.[LibraryId] And
				D.[ParentNameSpace] = P.[NameSpace]
	
	Insert Into @Values
	Select	N.[MemberId],
			N.[LibraryId],
			P.[MemberId] As [MemberParentId],
			N.[MemberName],
			N.[ScopeId],
			Null As [MemberData]
	From	@NameSpace N
			Left Join @NameSpace P
			On	N.[ParentNameSpace] = P.[NameSpace]

	-- Apply Changes
	Delete From [App_DataDictionary].[LibraryMember]
	From	[App_DataDictionary].[LibraryMember] T
			Left Join @Values S
			On	T.[MemberId] = S.[MemberId] And
				((T.[MemberParentId] is Null And S.[MemberParentId] is Null) Or
				 (T.[MemberParentId] = S.[MemberParentId]))
	Where	S.[MemberId] is Null And
			T.[LibraryId] In (
			Select	A.[LibraryId]
			From	[App_DataDictionary].[LibrarySource] A
					Left Join [App_DataDictionary].[ModelLibrary] C
					On	A.[LibraryId] = C.[LibraryId]
			Where	(@LibraryId is Null Or @LibraryId = A.[LibraryId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[MemberId],
				[LibraryId],
				[MemberParentId],
				[MemberName],
				[ScopeId],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	@Values
		Except
		Select	[MemberId],
				[LibraryId],
				[MemberParentId],
				[MemberName],
				[ScopeId],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[App_DataDictionary].[LibraryMember])
	Update [App_DataDictionary].[LibraryMember]
	Set		[LibraryId] = S.[LibraryId],
			[MemberParentId] = S.[MemberParentId],
			[MemberName] = S.[MemberName],
			[ScopeId] = S.[ScopeId],
			[MemberData] = S.[MemberData]
	From	[App_DataDictionary].[LibraryMember] T
			Inner Join [Delta] S
			On	T.[MemberId] = S.[MemberId]
	Print FormatMessage ('Update [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[LibraryMember] (
			[MemberId],
			[LibraryId],
			[MemberParentId],
			[MemberName],
			[ScopeId],
			[MemberData])
	Select	S.[MemberId],
			S.[LibraryId],
			S.[MemberParentId],
			S.[MemberName],
			S.[ScopeId],
			S.[MemberData]
	From	@Values S
			Left Join [App_DataDictionary].[LibraryMember] T
			On	S.[MemberId] = T.[MemberId]
	Where	T.[MemberId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

----------------------------------------------------------
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

declare @Library App_DataDictionary.typeLibrarySource
insert into @Library values('2C10EC5D-8E42-4170-89C5-EB2216EF864A',N'DataDictionary.DataLayer',NULL,N'DataDictionary.DataLayer',N'Library',N'DataDictionary.Test.xml','2023-12-11 11:50:06.3127993')

exec [App_DataDictionary].[procSetLibrarySource] @LibraryId='2C10EC5D-8E42-4170-89C5-EB2216EF864A',@Data=@Library

--Delete From [App_DataDictionary].[LibraryMember]

declare @p2 App_DataDictionary.typeLibraryMember
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','7732A392-4195-43A9-B80C-0DE889A2F1F0',NULL,N'DataDictionary.DataLayer',NULL,N'DataDictionary',N'Library.NameSpace',NULL,NULL)
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','3F05625E-0FC8-404F-9185-F58EA1DBB39D','7732A392-4195-43A9-B80C-0DE889A2F1F0',N'DataDictionary.DataLayer',N'DataDictionary',N'DataLayer',N'Library.NameSpace',NULL,NULL)
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','84DCE081-9DD5-4D5D-92D7-04778EA45B06','3F05625E-0FC8-404F-9185-F58EA1DBB39D',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer',N'DomainData',N'Library.NameSpace',NULL,NULL)
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','24254027-547C-474E-88C0-4E782D2B69EA','84DCE081-9DD5-4D5D-92D7-04778EA45B06',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData',N'Alias',N'Library.NameSpace',NULL,NULL)
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','9314670F-B636-4C8A-A7BF-8ED042D4F241','24254027-547C-474E-88C0-4E782D2B69EA',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias',N'IModelAliasKey',N'Library.Type',N'T',N'<summary>
            Interface for the Model Alias Key
            </summary>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','2A0E38E8-41ED-4828-BB93-ED12A8BEE598','9314670F-B636-4C8A-A7BF-8ED042D4F241',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.IModelAliasKey',N'SystemId',N'Library.Property',N'P',N'<summary>
            System Id of the Model Alias item.
            </summary>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','97FBBE32-ABCE-4CAB-B316-E614E6F664E0','24254027-547C-474E-88C0-4E782D2B69EA',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias',N'ModelAliasKey',N'Library.Type',N'T',N'<summary>
            Implementation for the Model Alias Key
            </summary>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DF9F41DA-256C-492E-B72F-526F87C07AC1','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'SystemId',N'Library.Property',N'P',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','E1339155-8B9D-473D-A63B-BAF4EE0C34E4','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B925A584-67A4-48D5-9654-020D2938F8EC','E1339155-8B9D-473D-A63B-BAF4EE0C34E4',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.IModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','E58E2D3F-07E9-4A45-831A-FCC755720939','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','CD3A51F4-2F51-4586-976F-C9A7C7B233A5','E58E2D3F-07E9-4A45-831A-FCC755720939',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Catalog.IDbCatalogKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DFB4DD1E-66DC-4924-9A68-03A9981FDD00','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','EFD0B334-8304-4D4B-B0FF-774B31AFC321','DFB4DD1E-66DC-4924-9A68-03A9981FDD00',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Schema.IDbSchemaKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DCBD55C1-4661-4BC9-A544-04A826A05C79','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DE32D0AE-1CAE-42DB-9919-C9E835BB41AE','DCBD55C1-4661-4BC9-A544-04A826A05C79',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Table.IDbTableKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','A89048B4-0BDF-453D-9BB3-CB72E68241AB','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','EA69BB8B-3141-4FDE-B2BD-6D96AFAF23F4','A89048B4-0BDF-453D-9BB3-CB72E68241AB',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Table.IDbTableColumnKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','773B801D-B1F9-4E1A-9E0F-84715EEC1F28','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B2B84544-9685-40AC-93FA-4408D7346B53','773B801D-B1F9-4E1A-9E0F-84715EEC1F28',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Domain.IDbDomainKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','E751332B-CF92-404F-AECA-46E4C288C7BC','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B3B61617-F242-4FB9-BE2C-0E3BFA3FC81B','E751332B-CF92-404F-AECA-46E4C288C7BC',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Routine.IDbRoutineKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','647E51F6-4EBD-408D-9F2E-79D606BBF5D9','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','958F90F3-C200-45CC-BB0E-A943EEEC9C35','647E51F6-4EBD-408D-9F2E-79D606BBF5D9',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Routine.IDbRoutineParameterKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','0FAD01DC-3756-44F6-9B15-7B0CE3D3C76C','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','5527E48A-E079-4F65-BDB3-CCB5E480DAB6','0FAD01DC-3756-44F6-9B15-7B0CE3D3C76C',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DatabaseData.Constraint.IDbConstraintKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','488961AA-05BA-4098-BF85-2A82FBA09824','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','8B4D2756-6C43-470A-8381-584FF9763F21','488961AA-05BA-4098-BF85-2A82FBA09824',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.LibraryData.Source.ILibrarySourceKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','39DCC4FF-671A-46B5-B74B-4315373CD664','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'#ctor',N'Library.Method',N'M',N'<summary>
            Constructor for the Model Alias Key
            </summary><param name="source"></param>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DF778821-DCAC-49C1-AFBB-8F227C550F13','39DCC4FF-671A-46B5-B74B-4315373CD664',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.#ctor',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.LibraryData.Member.ILibraryMemberKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','43F554F5-00C3-4BD3-859F-2B646B7BBFEC','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'Equals',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','3BEE662A-370C-4A24-94E8-2A0A1499FAC0','43F554F5-00C3-4BD3-859F-2B646B7BBFEC',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.Equals',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.IModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','FA9C2B6D-511A-401D-B43A-C3133EB7AD58','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'Equals',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','C9B6B8FC-0D26-4640-8588-E3E594BD2136','FA9C2B6D-511A-401D-B43A-C3133EB7AD58',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.Equals',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>System.Object</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B2C6E464-9AE8-4F26-A2FD-FF7CF392F686','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'CompareTo',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','759FAE51-86B0-4026-95D4-880E99EECCF2','B2C6E464-9AE8-4F26-A2FD-FF7CF392F686',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.CompareTo',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.IModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B99AD298-3B58-4981-834A-2A1447FDE471','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'CompareTo',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','BA00FA9F-73EC-41D6-853A-5A46C0DF1FDD','B99AD298-3B58-4981-834A-2A1447FDE471',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.CompareTo',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>System.Object</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','750C8471-A239-4312-818B-3326FD91F1CE','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_Equality',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','92243FE4-5600-4DE2-95B0-5B06DAC44B8D','750C8471-A239-4312-818B-3326FD91F1CE',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_Equality',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','AA0D3BAA-51CE-4F3B-B2ED-0BB5E1E69CE1','750C8471-A239-4312-818B-3326FD91F1CE',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_Equality',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','F09138C6-F07D-47D3-9A75-36194D620C9C','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_Inequality',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','498A034A-8689-48D8-A817-A3BDCC80B5DD','F09138C6-F07D-47D3-9A75-36194D620C9C',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_Inequality',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','F88667CA-CFED-42CD-8A58-332A1B74D300','F09138C6-F07D-47D3-9A75-36194D620C9C',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_Inequality',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','5AC1184D-4923-4E1E-8896-5D098BF5AB5B','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_LessThan',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B7A649B6-107C-4767-A6FF-18350FAD4184','5AC1184D-4923-4E1E-8896-5D098BF5AB5B',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_LessThan',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','FF46105C-9798-44C3-B76A-C5A6E8E24E3C','5AC1184D-4923-4E1E-8896-5D098BF5AB5B',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_LessThan',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','DBC0DC5A-7BCA-4862-A2B3-DE04066F72A7','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_LessThanOrEqual',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','89A50B18-1E81-49C5-B02B-B5F1B630BE46','DBC0DC5A-7BCA-4862-A2B3-DE04066F72A7',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_LessThanOrEqual',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','D995486B-0A78-48EC-B1F1-F72E87225316','DBC0DC5A-7BCA-4862-A2B3-DE04066F72A7',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_LessThanOrEqual',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','314E6F14-ED4C-4D92-8581-2B429E834A13','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_GreaterThan',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','3FEE109F-C0D3-4CD5-9DEC-755E5E1AFFDC','314E6F14-ED4C-4D92-8581-2B429E834A13',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_GreaterThan',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','E07B3B72-480F-4945-BDB0-712C641A8CB6','314E6F14-ED4C-4D92-8581-2B429E834A13',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_GreaterThan',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','CDB2DD0F-A663-40B1-A82F-6C7CEFF6C900','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'op_GreaterThanOrEqual',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','AA266E31-69CA-4229-8DBE-3F1046982219','CDB2DD0F-A663-40B1-A82F-6C7CEFF6C900',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_GreaterThanOrEqual',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','B2C00AAE-8DCD-4CC9-A2DD-A9CE21B451FE','CDB2DD0F-A663-40B1-A82F-6C7CEFF6C900',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey.op_GreaterThanOrEqual',N'@parameter',N'Library.Parameter',NULL,N'<ParamterType>DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey</ParamterType>')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','133C9E40-F66C-4999-A823-252015A91B27','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'GetHashCode',N'Library.Method',N'M',N'<inheritdoc />')
insert into @p2 values('2C10EC5D-8E42-4170-89C5-EB2216EF864A','3602079B-0148-4F21-9764-F25D782868A0','97FBBE32-ABCE-4CAB-B316-E614E6F664E0',N'DataDictionary.DataLayer',N'DataDictionary.DataLayer.DomainData.Alias.ModelAliasKey',N'ToString',N'Library.Method',N'M',N'<summary>
            Returns a string that represents the current object.
            </summary><returns></returns>')

Select	'Source', *
From	@p2
Order BY [NameSpace], [MemberName]

exec [App_DataDictionary].[procSetLibraryMember] @LibraryId='2C10EC5D-8E42-4170-89C5-EB2216EF864A',@Data=@p2


Declare @Result App_DataDictionary.typeLibraryMember
Insert Into @Result
exec [App_DataDictionary].[procGetLibraryMember]

Select	'Result', *
From	@Result

exec [App_DataDictionary].[procSetLibrarySource] @LibraryId='2C10EC5D-8E42-4170-89C5-EB2216EF864A'

	-- By default, throw and error and exit without committing
--;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
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