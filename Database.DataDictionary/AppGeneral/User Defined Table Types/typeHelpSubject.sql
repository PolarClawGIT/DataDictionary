CREATE TYPE [AppGeneral].[typeHelpSubject] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   [AppGeneral].[dtTitle] Null,
	[HelpToolTip]   [AppGeneral].[dtDescription] Null,
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
