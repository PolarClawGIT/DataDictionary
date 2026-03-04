CREATE VIEW [ProofOfConcept].[viewDocument] AS
With [Dates] As (
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[ProofOfConcept].[Document]
	/*Union -- TODO: Temporal not yet implemented
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	??
	Where	[SysStart] != [SysEnd]*/)
Select	D.[DocumentId], -- PK
		D.[TemplateId], --AK1, AK2, AK3
		D.[SchemaId], -- AK2
		D.[TransformId],-- AK3
		D.[ObjectId], -- AK2, AK3
		D.[RootFolder], --AK1
		D.[RelativePath], -- AK1
		D.[FileName], -- AK1
		-- Temporal Status
		D.[SysStart], -- PK, AK1, AK2, AK3
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[ProofOfConcept].[Document] D
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