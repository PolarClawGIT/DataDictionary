CREATE VIEW [AppScript].[DocumentHS] AS
-- Temporal View
With [Dates] As (
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[SchemaDocument]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[SchemaDocument]
	Where	[SysStart] != [SysEnd]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[TransformDocument]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[TransformDocument]
	Where	[SysStart] != [SysEnd]),
[Document] As (
	-- Rolls the Sub-Type of Document back into the Super-Type
	Select	D.[DocumentId],
			S.[RootFolder],
			S.[RelativePath],
			D.[FileName],
			D.[SysStart],
			D.[SysEnd]
	From	[AppScript].[SchemaDocument] D
			Inner Join [AppScript].[SchemaDefinition] S
			On	D.[SchemaId] = S.[SchemaId]
	Union
	Select	D.[DocumentId],
			S.[RootFolder],
			S.[RelativePath],
			D.[FileName],
			D.[SysStart],
			D.[SysEnd]
	From	[AppScript].[TransformDocument] D
			Inner Join [AppScript].[Transform] S
			On	D.[TransformId] = S.[TransformId])
Select	D.[DocumentId], -- PK
		D.[RootFolder],
		D.[RelativePath],
		D.[FileName],
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
From	[Document] D
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
