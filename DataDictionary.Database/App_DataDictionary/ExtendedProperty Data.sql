;With [Properties] As (
	Select	Convert(sysname,Null) As [PropertyName] Where 1=2
	Union Select 'MS_Description'),
[Data] As (
	Select	IsNull(E.[PropertyId],
				IsNull((Select Max([PropertyId]) From [App_DataDictionary].[ExtendedProperty]),0) +
				Row_Number() Over (
					Partition By E.[PropertyId]
					Order By P.[PropertyName]))
				As [PropertyId],
			P.[PropertyName]
	From	[Properties] P
			Left Join [App_DataDictionary].[ExtendedProperty] E
			On	P.[PropertyName] = E.[PropertyName]),
[Changed] As (
	Select	[PropertyId],
			[PropertyName]
	From	[Data]
	Except
	Select	[PropertyId],
			[PropertyName]
	From	[App_DataDictionary].[ExtendedProperty]),
[ToMerge] As (
	Select	D.[PropertyId],
			D.[PropertyName],
			IIF(D.[PropertyId] is Not Null And C.[PropertyId] is Null,0,1) As [IsChanged]
	From	[Data] D
			Left Join [Changed] C
			On	D.[PropertyId] = C.[PropertyId])

Merge [App_DataDictionary].[ExtendedProperty] T
Using [ToMerge] S
On	T.[PropertyId] = S.[PropertyId]
When Matched And S.[IsChanged] = 1 then Update Set
	[PropertyName] = S.[PropertyName]
When Not Matched By Target Then
	Insert ([PropertyId], [PropertyName])
	Values ([PropertyId], [PropertyName])
When Not Matched by Source Then Delete;
GO
Select	*
From	[App_DataDictionary].[ExtendedProperty] 