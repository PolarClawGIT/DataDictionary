CREATE VIEW [ProofOfConcept].[viewTransform] AS
-- Temporal View
With [Dates] As (
	Select	[TransformId],
			[SysStart],
			[SysEnd]
	From	[ProofOfConcept].[Transform]
	/*Union -- TODO: Temporal not yet implemented
	Select	[TransformId],
			[SysStart],
			[SysEnd]
	From	??
	Where	[SysStart] != [SysEnd]*/)
Select	D.[TransformId], -- PK
		D.[TransformTitle], -- AK
		D.[TemplateId],
		D.[SchemaId],
		D.[TransformScript],
		D.[RootFolder],
		D.[RelativePath],
		D.[FilePrefix],
		D.[FileSuffix],
		D.[FileExtension],
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
From	[ProofOfConcept].[Transform] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[TransformId] = D.[TransformId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[TransformId] = D.[TransformId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO