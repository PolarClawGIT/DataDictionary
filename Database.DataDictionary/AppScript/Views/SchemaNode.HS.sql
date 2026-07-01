CREATE VIEW [AppScript].[SchemaNodeHS] AS
-- Temporal View
With [Dates] As (
	Select	[NodeId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[SchemaNode]
	Union
	Select	[NodeId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[SchemaNode]
	Where	[SysStart] != [SysEnd])
Select	D.[NodeId], -- PK
		D.[SchemaId],
		F.[TemplateId],
		D.[NodeName],
		D.[NodeOrder],
		D.[RenderValueAs],
		D.[FixedValue],
		D.[ObjectScope],
		D.[ObjectProperty],
		D.[PropertyId],
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
From	[AppScript].[SchemaNode] D
		Inner Join [AppScript].[SchemaDefinition] F
		On	D.[SchemaId] = F.[SchemaId]
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
