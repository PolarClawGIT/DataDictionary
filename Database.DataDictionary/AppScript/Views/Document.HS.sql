CREATE VIEW [AppScript].[DocumentObjectHS] AS
-- Temporal View
With [Dates] As (
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[Document]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[Document]
	Where	[SysStart] != [SysEnd]),
[Object] As (
	-- Leaf Nodes
	-- Uses a Leaf first approch to building a tree where the Document defines the Leaf.
	Select	L.[ObjectId],
			L.[TemplateId],
			L.[ModelId],
			L.[ParentObjectId],
			L.[ObjectMember],
			L.[ObjectScope],
			Convert(NVarChar(Max), Concat('[',L.[ObjectMember],']')) As [ObjectPath],
			L.[IsExcluded],
			L.[KeepOrphaned],
			Convert(TinyInt,1) As [Depth],
			L.[SysStart],
			L.[SysEnd]
	From	[AppScript].[DocumentObject] L
			Inner Join [AppScript].[Document] D
			On	L.[ObjectId] = D.[ObjectId]
	-- Parent Nodes
	Union All
	Select	L.[ObjectId],
			L.[TemplateId],
			L.[ModelId],
			P.[ParentObjectId],
			L.[ObjectMember],
			L.[ObjectScope],
			Convert(NVarChar(Max), Concat('[',P.[ObjectMember],'].',L.[ObjectPath])) As [ObjectPath],
			L.[IsExcluded],
			L.[KeepOrphaned],
			Convert(TinyInt,L.[Depth] + 1) As [Depth],
			L.[SysStart],
			L.[SysEnd]
	From	[AppScript].[DocumentObject] P
			Inner Join [Object] L
			On	P.[ObjectId] = L.[ParentObjectId])
Select	D.[DocumentId], -- PK
		D.[TemplateId],
		D.[SchemaId],
		D.[TransformId],
		O.[ModelId],
		O.[ObjectScope],
		O.[ObjectPath],
		O.[ObjectMember],
		D.[FileName],
		O.[IsExcluded],
		O.[KeepOrphaned],
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
From	[AppScript].[Document] D
		Left Join [Object] O
		On	D.[ObjectId] = O.[ObjectId] And
			O.[ParentObjectId] is Null
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[DocumentId] = D.[DocumentId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[DocumentId] = D.[DocumentId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
