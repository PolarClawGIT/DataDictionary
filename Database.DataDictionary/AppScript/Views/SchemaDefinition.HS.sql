CREATE VIEW [AppScript].[SchemaDefinitionHS] AS
-- Temporal View
With [Dates] As (
	Select	[SchemaId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[SchemaDefinition]
	Union
	Select	[SchemaId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[SchemaDefinition]
	Where	[SysStart] != [SysEnd])
Select	D.[SchemaId], -- PK
		D.[TemplateId], -- AK
		D.[SchemaTitle], -- AK
		-- Root Node Behavior
		D.[RootNodeName],
		D.[BreakOnScope],
		-- Folder Patern for the files (Output for XSD, Input for XSLT)
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
From	[AppScript].[SchemaDefinition] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[TemplateId] = D.[TemplateId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[TemplateId] = D.[TemplateId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
