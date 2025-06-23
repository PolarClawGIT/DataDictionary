CREATE TYPE [AppGeneral].[udttHelpSubject] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   [AppGeneral].[uddtTitle] Null,
	[HelpToolTip]   [AppGeneral].[uddtDescription] Null,
	[HelpText]      NVarChar(Max) Null,
	[NameSpace]     NVarChar(1023) Null,
	[CreatedOn]		DateTime2 (7) Null,
	[CreatedBy]		SysName Null,
	[RemovedOn]		DateTime2 (7) Null,
	[RemovedBy]		SysName Null,
	[IsInserted]    Bit Null,
	[IsUpdated]     Bit Null,
	[IsDeleted]     Bit Null,
	[IsCurrent]     Bit Null
)
