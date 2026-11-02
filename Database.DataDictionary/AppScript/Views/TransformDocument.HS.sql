CREATE VIEW [AppScript].[TransformDocumentHS] AS
-- Temporal View
With [Dates] As (
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[TransformDocument]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[TransformDocument]
	Where	[SysStart] != [SysEnd])
Select	D.[DocumentId], -- PK
		FT.[TemplateId], -- AK
		FT.[SchemaId],
		D.[TransformId],
		D.[DataFileName],
		FO.[ObjectId],
		FO.[ObjectScope],
		FO.[ObjectMember],
		D.[ScriptedFileName], -- AK
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
From	[AppScript].[TransformDocument] D
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
		-- Not specifying a For System_Time returns the current value
		-- For System_Time <some date> returns the value for that date
		-- Otherwise the last value is returned
		Outer Apply (
			Select	Top 1
					[TemplateId],
					[SchemaId]
			From	[AppScript].[Transform]
			Where	[TransformId] = D.[TransformId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
		Outer Apply (
			Select	Top 1
					O.[ObjectId],
					O.[ObjectScope],
					O.[ObjectMember]
			From	[AppScript].[SchemaDocument] S
					Inner Join [AppScript].[TemplateObject] O
					On	S.[ObjectId] = O.[ObjectId]
			Where	[SchemaId] = FT.[SchemaId] And
					[DataFileName] = D.[DataFileName] And
					O.[SysStart] <= D.[SysEnd]
			Order By O.[SysStart] Desc) FO
GO

