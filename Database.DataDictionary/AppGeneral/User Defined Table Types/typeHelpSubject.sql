CREATE TYPE [AppGeneral].[typeHelpSubject] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   [AppGeneral].[typeTitle] Null,
	[HelpToolTip]   [AppGeneral].[typeDescription] Null,
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
