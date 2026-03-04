CREATE VIEW [ProofOfConcept].[viewSchemaNode] AS
With [Dates] As (
	Select	[NodeId],
			[SysStart],
			[SysEnd]
	From	[ProofOfConcept].[SchemaNode]
	/*Union -- TODO: Temporal not yet implemented
	Select	[NodeId],
			[SysStart],
			[SysEnd]
	From	??
	Where	[SysStart] != [SysEnd]*/)
Select	D.[NodeId], -- PK
		D.[TemplateId],
		D.[SchemaId],
		D.[NodeOrder],
		D.[RenderValueAs],
		D.[FixedValue],
		D.[ObjectScope],
		D.[ObjectProperty],
		D.[ModelPropertyId],
		-- Temporal Status
		D.[SysStart], -- PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[ProofOfConcept].[SchemaNode] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[NodeId] = D.[NodeId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[NodeId] = D.[NodeId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO