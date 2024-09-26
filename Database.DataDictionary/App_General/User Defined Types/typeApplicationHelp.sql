CREATE TYPE [App_General].[typeApplicationHelp] AS TABLE
(
	[HelpId]        UniqueIdentifier Null,
	[HelpSubject]   NVarChar(100)    Null,
	[HelpToolTip]   NVarChar(500)    Null,
	[HelpText]      NVarChar(Max)    Null,
	[NameSpace]     NVarChar(1023)   Null,
	[ModifiedBy]    SysName          Null,
	[ModifiedOn]    DateTime2 (7)    Null,
	[Modification]  NVarChar(10)     Null,
	[IsInserted]    Bit              Null,
	[IsUpdated]     Bit              Null,
	[IsDeleted]     Bit              Null,
	[IsCurrent]      Bit              Null
)
