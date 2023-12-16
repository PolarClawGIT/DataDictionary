CREATE FUNCTION [App_DataDictionary].[funcSplitAliasName](@AliasName NVarChar(Max))
-- This takes a String that has a Delimiter of a period and creates a hierarchy of values.
-- Each level of the hierarchy is returned as separate row.
-- The example of this is a Database Fully Qualified (aka the four part name) name where the period is the delimiter.
-- This also applies to .Net NameSpaces where the depth is unknown.
-- This has the best performance found so far, even over large set of data.
-- Methods that took a list of AliasNames performed poorly.
--
-- Columns:
--   [AliasName]      : Fully Qualified AliasName formated like SQL Qualified Object Names. Includes the Element Name.
--   [ParentAliasName]: Fully Qualified AliasNamed formated like SQL Qualified Object Names without the Element Name.
--   [AliasElement]   : Element of the AliasName without formating.
--   [IsBase]         : This row is the AliasName passed to the function (not a parent row).
--
-- In: [DatabaseName].[SchemaName].[TableName].[ColumnName] or
--     DataDictionary.SchemaName.TableName.ColumnName
-- Out: [DatabaseName]
--      [DatabaseName].[SchemaName]
--      [DatabaseName].[SchemaName].[TableName]
--      [DatabaseName].[SchemaName].[TableName].[ColumnName]
RETURNS TABLE AS RETURN (
	With [Parse] As (
		Select	@AliasName As [AliasNameParent],
				Convert(NVarChar(Max),Null) As [AliasNameChild]
		Union All
		Select	N.[AliasNameParent],
				P.[AliasNameParent] As [AliasNameChild]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[AliasNameParent]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[AliasNameParent])) = 0  Then Null
						-- No more brackets
						When CharIndex(']',Reverse(P.[AliasNameParent])) = 0 And
							CharIndex('[',Reverse(P.[AliasNameParent])) = 0  
							Then Left(P.[AliasNameParent], Len(P.[AliasNameParent]) - CharIndex('.',Reverse(P.[AliasNameParent])) -0)
						-- Period is after the bracket
						When CharIndex(']',Reverse(P.[AliasNameParent])) > 0 And
							CharIndex(']',Reverse(P.[AliasNameParent])) > CharIndex('.', Reverse(P.[AliasNameParent]))
							Then Left(P.[AliasNameParent], Len(P.[AliasNameParent]) - CharIndex('.',Reverse(P.[AliasNameParent])) -0)
						-- Period is before the bracket
						When CharIndex(']',Reverse(P.[AliasNameParent])) > 0 And
							CharIndex('[.', Reverse(P.[AliasNameParent])) > 0
							Then Left(P.[AliasNameParent], Len(P.[AliasNameParent]) - CharIndex('[.',Reverse(P.[AliasNameParent])) -1)
						Else Null
						End As [AliasNameParent] ) N
		Where	NullIf(P.[AliasNameParent],'') is Not Null),
	[Format] As (
		Select	[AliasNameParent],
				[AliasNameChild],
				Replace(Replace(
					IIF([AliasNameParent] is Null,
						[AliasNameChild],
						Right([AliasNameChild],Len([AliasNameChild]) - Len([AliasNameParent]) -1)),
					'[',''),']','')
					As [AliasElement]
		From	[Parse]
		Where	[AliasNameChild] is Not Null),
	[Tree] As (
		Select	[AliasElement],
				[AliasNameParent],
				[AliasNameChild],
				FormatMessage('[%s]',[AliasElement]) As [AliasName],
				Convert(Int,1) As [Level]
		From	[Format]
		Where	[AliasNameParent] is Null
		Union All
		Select	F.[AliasElement],
				F.[AliasNameParent],
				F.[AliasNameChild],
				FormatMessage('%s.[%s]',T.[AliasName],F.[AliasElement]) As [AliasName],
				T.[Level] + 1 As [Level]
		From	[Tree] T
				Inner Join [Format] F
				On	T.[AliasNameChild] = F.[AliasNameParent])
Select	[AliasName],
		IIF(CharIndex('.', Reverse([AliasName])) = 0,
			Null,
			Left([AliasName], Len([AliasName]) - CharIndex('[.',Reverse([AliasName])) -1))
			As [ParentAliasName],
		[AliasElement],
		[Level],
		IIF(Row_Number() Over (Order By [AliasName] Desc) = 1,1,0) As [IsBase]
From	[Tree])
GO
/*
Select	*
From	[App_DataDictionary].[funcSplitAliasName]('[DatabaseName].[SchemaName].[TableName].[ColumnName]')
Order By [AliasName]

Select	Distinct X.*
From	Sys.Columns C
		outer apply [App_DataDictionary].[funcSplitAliasName](FormatMessage('[%s].[%s].[%s].[%s]',Db_Name(),Object_Schema_Name(object_id),object_name(object_id),name)) X
Order By [AliasName]
*/
