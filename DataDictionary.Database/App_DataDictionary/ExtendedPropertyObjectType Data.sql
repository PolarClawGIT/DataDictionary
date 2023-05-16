-- This script is used to build and maintain the data in the DomainObjectType list.
-- This list is intended to support the diffrent things MS SQL extended properties can be placed upon
-- AND ones going to be supported by the application
-- List is sourced from: https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql?view=sql-server-ver16
With [Level0] As (
	Select	Convert(sysname,Null) As [level0type] Where 1 = 2
	--Union Select 'ASSEMBLY'
	--Union Select 'CONTRACT'
	--Union Select 'EVENT NOTIFICATION'
	--Union Select 'FILEGROUP'
	--Union Select 'MESSAGE TYPE'
	--Union Select 'PARTITION FUNCTION'
	--Union Select 'PARTITION SCHEME'
	--Union Select 'REMOTE SERVICE BINDING'
	--Union Select 'ROUTE'
	Union Select 'SCHEMA'
	--Union Select 'SERVICE'
	--Union Select 'USER'
	--Union Select 'TRIGGER'
	--Union Select 'TYPE'
	--Union Select 'PLAN GUIDE'
	),
[Level1] As (
	Select	Convert(sysname,Null) As [level0type], Convert(sysname,Null) As [level1type] Where 1 = 2
	Union Select 'SCHEMA', 'AGGREGATE'
	Union Select 'SCHEMA', 'DEFAULT'
	Union Select 'SCHEMA', 'FUNCTION'
	--Union Select 'SCHEMA', 'LOGICAL FILE NAME'
	Union Select 'SCHEMA', 'PROCEDURE'
	--Union Select 'SCHEMA', 'QUEUE'
	--Union Select 'SCHEMA', 'RULE'
	--Union Select 'SCHEMA', 'SEQUENCE'
	--Union Select 'SCHEMA', 'SYNONYM'
	Union Select 'SCHEMA', 'TABLE'
	--Union Select 'SCHEMA', 'TABLE_TYPE'
	Union Select 'SCHEMA', 'TYPE'
	Union Select 'SCHEMA', 'VIEW'
	--Union Select 'SCHEMA', 'XML SCHEMA COLLECTION'
	),
[Level2] As (
	Select	Convert(sysname,Null) As [level1type], Convert(sysname,Null) As [level2type] Where 1 = 2
	Union Select 'TABLE',	'COLUMN'
	Union Select 'VIEW',	'COLUMN'
	Union Select 'TABLE',	'CONSTRAINT'
	--Union Select 'TABLE',	'EVENT NOTIFICATION'
	Union Select 'TABLE',	'INDEX'
	Union Select 'VIEW',	'INDEX'
	Union Select 'PROCEDURE', 'PARAMETER'
	Union Select 'FUNCTION', 'PARAMETER'
	Union Select 'TABLE',	'TRIGGER'
	Union Select 'VIEW',	'TRIGGER'
	),
[Combine] As (
	Select	Convert(sysname,Null) As [level0type],
			Convert(sysname,Null) As [level1type],
			Convert(sysname,Null) As [level2type]
	Where	1 = 2
	Union
	Select	[level0type],
			Null As [level1type],
			Null As [level2type]
	From	[Level0]
	Union
	Select	P.[level0type],
			C.[level1type],
			Null As [level2type]
	From	[Level1] C
			Inner Join [Level0] P
			On	C.[level0type] = P.[level0type]
	Union
	Select	G.[level0type],
			P.[level1type],
			C.[level2type]
	From	[Level2] C
			Inner Join [Level1] P
			On	C.[level1type] = P.[level1type]
			Inner Join [Level0] G
			On	P.[level0type] = G.[level0type]),
[Data] As (
	Select	IsNull(O.[ObjectTypeId],
				IsNull((Select Max([ObjectTypeId]) From [App_DataDictionary].[ExtendedPropertyType]),0) + 
				Row_Number() Over (
					Partition By O.[ObjectTypeId]
					Order By C.[level0type], C.[level1type], C.[level2type]))
				As [ObjectTypeId],
			C.[level0type],
			C.[level1type],
			C.[level2type]
	From	[Combine] C
			Left Join [App_DataDictionary].[ExtendedPropertyType] O
			On	C.[level0type] = O.[level0type] And
				IsNull(C.[level1type],'') = IsNull(O.[level1type],'') And
				IsNull(C.[level2type],'') = IsNull(O.[level2type],'')),
[Changed] As (
	Select	[ObjectTypeId],
			[level0type],
			[level1type],
			[level2type]
	From	[Data]
	Except
	Select	[ObjectTypeId],
			[level0type],
			[level1type],
			[level2type]
	From	[App_DataDictionary].[ExtendedPropertyType]),
[ToMerge] As (
	Select	D.[ObjectTypeId],
			D.[level0type],
			D.[level1type],
			D.[level2type],
			IIF(D.[ObjectTypeId] is Not Null And C.[ObjectTypeId] is Null,0,1) As [IsChanged]
	From	[Data] D
			Left Join [Changed] C
			On	D.[ObjectTypeId] = C.[ObjectTypeId])

Merge [App_DataDictionary].[ExtendedPropertyType] T
Using [ToMerge] S
On	T.[ObjectTypeId] = S.[ObjectTypeId]
When Matched And S.[IsChanged] = 1 then Update Set
	[Level0Type] = S.[Level0Type],
	[level1type] = S.[level1type],
	[level2type] = S.[level2type]
When Not Matched By Target Then
	Insert (ObjectTypeId, Level0Type, Level1Type, Level2Type)
	Values (ObjectTypeId, Level0Type, Level1Type, Level2Type)
When Not Matched by Source Then Delete;
GO

Select	*
From	[App_DataDictionary].[ExtendedPropertyType]