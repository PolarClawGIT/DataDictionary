CREATE TYPE [App_General].[typeApplicationHelp] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   NVarChar(100)    Null,
	[HelpToolTip]   NVarChar(500)    Null,
	[HelpText]      NVarChar(Max)    Null,
	[NameSpace]     NVarChar(1023)   Null,
	[ModfiedBy]     SysName          Null,
	[SysStart]      DateTime2 (7)    Null,
	[RowState]      NVarChar(10)     Null
)
