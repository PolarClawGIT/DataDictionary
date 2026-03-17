CREATE VIEW [AppScript].[DocumentObjectTree] AS
-- Returns the Tree Strcutre of the Document Objects.
-- This uses a Parent first approch to build a tree and is primarly here for validation/testing.
With [Dates] As (
	Select	[ObjectId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DocumentObject]
	Union
	Select	[ObjectId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[DocumentObject]
	Where	[SysStart] != [SysEnd]),
[Data] As (
	-- Parent Nodes
	Select	L.[ObjectId],
			L.[TemplateId],
			L.[ParentObjectId],
			L.[ObjectMember],
			L.[ObjectScope],
			Convert(NVarChar(Max), Concat('[',L.[ObjectMember],']')) As [ObjectPath],
			L.[IsExcluded],
			L.[KeepOrphaned],
			Convert(TinyInt,1) As [Depth],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [ObjectMember])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DocumentObject] L
	Where	L.[ParentObjectId] is Null
	-- Parent Nodes
	Union All
	Select	C.[ObjectId],
			IsNull(C.[TemplateId], P.[TemplateId]) As [TemplateId],
			C.[ParentObjectId],
			C.[ObjectMember],
			IsNull(C.[ObjectScope], P.[ObjectScope]) As [ObjectScope],
			Convert(NVarChar(Max), Concat(P.[ObjectPath],'.[',C.[ObjectMember],']')) As [ObjectPath],
			C.[IsExcluded],
			C.[KeepOrphaned],
			Convert(TinyInt,P.[Depth] + 1) As [Depth],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', P.[HierarchyId],
				Row_Number() Over (Partition By C.[ObjectId] Order By C.[ObjectMember])))
				As [HierarchyId],
			Greatest(P.[SysStart], C.[SysStart]) As [SysStart],
			Least(P.[SysEnd], C.[SysEnd]) As [SysEnd]
	From	[Data] P
			Inner Join [AppScript].[DocumentObject] C
			On	P.[ObjectId] = C.[ParentObjectId] And
				-- Temporal, multiple rows could be returned. Do not have confidence in this.
				((P.[SysStart] >= C.[SysStart] And P.[SysStart] < C.[SysEnd]) Or
				(C.[SysStart] >= P.[SysStart] And C.[SysStart] < P.[SysEnd])))
Select	D.[ObjectId],
		D.[TemplateId],
		D.[ObjectScope],
		D.[ObjectPath],
		D.[ObjectMember],
		D.[IsExcluded],
		D.[KeepOrphaned],
		Convert(HierarchyId, D.[HierarchyId]) As [HierarchyId], -- Values is not guaranteed between executions.
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[Data] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[ObjectId] = D.[ObjectId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[ObjectId] = D.[ObjectId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO

-- Testing
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	-- Setup
	Declare @ModelId UniqueIdentifier = (Select [ModelId] From [AppModel].[Model] Where [ModelTitle] = 'Unit Test')

	Declare	@TemplateId UniqueIdentifier = NewId(),
			@Template [AppScript].[udttTemplate]

	Insert Into @Template ([TemplateId], [TemplateTitle], [TemplateDescription])
	Values (@TemplateId, 'Unit Test', 'Testing')
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Template

	-- Root Node
	Insert Into [AppScript].[DocumentObject] ([TemplateId], [ObjectMember])
	Values (@TemplateId, 'Root Node')

	-- Children
	Insert Into [AppScript].[DocumentObject] ([ParentObjectId], [TemplateId], [ObjectMember])
	Select	[ObjectId] As [ParentObjectId],
			@TemplateId As [TemplateId],
			'Child 1' As [ObjectMember]
	From	[AppScript].[DocumentObject]
	Where	[ObjectMember] = 'Root Node'
	Union
	Select	[ObjectId] As [ParentObjectId],
			@TemplateId As [TemplateId],
			'Child 2' As [ObjectMember]
	From	[AppScript].[DocumentObject]
	Where	[ObjectMember] = 'Root Node'

	-- Grand Children
	Insert Into [AppScript].[DocumentObject] ([ParentObjectId], [TemplateId], [ObjectMember])
	Select	[ObjectId] As [ParentObjectId],
			@TemplateId As [TemplateId],
			'Grand Child 1' As [ObjectMember]
	From	[AppScript].[DocumentObject]
	Where	[ObjectMember] = 'Child 1'

	Select	*
	From	[AppScript].[DocumentObjectTree]

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
	If(ERROR_NUMBER()<> 50000) Throw;
End Catch;
*/