CREATE VIEW [Obsolete].[TemplateHs] As
-- Temporal View
With [Dates] As (
	Select	[TemplateId],
			[SysStart],
			[SysEnd]
	From	[Obsolete].[Template]
	Union
	Select	[TemplateId],
			[SysStart],
			[SysEnd]
	From	[Obsolete].[TemplateFile]
	/*Union -- TODO: Temporal not yet implemented
	Select	[TemplateId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[Document]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[TemplateId], -- PK
		D.[TemplateTitle], -- AK
		D.[TemplateDescription],
		D.[BreakOnScope],
		D.[TransformScript],
		D.[RootFolder],
		I.[RelativePath] As [DocumentDirectory],
		I.[FilePrefix] As [DocumentPrefix],
		I.[FileSuffix] As [DocumentSuffix],
		I.[FileExtension] As [DocumentExtension],
		D.[ScriptAs], -- obsolete, The Transform results determines type
		O.[RelativePath] As [ScriptDirectory],
		O.[FilePrefix] As [ScriptPrefix],
		O.[FileSuffix] As [ScriptSuffix],
		O.[FileExtension] As [ScriptExtension],
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
From	[Obsolete].[Template] D
		Left Join [Obsolete].[TemplateFile] I
		On	D.[TemplateId] = I.[TemplateId] And
			I.[IsInput] = 1
		Left Join [Obsolete].[TemplateFile] T
		On	D.[TemplateId] = T.[TemplateId] And
			T.[IsProcess] = 1
		Left Join [Obsolete].[TemplateFile] O
		On	D.[TemplateId] = O.[TemplateId] And
			O.[IsOutput] = 1

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