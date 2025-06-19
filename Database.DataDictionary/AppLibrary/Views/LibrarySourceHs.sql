CREATE VIEW [AppLibrary].[LibrarySourceHs] As
-- Temporal View
With [Dates] As (
	Select	[LibraryId],
			[SysStart],
			[SysEnd]
	From	[AppLibrary].[LibrarySource]
	/*Union -- TODO: Temporal not yet implemented
	Select	[LibraryId],
			[SysStart],
			[SysEnd]
	From	[HsLibrary].[LibrarySource]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[LibraryId],
		D.[LibraryTitle],
		D.[LibraryDescription],
		D.[AssemblyName],
		D.[SourceFile],
		D.[SourceDate],
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
From	[AppLibrary].[LibrarySource] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[LibraryId] = D.[LibraryId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[LibraryId] = D.[LibraryId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO