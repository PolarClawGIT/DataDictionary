CREATE VIEW [App_DataDictionary].[DomainNameSpace] AS
With [Data] As (
	Select	[AliasId],
			[AliasElementName],
			Convert(NVarChar(Max), [AliasElementName]) As [AliasName],
			Convert(Int,0) As [Level]
	From	[App_DataDictionary].[DomainAlias]
	Where	[AliasParentId] is Null
	Union All
	Select	A.[AliasId],
			A.[AliasElementName],
			Convert(NVarChar(Max), 
				FormatMessage('%s.%s',D.[AliasName],A.[AliasElementName]))
				As [NameSpace],
			D.[Level] + 1 As [Level]
	From	[Data] D
			Inner Join [App_DataDictionary].[DomainAlias] A
			On	D.[AliasId] = A.[AliasParentId])
Select	[AliasId],
		[AliasElementName],
		[AliasName],
		[Level]
From	[Data]
GO