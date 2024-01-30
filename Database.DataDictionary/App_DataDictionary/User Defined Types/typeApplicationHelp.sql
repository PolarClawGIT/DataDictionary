CREATE TYPE [App_DataDictionary].[typeApplicationHelp] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpParentId]  UniqueIdentifier Null,
	[HelpSubject]   NVarChar(100)    Null,
	[HelpToolTip]   NVarChar(500)    Null,
	[HelpText]      NVarChar(Max)    Null,
	[NameSpace]     NVarChar(1023)   Null
)
