CREATE TYPE [App_DataDictionary].[typeApplicationHelp] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpParentId]  UniqueIdentifier Null,
	[HelpSubject]   NVarChar(100)    Null,
	[HelpText]      NVarChar(Max)    Null,
	[NameSpace]     NVarChar(1000)   Null,
	[Obsolete]      Bit              Null,
	[SysStart]      DATETIME2        Null
)
