CREATE TYPE [AppGeneral].[typeHelpSubject] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   [App_DataDictionary].[typeTitle] Null,
	[HelpToolTip]   [App_DataDictionary].[typeDescription] Null,
	[HelpText]      NVarChar(Max) Null,
	[NameSpace]     NVarChar(1023) Null,
	[ModifiedBy]    SysName Null,
	[ModifiedOn]    DateTime2 (7) Null,
	[IsInserted]    Bit Null,
	[IsUpdated]     Bit Null,
	[IsDeleted]     Bit Null,
	[IsCurrent]     Bit Null
)
